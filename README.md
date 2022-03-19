# SDK для интеграции с АПИ Контур Экстерн

## Быстрый старт

### Шаг 1. Создание и настройка объекта IExtern

Для выполнения запросов необходимы реквизиты доступа, получить их можно обратившись по контактам, указанным в [соответствующем разделе  документации](https://docs-ke.kontur.ru/auth_oidc/api-key.html)

#### Тестовая площадка

    string apiKey = "..."; // Апи-ключ, назначенный вашему приложению, для использования на ТЕСТОВОЙ площадке
    string someUserLogin = "..."; // Логин пользователя (на ТЕСТОВОЙ площадке), от имени которого выполняется аутентификация
    string someUserPassword = "..."; // Пароль пользователя (на ТЕСТОВОЙ площадке), от имени которого выполняется аутентификация
    string clientId = "..."; // Уникальное сервисное имя вашего приложения (на ТЕСТОВОЙ площадке)

    IExtern externApi = new ExternBuilder()
          .WithExternApiUrl(
              new Uri("https://extern-api.testkontur.ru/"), // урл ТЕСТОВОЙ площадки апи Экстерна
              log)
          .WithOpenIdAuthenticator(
                builder => builder
                .WithHttpConfiguration(new TestingHttpClientConfiguration("https://identity.testkontur.ru")) // урл ТЕСТОВОЙ площадки сервиса аутентификации
                .WithClientIdentification(clientId, apiKey)
                .WithAuthenticationByPassword(someUserLogin, someUserPassword)
                .Build())                
           .Create();
           
           
#### Продуктовая площадка


    // Значения параметров для продуктовой площадки не будут совпадать с аналогичными значениями для тестовой площадки
    string apiKey = "..."; // Апи-ключ, назначенный вашему приложению, для использования на ПРОДУКТОВОЙ площадке
    string someUserLogin = "..."; // Логин пользователя (на ПРОДУКТОВОЙ площадке), от имени которого выполняется аутентификация
    string someUserPassword = "..."; // Пароль пользователя (на ПРОДУКТОВОЙ площадке), от имени которого выполняется аутентификация
    string clientId = "..."; // Уникальное сервисное имя вашего приложения (на ПРОДУКТОВОЙ площадке)

    IExtern externApi = new ExternBuilder()
            .WithExternApiUrl(
                new Uri("https://extern-api.kontur.ru/"), // урл ПРОДУКТОВОЙ площадки апи Экстерна
                log)
            .WithPasswordAuthentication( // Дополнительно указывать урл ПРОДУКТОВОЙ площадки сервиса аутентификации не нужно, его значение используется по умолчанию.
                                         // Для более гибкой настройки процесса аутентификации вместо вызова .WithPasswordAuthentication(...) можно воспользоваться
                                         // .WithOpenIdAuthenticator(...), аналогично примеру для тестовой площадки. В этом случае в .WithHttpConfiguration(...) нужно
                                         // будет передать урл ПРОДУКТОВОЙ площадки сервиса аутентификации - "https://identity.kontur.ru"
                new Credentials(someUserLogin, someUserPassword),
                clientId,
                apiKey)
            .Create();
            
### Шаг 2. Выполнение запросов

После создания и настройки объекта IExtern можно использовать его для выполнения запросов к АПИ. Например, загрузить все учетные записи, доступные аутентифицированному пользователю:

    IReadOnlyList<Account> accounts = await externApi.Accounts.List().SliceBy(100).LoadAllAsync();

### (!) Дополнительно аутентифицироваться до выполнения запросов не нужно (!)
Cдк автоматически выполнит попытку аутентифицироваться, если код ответа на запрос будет 403.

Данное поведение можно отключить, вызвав 

     .TryResolveUnauthorizedResponsesAutomatically(false) 

при настройке IExtern после вызова
    
    .WithPasswordAuthentication(...) или .WithOpenIdAuthenticator(...)

В таком случае перед выполнением запросов нужно будет выполнить код 

    await externApi.ReauthenticateAsync();

Также сдк автоматически выполнит реаутенификакцию при приблежении времени истечения токена. 
Интервал можно задать, вызвав

    RefreshAccessTokensBeforeExpirationProactivelyWithinInterval(...)
    
при настройке внутри вызова .WithOpenIdAuthenticator(...). Например, после вызова .WithAuthenticationByPassword(...).


## Другие методы аутентификации

Доступна аутентификация по сертификату, для этого вместо .WithPasswordAuthentication(...) нужно вызвать  .WithCertificateAuthentication(...).

Более подробно про процесс аутентификации можно прочитать [ в документации к АПИ](https://docs-ke.kontur.ru/auth_oidc/index.html)

