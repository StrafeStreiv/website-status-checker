using Microsoft.EntityFrameworkCore;
using WebsiteStatusChecker.Models;

namespace WebsiteStatusChecker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Website> Websites { get; set; }
        public DbSet<UptimeCheck> UptimeChecks { get; set; }

        // Конструктор, который принимает опции (настройки БД) - ОБЯЗАТЕЛЕН для DI
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Можно удалить или закомментировать старый конструктор, если он был без параметров.
        // public AppDbContext() { }
    }
}