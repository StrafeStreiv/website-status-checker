using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

using WebsiteStatusChecker.Data;

namespace WebsiteStatusChecker
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1. Создаем конфигурацию, которая будет читать appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Указывает, где искать файл
                .AddJsonFile("appsettings.json") // Читаем этот файл
                .Build();

            // 2. Создаем построитель опций для DbContext
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 3. Получаем строку подключения из конфигурации
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 4. Говорим использовать PostgreSQL с этой строкой подключения
            optionsBuilder.UseNpgsql(connectionString);

            // 5. Возвращаем новый экземпляр AppDbContext с настроенными опциями
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}