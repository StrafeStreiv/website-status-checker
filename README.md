# Website Status Checker

[![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16.0-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

**Website Status Checker** — это backend-сервис на ASP.NET Core для мониторинга доступности веб-сайтов. Он периодически проверяет список сайтов, сохраняет их HTTP-статусы и предоставляет REST API для управления отслеживаемыми сайтами и просмотра истории проверок.

## Возможности

*   ** RESTful API** для управления сайтами (добавление, удаление, просмотр)
*     ** Фоновый сервис** для автоматической периодической проверки доступности сайтов
*     ** Хранение данных** в PostgreSQL с использованием Entity Framework Core (Code-First подход)
*     ** Полная история проверок** с сохранением кода ответа, времени и статуса
*     ** Автоматическая документация API** через Swagger (OpenAPI)
*     ** Внедрение зависимостей** и чистая архитектура

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

