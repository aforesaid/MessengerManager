# MessengerManager

### Бот, позволяющий управлять всеми поддерживаемыми соц. сетями с помощью канала в Telegram.

# Стек

### Net 5.0, Docker, PostgreSql, в качестве ORM выступает EntityFrameworkCore.

# Конфигурация

```json
{
    "POSTGRESQL": "Строка подключение к БД, провайдер PostgreSql",
    "Telegram": {
        "Token": "Token бота",
        "MainChatId": "ChatId канала",
        "SupportChatId": "ChatId группы"
    }
}
```
