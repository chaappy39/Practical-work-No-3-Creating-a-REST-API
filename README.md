# Создание REST API

## 1. Инициализация приложения: Создается новый экземпляр приложения с помощью WebApplication.CreateBuilder, который позволяет настраивать зависимости и конфигурацию.

## 2. Настройка базы данных: Регистрация контекста базы данных AgarContext, который использует SQL Server для работы с данными.

## 3. Добавление контроллеров: Регистрация сервисов контроллеров для обработки HTTP-запросов.

## 4. Настройка Swagger: Добавление Swagger для автоматической генерации документации API, позволяющей тестировать его через веб-интерфейс.

## 5. Построение приложения: Создание экземпляра приложения на основе ранее заданных настроек.

## 6. Конфигурация middleware: Настройка промежуточного программного обеспечения для обработки HTTPS-редиректа, авторизации и маршрутизации запросов к контроллерам.

## 7. Запуск приложения: Приложение запускается и начинает прослушивание входящих HTTP-запросов.

## 8. Определение контроллера: Создание контроллера AdController, который отвечает за обработку запросов, связанных с рекламными заказами.

## 9. Методы контроллера: Определены методы для:

### Получения средней стоимости рекламных заказов.
### Получения списка рекламных заказов, стоимость которых превышает указанную величину.
### Определения самых популярных рекламных заказов на основе общего времени показа.
