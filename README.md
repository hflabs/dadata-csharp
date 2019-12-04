API DaData.ru для C# / .NET
====================

Описание
---------------

Библиотека `dadata-csharp` — это обертка над [API «Дадаты»](https://dadata.ru/api/) для C# и других .NET-языков.

Установка
---------

### 1. Подключите библиотеку

Можно установить через [NuGet](https://www.nuget.org/packages/Dadata) или скачать [бинарники](https://github.com/hflabs/dadata-csharp/releases/latest).

Внешние зависимости:

- [JSON.NET](http://james.newtonking.com/json)

### 2. Получите API-ключи

Зарегистрируйтесь на [dadata.ru](https://dadata.ru) и получите API-ключи в [личном кабинете](https://dadata.ru/profile/#info).

### 3. Пользуйтесь API!

Примеры вызова API смотрите [в юнит-тестах](https://github.com/hflabs/dadata-csharp/blob/master/Dadata.Test) или ниже по тексту.

Использование
---------

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
var api = new CleanClient(token, secret);
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
var structure = new List<StructureType> { StructureType.NAME, StructureType.ADDRESS, StructureType.PHONE };
var data = new List<string> { "Кузнецов Петр Алексеич", "Москва Милютинский 13", "846)231.60.14" };
var cleaned = await api.Clean(structure, data);
var fullname = (Fullname)cleaned[0];
var address = (Fullname)cleaned[1];
var phone = (Fullname)cleaned[2];
```

### [API подсказок](https://dadata.ru/api/suggest/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClient(token);
```

И используйте для обработки интересных вам типов данных.

Например, компаний:

```csharp
var response = await api.SuggestParty("моторика сколково");
var party = response.suggestions[0];
```

```csharp
var request = new SuggestPartyRequest("витас");
request.type = PartyType.INDIVIDUAL;
var response = await api.SuggestParty(request);
var party = response.suggestions[0];
```

Или банков:

```csharp
var response = await api.SuggestBank("тинь");
var bank = response.suggestions[0].data;
```

```csharp
var request = new SuggestBankRequest("я");
request.type = new BankType[] { BankType.NKO };
var response = await api.SuggestBank(request);
var bank = response.suggestions[0].data;
```

### [Адрес по координатам](https://dadata.ru/api/geolocate/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new GeolocateClient(token);
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
var api = new SuggestClient(token);
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
var api = new IplocateClient(token);
```

И получите город по IP-адресу:

```csharp
var response = await api.Iplocate("213.180.193.3");
var address = response.location.data;
```

### [Организация по ИНН или ОГРН](https://dadata.ru/api/find-party/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClient(token);
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

### [Банк по БИК, SWIFT или рег. номеру](https://dadata.ru/api/find-bank/)

Создайте апи-клиента:

```csharp
var token = "ВАШ_API_КЛЮЧ";
var api = new SuggestClient(token);
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
