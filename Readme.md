# Planner

Кроссплатформенное приложение для управления задачами, разработанное на Avalonia UI и ASP.NET Core.

## Описание

Planner позволяет пользователям создавать, редактировать и удалять задачи, отслеживать сроки их выполнения и управлять списком задач через удобный интерфейс.

Основные возможности:

* регистрация и авторизация пользователей;
* создание, редактирование и удаление задач;
* изменение статуса выполнения задач;
* установка дедлайнов;
* фильтрация задач;
* визуальная индикация просроченных и выполненных задач.

## Стек технологий

### Клиентская часть

* Avalonia UI
* CommunityToolkit.Mvvm
* HttpClient

### Серверная часть

* ASP.NET Core Web API
* Entity Framework Core
* JWT Authentication

### База данных

* PostgreSQL

## Архитектура

```text
Avalonia UI
      │
      ▼
ASP.NET Core Web API
      │
      ▼
PostgreSQL
```

## Запуск проекта

### 1. Клонирование репозитория

```bash
git clone https://github.com/USERNAME/Planner.git
cd Planner
```

### 2. Настройка базы данных

Создать базу данных PostgreSQL и указать строку подключения в файле:

```text
Planner_server/appsettings.json
```

Пример:

```json
"ConnectionStrings": {
  "DbConnection": "Host=localhost;Database=PlannerDb;Username=postgres;Password=password"
}
```

### 3. Применение миграций

```bash
dotnet ef database update
```

### 4. Запуск сервера

```bash
cd Planner_server
dotnet run
```

По умолчанию API будет доступно по адресу:

```text
https://localhost:5001
```

Swagger:

```text
https://localhost:5001/swagger
```

### 5. Запуск клиента

```bash
cd Planner_UI
dotnet run
```

## Реализованный функционал

* JWT авторизация;
* REST API;
* CRUD операции для задач;
* дедлайны;
* фильтрация задач;
* отображение статуса выполнения;
* поддержка нескольких пользователей.

## Перспективы развития

* поиск задач;
* уведомления о дедлайнах;
* категории и теги;
* командная работа;
* мобильная версия приложения.

## Автор

Михлик Богдан

Разработано в рамках дипломного проекта.
