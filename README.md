API DaData.ru для C# / .NET
====================

Описание
---------------

Библиотека `dadata-csharp` — это обертка над [API «Дадаты»](https://dadata.ru/api/) для C# и других .NET-языков (.NET Standard 2.0).

Установка
---------

### 1. Подключите библиотеку

Можно установить через [NuGet](https://www.nuget.org/packages/Dadata) или скачать [бинарники](https://github.com/hflabs/dadata-csharp/releases/latest).

Внешние зависимости: [JSON.NET](http://james.newtonking.com/json)

Установить через [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/):

```
dotnet add package Newtonsoft.Json
dotnet add package Dadata
```

Установить через [NuGet CLI](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference):

```
nuget install Newtonsoft.Json
nuget install Dadata
```

Установить через [Package Manager](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell):

```
Install-Package Newtonsoft.Json
Install-Package Dadata
```

### 2. Получите API-ключи

Зарегистрируйтесь на [dadata.ru](https://dadata.ru) и получите API-ключи в [личном кабинете](https://dadata.ru/profile/#info).

### 3. Пользуйтесь API!

Примеры вызова API смотрите [в юнит-тестах](https://github.com/hflabs/dadata-csharp/blob/master/Dadata.Test) или ниже по тексту.

Использование
---------

В примерах используются асинхронные API-клиенты: `CleanClientAsync`, `SuggestClientAsync`, `OutwardClientAsync` и `ProfileClientAsync`. У них есть синхронные альтернативы с суффиксом `Sync` — они существуют для обратной совместимости и в будущих версиях будут удалены. Рекомендуем использовать асинхронные версии.

Прежде всего, подключите пространства имён:

```csharp
using Dadata;
using Dadata.Model;
```

### [API стандартизации](https://dadata.ru/api/clean/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var secret = "ВАШ_СЕКРЕТНЫЙ_КЛЮЧ";
var api = new CleanClientAsync(token, secret);
```

И используйте для обработки интересных вам типов данных:

```csharp
var address = await api.Clean<Address>("Москва Милютинский 13");
var birthdate = await api.Clean<Birthdate>("12.03.1990");
var email = await api.Clean<Email>("anderson@matrix.ru");
var fullname = await api.Clean<Fullname>("Ольга Викторовна Раздербань");
var phone = await api.Clean<Phone>("89168459285");
var passport = await api.Clean<Passport>("4506 629672");
var vehicle = await api.Clean<Vehicle>("форд фокус");
```

Можно за один раз обработать запись из нескольких полей (например, ФИО + адрес + телефон):

```csharp
var structure = new[] { StructureType.NAME, StructureType.ADDRESS, StructureType.PHONE };
var data = new[] { "Кузнецов Петр Алексеич", "Москва Милютинский 13", "846)231.60.14" };
var cleaned = await api.Clean(structure, data);
var fullname = (Fullname)cleaned[0];
var address = (Address)cleaned[1];
var phone = (Phone)cleaned[2];
```

### [API подсказок](https://dadata.ru/api/suggest/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И используйте для обработки интересных вам типов данных.

Например, компаний:

```csharp
var response = await api.SuggestParty("моторика сколково");
var party = response.suggestions[0];
```

```csharp
var request = new SuggestPartyRequest("вита")
{
    type = PartyType.INDIVIDUAL,
    locations = new[] {
        new Address() { kladr_id = "78" },
    }
};
var response = await api.SuggestParty(request);
var party = response.suggestions[0];
```

Или банков:

```csharp
var response = await api.SuggestBank("тинь");
var bank = response.suggestions[0].data;
```

```csharp
var request = new SuggestBankRequest("я")
{
    type = new[] { BankType.NKO }
};
var response = await api.SuggestBank(request);
var bank = response.suggestions[0].data;
```

### [Адрес по координатам](https://dadata.ru/api/geolocate/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И получите список ближайших адресов по заданным координатам:

```csharp
var response = await api.Geolocate(lat: 55.7366021, lon: 37.597643);
var address = response.suggestions[0].data;
```

### [Адрес по коду КЛАДР или ФИАС](https://dadata.ru/api/find-address/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И получите адрес по КЛАДР- или ФИАС-коду:

```csharp
var response = await api.FindAddress("7700000000000");
var address = response.suggestions[0].data;
```

```csharp
var response = await api.FindAddress("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
var address = response.suggestions[0].data;
```

### [Город по IP-адресу](https://dadata.ru/api/iplocate/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И получите город по IP-адресу:

```csharp
var response = await api.Iplocate("213.180.193.3");
var address = response.location.data;
```

### [Город в службе доставки](https://dadata.ru/api/delivery/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Find<DeliveryCity>("3100400100000");
var city = response.suggestions[0].data;
```

### [Почтовое отделение](https://dadata.ru/api/suggest/postal_unit/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);
```

Полнотекстовый поиск:

```csharp
var response = await api.Suggest<PostalUnit>("дежнева 2а");
var unit = response.suggestions[0].data;
```

Поиск по почтовому индексу:

```csharp
var response = await api.Find<PostalUnit>("127642");
var unit = response.suggestions[0].data;
```

Поиск ближайшего по координатам:

```csharp
var response = await api.Geolocate<PostalUnit>(lat: 55.878, lon: 37.653, radius_meters: 1000);
var unit = response.suggestions[0].data;
```

### [Станция метро](https://dadata.ru/api/suggest/metro/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);
```

Полнотекстовый поиск:

```csharp
var response = await api.Suggest<MetroStation>("александр");
var station = response.suggestions[0].data;
```

Фильтрация по городу:

```csharp
var request = new SuggestOutwardRequest("александр")
{
    filters = new Dictionary<string, string>() { { "city", "Санкт-Петербург" } }
};
var response = await api.Suggest<MetroStation>(request);
var station = response.suggestions[0].data;
```

### [Страна](https://dadata.ru/api/suggest/country/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Suggest<Country>("ru");
var country = response.suggestions[0].data;
```

### [Организация по ИНН или ОГРН](https://dadata.ru/api/find-party/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И получите компанию по ИНН или ОГРН:

```csharp
var response = await api.FindParty("7719402047");
var party = response.suggestions[0].data;
```

```csharp
var response = await api.FindParty("1157746078984");
var party = response.suggestions[0].data;
```

```csharp
var request = new FindPartyRequest(query: "7728168971", kpp: "667102002");
var response = await api.FindParty(request);
var party = response.suggestions[0].data;
```

### [Поиск аффилированных компаний](https://dadata.ru/api/find-affiliated/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И найдите компании по ИНН учредителей и руководителей:

```csharp
var response = await api.FindAffiliated("7736207543");
var party = response.suggestions[0].data;
```

```csharp
var request = new FindAffiliatedRequest("773006366201")
{
    scope = new[] { FindAffiliatedScope.MANAGERS }
};
var response = await api.FindAffiliated(request);
var party = response.suggestions[0].data;
```

### [Банк по БИК, SWIFT или рег. номеру](https://dadata.ru/api/find-bank/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClientAsync(token);
```

И получите банк по идентификатору:

```csharp
// БИК
var response = await api.FindBank("044525974");
var bank = response.suggestions[0].data;
```

```csharp
// SWIFT
var response = await api.FindBank("TICSRUMMXXX");
var bank = response.suggestions[0].data;
```

```csharp
// Рег. номер
var response = await api.FindBank("2673");
var bank = response.suggestions[0].data;
```

### [Кем выдан паспорт](https://dadata.ru/api/suggest/fms_unit/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Suggest<FmsUnit>("772-053");
var unit = response.suggestions[0].data;
```

### [Налоговая инспекция](https://dadata.ru/api/suggest/fns_unit/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Find<FnsUnit>("5257");
var unit = response.suggestions[0].data;
```

### [Марка автомобиля](https://dadata.ru/api/suggest/car_brand/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Suggest<CarBrand>("FORD");
var brand = response.suggestions[0].data;
```

### [ОКВЭД 2](https://dadata.ru/api/suggest/okved2/)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new OutwardClientAsync(token);

var response = await api.Suggest<OkvedRecord>("51.22.3");
var record = response.suggestions[0].data;
```

### [API личного кабинета](https://dadata.ru/api/#profile)

```csharp
var token = "ВАШ_API_КЛЮЧ";
var secret = "ВАШ_СЕКРЕТНЫЙ_КЛЮЧ";
var api = new ProfileClientAsync(token, secret);
```

Баланс:

```csharp
var response = await api.GetBalance();
var balance = response.balance;
```

Статистика использования:

```csharp
var response = await api.GetDailyStats();
var cleanCount = response.services.clean;
var suggestionsCount = response.services.suggestions;
var mergingCount = response.services.merging;
```

Версии справочников:

```csharp
var response = await api.GetVersions();
var egrulVersion = response.suggestions.resources["ЕГРЮЛ"];
var geoVersion = response.factor.resources["Геокоординаты"];
```

[История версий](https://github.com/hflabs/dadata-csharp/releases)
-----

Библиотека использует [CalVer](https://calver.org/) по схеме YY.MM.MINOR (например, 20.7.2 = третий релиз в июле 2020). Подробности изменений — в описании каждого релиза.
