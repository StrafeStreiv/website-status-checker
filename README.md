# Website Status Checker

[![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16.0-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

**Website Status Checker** — это backend-сервис на ASP.NET Core для мониторинга доступности веб-сайтов. Он периодически проверяет список сайтов, сохраняет их HTTP-статусы и предоставляет REST API для управления отслеживаемыми сайтами и просмотра истории проверок.

## Возможности

*     RESTful API для управления сайтами (добавление, удаление, просмотр)
*     Фоновый сервис для автоматической периодической проверки доступности сайтов
*     Хранение данных** в PostgreSQL с использованием Entity Framework Core (Code-First подход)
*     Полная история проверок** с сохранением кода ответа, времени и статуса
*     Автоматическая документация API** через Swagger (OpenAPI)
*     Внедрение зависимостей** и чистая архитектура

## Стек технологий

*   **Backend:** ASP.NET Core 7.0, C# 11
*   **База данных:** PostgreSQL, Entity Framework Core 7.0
*   **Инструменты:** Swagger, HttpClientFactory
*   **Архитектура:** REST API, Фоновые сервисы (BackgroundService), Внедрение зависимостей

## Структура проекта
website-status-checker/
│
├── WebsiteStatusChecker/ # Основной проект
│ ├── Controllers/ # Контроллеры API
│ │ ├── WebsitesController.cs # Управление сайтами (CRUD)
│ │ └── UptimeChecksController.cs # Просмотр истории проверок
│ ├── Data/ # Работа с данными
│ │ └── AppDbContext.cs # Контекст базы данных
│ ├── Models/ # Модели данных
│ │ ├── Website.cs # Сущность "Веб-сайт"
│ │ └── UptimeCheck.cs # Сущность "Результат проверки"
│ ├── Services/ # Фоновые и бизнес-сервисы
│ │ └── UptimeCheckBackgroundService.cs # Сервис периодической проверки
│ ├── Migrations/ # Миграции базы данных
│ ├── Properties/ # Свойства проекта
│ ├── appsettings.json # Конфигурация приложения
│ ├── Program.cs # Точка входа и конфигурация
│ └── WebsiteStatusChecker.csproj # Файл проекта
│
├── WebsiteMonitoringSolution.sln # Решение Visual Studio
├── LICENSE # Лицензия MIT
├── .gitignore # Git ignore правила
└── README.md # Этот файл
## Быстрый старт

### Предварительные требования

*   [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
*   [PostgreSQL](https://www.postgresql.org/download/) (версия 14 или выше)
*   Git

### 1. Клонирование репозитория

```bash
git clone https://github.com/your-username/website-status-checker.git
cd website-status-checker

# Website Status Checker

Простое веб-приложение для мониторинга доступности сайтов с использованием .NET и PostgreSQL.

---

### 2. Настройка базы данных
Создайте базу данных WebsiteStatusDb в вашем PostgreSQL.

Обновите строку подключения в файле WebsiteStatusChecker/appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=WebsiteStatusDb;Username=your_username;Password=your_password"
}
Замените your_username и your_password на реальные данные для подключения.

3. Применение миграций базы данных
bash
cd WebsiteStatusChecker
dotnet ef database update
Эта команда создаст все необходимые таблицы в вашей базе данных.

4. Запуск приложения
bash
dotnet run
Приложение запустится и будет доступно по адресу https://localhost:7000 (или другому порту, указанному в выводе команды).

5. Использование API через Swagger
Откройте в браузере адрес https://localhost:7000/swagger. Вы увидите интерактивную документацию Swagger, через которую можно тестировать все endpoints API.

📡 API Endpoints
Управление сайтами (/api/Websites)
Метод	Эндпоинт	Описание	Тело запроса
GET	/api/Websites	Получить список всех сайтов	-
POST	/api/Websites	Добавить новый сайт для мониторинга	{"name": string, "url": string}
GET	/api/Websites/{id}	Получить сайт по ID	-
DELETE	/api/Websites/{id}	Удалить сайт по ID	-
Просмотр истории проверок (/api/UptimeChecks)
Метод	Эндпоинт	Описание
GET	/api/UptimeChecks	Получить полную историю всех проверок
🧪 Пример использования
Добавьте сайт для мониторинга:

bash
curl -X POST "https://localhost:7000/api/Websites" \
-H "Content-Type: application/json" \
-d '{"name": "Google", "url": "https://google.com"}'
Проверьте список сайтов:

bash
curl "https://localhost:7000/api/Websites"
Подождите 5 минут (интервал проверки по умолчанию).

Просмотрите историю проверок:

bash
curl "https://localhost:7000/api/UptimeChecks"
🔧 Настройка
Основные настройки можно изменить в файле appsettings.json:

Интервал проверки: Измените значение в коде UptimeCheckBackgroundService.cs (поле _period)

Строка подключения к БД: В секции ConnectionStrings

Уровень логирования: В секции Logging

👨‍💻 Автор
StrafeStreiv

📄 Лицензия
Этот проект лицензирован по лицензии MIT - подробности см. в файле LICENSE.
