# Website Status Checker

Простое веб-приложение для мониторинга доступности сайтов с использованием **.NET** и **PostgreSQL**.

---

##  Быстрый старт

###  Предварительные требования

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)  
- [PostgreSQL](https://www.postgresql.org/download/) (версия 14 или выше)  
- [Git](https://git-scm.com/)  

---

### 1. Клонирование репозитория

```bash
git clone https://github.com/StrafeStreiv/website-status-checker.git
cd website-status-checker
````

---

### 2. Настройка базы данных

Создайте базу данных **WebsiteStatusDb** в PostgreSQL.

Обновите строку подключения в файле `WebsiteStatusChecker/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=WebsiteStatusDb;Username=your_username;Password=your_password"
}
```

Замените `your_username` и `your_password` на реальные данные.

---

### 3. Применение миграций базы данных

```bash
cd WebsiteStatusChecker
dotnet ef database update
```

Эта команда создаст все необходимые таблицы в вашей базе данных.

---

### 4. Запуск приложения

```bash
dotnet run
```

Приложение запустится и будет доступно по адресу:
 [https://localhost:7000](https://localhost:7000)
(или другой порт, указанный в выводе команды).

---

### 5. Использование API через Swagger

Откройте в браузере:
 [https://localhost:7000/swagger](https://localhost:7000/swagger)

Вы увидите интерактивную документацию Swagger, где можно тестировать все endpoints API.

---

## API Endpoints

### Управление сайтами (`/api/Websites`)

| Метод  | Эндпоинт             | Описание                    | Тело запроса                        |
| ------ | -------------------- | --------------------------- | ----------------------------------- |
| GET    | `/api/Websites`      | Получить список всех сайтов | -                                   |
| POST   | `/api/Websites`      | Добавить новый сайт         | `{ "name": string, "url": string }` |
| GET    | `/api/Websites/{id}` | Получить сайт по ID         | -                                   |
| DELETE | `/api/Websites/{id}` | Удалить сайт по ID          | -                                   |

---

### Просмотр истории проверок (`/api/UptimeChecks`)

| Метод | Эндпоинт            | Описание                              |
| ----- | ------------------- | ------------------------------------- |
| GET   | `/api/UptimeChecks` | Получить полную историю всех проверок |

---

## Примеры использования

Добавьте сайт для мониторинга:

```bash
curl -X POST "https://localhost:7000/api/Websites" \
-H "Content-Type: application/json" \
-d '{"name": "Google", "url": "https://google.com"}'
```

Проверьте список сайтов:

```bash
curl "https://localhost:7000/api/Websites"
```

Подождите 5 минут (интервал проверки по умолчанию).

Просмотрите историю проверок:

```bash
curl "https://localhost:7000/api/UptimeChecks"
```

---

## Настройка

Все основные настройки находятся в `appsettings.json`:

* **Интервал проверки:** задаётся в `UptimeCheckBackgroundService.cs` (поле `_period`).
* **Строка подключения к БД:** в секции `ConnectionStrings`.
* **Уровень логирования:** в секции `Logging`.

---

## Автор

**StrafeStreiv**

---

## Лицензия

Этот проект лицензирован по лицензии **MIT** – подробности см. в файле [LICENSE](LICENSE).


