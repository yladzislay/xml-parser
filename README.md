### Шаги по развертыванию системы на хосте (Для разработчика):

1. Установите средства разработки [.NET SDK > 8.0.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)'
2. Скачайте [репозиторий](https://github.com/yladzislay/xml-parser.git)
3. Перейдите в каталог проекта: 'cd xml-parser/' и выполните сборку проекта: 'dotnet build'.
5. Перейдите в каталог Executor: 'cd Executor/' и запустите систему 'dotnet run'.

### Инструкции по установке и запуску сервисов:

В решении реализован исполнитель (Executor), который самостоятельно запускает и завершает микросервисы.

### Шаги по созданию и настройке базы данных:

В решении используется Entity Framework Core (ORM-система), которая самостоятельно создаёт и настраивает компактную файловую СУБД - SQLite.   

### Конфигурирование подключения к RabbitMQ:

Вы можете настроить подключение к RabbitMQ в конфигурационном файле 'appsettings.json'.