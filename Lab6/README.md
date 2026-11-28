# Лабораторная работа №6 — «Новогодний подарок»

Многослойное приложение на C# / .NET 8 Windows Forms.

## Слои
- **BusinessLayer** — модели конфет, интерфейс IGift и бизнес-операции с подарком.
- **DataAccessLayer** — сохранение и загрузка данных в JSON.
- **ServiceLayer** — координация бизнес-логики и репозитория.
- **PresentationLayer** — графический интерфейс Windows Forms.

## Запуск
Откройте `NewYearGift_Lab6.sln` в Visual Studio, назначьте `PresentationLayer` запускаемым проектом и нажмите F5.

Данные сохраняются в `gift.json` в рабочей папке приложения.
