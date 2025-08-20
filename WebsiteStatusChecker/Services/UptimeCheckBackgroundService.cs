using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebsiteStatusChecker.Data;
using WebsiteStatusChecker.Models;

namespace WebsiteStatusChecker.Services
{
    public class UptimeCheckBackgroundService : BackgroundService
    {
        private readonly ILogger<UptimeCheckBackgroundService> _logger;
        // Важно! Мы получаем Scope-фабрику, а не сам AppDbContext.
        // Это связано с тем, что BackgroundService живёт долго, а DbContext - нет.
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HttpClient _httpClient;

        // Период проверки (например, раз в 5 минут)
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);

        public UptimeCheckBackgroundService(
            ILogger<UptimeCheckBackgroundService> logger,
            IServiceScopeFactory scopeFactory,
            HttpClient httpClient)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _httpClient = httpClient;
        }

        // Главный метод, который запускает сервис
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Фоновый сервис проверки сайтов запущен.");

            // Создаем периодический таймер, который не блокирует поток
            using PeriodicTimer timer = new PeriodicTimer(_period);

            try
            {
                // Бесконечный цикл, который прервется при получении сигнала остановки
                do
                {
                    _logger.LogInformation("Начинается новый цикл проверки...");
                    await CheckAllWebsitesUptime(); // Вызываем метод проверки
                    _logger.LogInformation("Цикл проверки завершен. Ожидание следующего цикла.");
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Фоновый сервис проверки сайтов остановлен.");
            }
        }

        // Метод для проверки ВСЕХ сайтов из базы данных
        private async Task CheckAllWebsitesUptime()
        {
            // Создаем новый scope для этого цикла проверки.
            // Это гарантирует, что мы получим свежий экземпляр DbContext.
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Получаем ВСЕ сайты из базы данных
                var websites = await dbContext.Websites.ToListAsync();

                _logger.LogInformation("Найдено {Count} сайтов для проверки.", websites.Count);

                foreach (var website in websites)
                {
                    try
                    {
                        _logger.LogInformation("Проверяем сайт: {WebsiteUrl}", website.Url);
                        var checkResult = await CheckSingleWebsite(website.Url);

                        // Создаем и сохраняем запись о проверке
                        var uptimeCheck = new UptimeCheck
                        {
                            WebsiteId = website.Id,
                            CheckTime = DateTime.UtcNow,
                            StatusCode = checkResult.StatusCode,
                            ResponseBody = checkResult.ResponseBodyTruncated,
                            IsUp = checkResult.IsUp
                        };

                        dbContext.UptimeChecks.Add(uptimeCheck);
                        await dbContext.SaveChangesAsync();

                        var status = checkResult.IsUp ? "UP" : "DOWN";
                        _logger.LogInformation("Сайт {WebsiteUrl} имеет статус: {Status} ({StatusCode})", website.Url, status, checkResult.StatusCode);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Произошла ошибка при проверке сайта {WebsiteUrl}", website.Url);
                    }
                }
            }
        }

        // Метод для проверки ОДНОГО сайта
        private async Task<(int StatusCode, string? ResponseBodyTruncated, bool IsUp)> CheckSingleWebsite(string url)
        {
            try
            {
                // Отправляем HTTP-запрос (используем HEAD для экономии трафика, 
                // но если сервер не поддерживает, перейдем на GET)
                using var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

                // Если HEAD не поддерживается, пробуем GET
                if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                {
                    using var getResponse = await _httpClient.GetAsync(url);
                    return ((int)getResponse.StatusCode, null, IsSuccessStatusCode((int)getResponse.StatusCode));
                }

                return ((int)response.StatusCode, null, IsSuccessStatusCode((int)response.StatusCode));
            }
            catch (HttpRequestException ex)
            {
                // Сайт недоступен (таймаут, DNS ошибка и т.д.)
                _logger.LogWarning(ex, "Ошибка сети при проверке сайта {Url}", url);
                return (0, ex.Message, false);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Таймаут запроса
                _logger.LogWarning("Таймаут при проверке сайта {Url}", url);
                return (0, "Timeout", false);
            }
            catch (Exception ex)
            {
                // Любая другая непредвиденная ошибка
                _logger.LogError(ex, "Неожиданная ошибка при проверке сайта {Url}", url);
                return (0, ex.Message, false);
            }
        }

        // Вспомогательный метод для проверки успешного статус-кода
        private bool IsSuccessStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }
    }
}