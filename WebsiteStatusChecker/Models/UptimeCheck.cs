namespace WebsiteStatusChecker.Models
{
    public class UptimeCheck
    {
        public int Id { get; set; }
        public int WebsiteId { get; set; } // Ссылка на сайт (Foreign Key)
        public DateTime CheckTime { get; set; } = DateTime.UtcNow; // Время проверки (в UTC)
        public int StatusCode { get; set; } // Полученный HTTP-статус код (200, 404, 500...)
        public string? ResponseBody { get; set; } // Необязательно: можно сохранить часть тела ответа для анализа
        public bool IsUp { get; set; } // Простое поле: true если статус 200-299

        // Навигационное свойство для Entity Framework
        public Website Website { get; set; } = new Website();
    }
}