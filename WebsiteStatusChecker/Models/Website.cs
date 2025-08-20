namespace WebsiteStatusChecker.Models
{
    public class Website
    {
        public int Id { get; set; } // Уникальный идентификатор (Primary Key в БД)
        public string Url { get; set; } = string.Empty; // URL сайта для проверки
        public string? Name { get; set; } // Необязательное понятное имя сайта
    }
}