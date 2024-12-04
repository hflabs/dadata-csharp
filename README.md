# Dadata API Client

> Data cleansing, enrichment and suggestions via [Dadata API](https://dadata.ru/api)

[![Nuget Version][nuget-image]][nuget-url]
[![Downloads][downloads-image]][nuget-url]

The official Dadata .NET library, supporting .NET Standard 2.0+

## Installation

Using the .NET Core command-line interface (CLI) tools:

```
dotnet add package Newtonsoft.Json
dotnet add package Dadata
```

Using the NuGet Command Line Interface (CLI):

```
nuget install Newtonsoft.Json
nuget install Dadata
```

Using the Package Manager Console:

```
Install-Package Newtonsoft.Json
Install-Package Dadata
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on _Manage NuGet Packages..._
4. Click on the _Browse_ tab and search for "Dadata".
5. Click on the Dadata package, select the appropriate version in the right-tab and click _Install_.

## Usage

Import namespaces:

```csharp
using Dadata;
using Dadata.Model;
```

Create API client instance:

```csharp
var token = "Replace with Dadata API key";
var secret = "Replace with Dadata secret key";

var api = new CleanClientAsync(token, secret);
// or any of the following, depending on the API method
api = new SuggestClientAsync(token);
api = new OutwardClientAsync(token);
api = new ProfileClientAsync(token, secret);
```

Then call API methods as specified below.

Examples use async clients: `CleanClientAsync`, `SuggestClientAsync`, `OutwardClientAsync` and `ProfileClientAsync`. There are sync alternatives with `Sync` suffixes — they exist for backward compatibility and will be removed in future releases.

## Postal Address

### [Validate and cleanse address](https://dadata.ru/api/clean/address/)

```csharp
var api = new CleanClientAsync(token, secret);
var address = await api.Clean<Address>("мск сухонская 11 89");
```

### [Geocode address](https://dadata.ru/api/geocode/)

Same API method as "validate and cleanse":

```csharp
var api = new CleanClientAsync(token, secret);
var address = await api.Clean<Address>("москва сухонская 11");
```

### [Reverse geocode address](https://dadata.ru/api/geolocate/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.Geolocate(lat: 55.878, lon: 37.653);
var address = response.suggestions[0].data;
```

### [GeoIP city](https://dadata.ru/api/iplocate/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.Iplocate("46.226.227.20");
var address = response.location.data;
```

### [Autocomplete (suggest) address](https://dadata.ru/api/suggest/address/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.SuggestAddress("самара метал");
var address = response.suggestions[0].data;
```

Show suggestions in English:

```csharp
var request = new SuggestAddressRequest("samara metal") { language = "en" };
var response = await api.SuggestAddress(request);
var address = response.suggestions[0].data;
```

Constrain by city (Yuzhno-Sakhalinsk):

```csharp
var request = new SuggestAddressRequest("ватутина")
{
    locations = new[] {
        new Address() { kladr_id = "6500000100000" },
    }
};
var response = await api.SuggestAddress(request);
var address = response.suggestions[0].data;
```

Constrain by specific geo point and radius (in Vologda city):

```csharp
var request = new SuggestAddressRequest("сухонская")
{
    locations_geo = new[]
    {
        new LocationGeo() { lat=59.244634, lon=39.913355, radius_meters=200}
    }
};
var response = await api.SuggestAddress(request);
var address = response.suggestions[0].data;
```

Boost city to top (Toliatti):

```csharp
var request = new SuggestAddressRequest("авто")
{
    locations_boost = new[]
    {
        new Address() { kladr_id = "6300000700000" },
    }
};
var response = await api.SuggestAddress(request);
var address = response.suggestions[0].data;
```

### [Find address by FIAS ID](https://dadata.ru/api/find-address/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.FindAddress("9120b43f-2fae-4838-a144-85e43c2bfb29");
var address = response.suggestions[0].data;
```

Find by KLADR ID:

```csharp
var response = await api.FindAddress("77000000000268400");
var address = response.suggestions[0].data;
```

### [Find postal office](https://dadata.ru/api/suggest/postal_unit/)

Suggest postal office by address or code:

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<PostalUnit>("дежнева 2а");
var unit = response.suggestions[0].data;
```

Find postal office by code:

```csharp
var response = await api.Find<PostalUnit>("127642");
var unit = response.suggestions[0].data;
```

Find nearest postal office:

```csharp
var response = await api.Geolocate<PostalUnit>(lat: 55.878, lon: 37.653, radius_meters: 1000);
var unit = response.suggestions[0].data;
```

### [Get City ID for delivery services](https://dadata.ru/api/delivery/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Find<DeliveryCity>("3100400100000");
var city = response.suggestions[0].data;
```

### [Get address strictly according to FIAS](https://dadata.ru/api/find-fias/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.FindFias("9120b43f-2fae-4838-a144-85e43c2bfb29");
var address = response.suggestions[0].data;
```

### [Suggest country](https://dadata.ru/api/suggest/country/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<Country>("та");
var country = response.suggestions[0].data;
```

## Company or individual enterpreneur

### [Find company by INN](https://dadata.ru/api/find-party/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.FindParty("7707083893");
var party = response.suggestions[0].data;
```

Find by INN and KPP:

```csharp
var request = new FindPartyRequest(query: "7707083893", kpp: "540602001");
var response = await api.FindParty(request);
var party = response.suggestions[0].data;
```

### [Suggest company](https://dadata.ru/api/suggest/party/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.SuggestParty("сбер");
var party = response.suggestions[0];
```

Constrain by specific regions (Saint Petersburg and Leningradskaya oblast):

```csharp
var request = new SuggestPartyRequest("сбер")
{
    locations = new[] {
        new Address() { kladr_id = "7800000000000" },
        new Address() { kladr_id = "4700000000000" },
    }
};
var response = await api.SuggestParty(request);
var party = response.suggestions[0];
```

Constrain by active companies:

```csharp
var request = new SuggestPartyRequest("сбер")
{
    status = new[] { PartyStatus.ACTIVE }
};
var response = await api.SuggestParty(request);
var party = response.suggestions[0];
```

Constrain by individual entrepreneurs:

```csharp
var request = new SuggestPartyRequest("сбер")
{
    type = PartyType.INDIVIDUAL
};
var response = await api.SuggestParty(request);
var party = response.suggestions[0];
```

### [Find affiliated companies](https://dadata.ru/api/find-affiliated/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.FindAffiliated("7736207543");
var party = response.suggestions[0].data;
```

Search only by manager INN:

```csharp
var request = new FindAffiliatedRequest("773006366201")
{
    scope = new[] { FindAffiliatedScope.MANAGERS }
};
var response = await api.FindAffiliated(request);
var party = response.suggestions[0].data;
```

## Bank

### [Find bank by BIC, SWIFT or INN](https://dadata.ru/api/find-bank/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.FindBank("044525225");
var bank = response.suggestions[0].data;
```

Find by SWIFT code:

```csharp
var response = await api.FindBank("SABRRUMM");
var bank = response.suggestions[0].data;
```

Find by INN:

```csharp
var response = await api.FindBank("7728168971");
var bank = response.suggestions[0].data;
```

Find by INN and KPP:

```csharp
var request = new FindBankRequest(query: "7728168971", kpp: "667102002");
var response = await api.FindBank(request);
var bank = response.suggestions[0].data;
```

Find by registration number:

```csharp
var response = await api.FindBank("1481");
var bank = response.suggestions[0].data;
```

### [Suggest bank](https://dadata.ru/api/suggest/bank/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.SuggestBank("ти");
var bank = response.suggestions[0].data;
```

## Personal name

### [Validate and cleanse name](https://dadata.ru/api/clean/name/)

```csharp
var api = new CleanClientAsync(token, secret);
var fullname = await api.Clean<Fullname>("Срегей владимерович иванов");
```

### [Suggest name](https://dadata.ru/api/suggest/name/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.SuggestName("викт");
var fullname = response.suggestions[0].data;
```

Suggest female first name:

```csharp
var request = new SuggestNameRequest("викт")
{
    parts = new[] { FullnamePart.NAME },
    gender = Gender.FEMALE
};
var response = await api.SuggestName(request);
var fullname = response.suggestions[0].data;
```

## Phone

### [Validate and cleanse phone](https://dadata.ru/api/clean/phone/)

```csharp
var api = new CleanClientAsync(token, secret);
var phone = await api.Clean<Phone>("9168-233-454");
```

## Passport

### [Validate passport](https://dadata.ru/api/clean/passport/)

```csharp
var api = new CleanClientAsync(token, secret);
var passport = await api.Clean<Passport>("4509 235857");
```

### [Suggest issued by](https://dadata.ru/api/suggest/fms_unit/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<FmsUnit>("772 053");
var unit = response.suggestions[0].data;
```

## Email

### [Validate email](https://dadata.ru/api/clean/email/)

```csharp
var api = new CleanClientAsync(token, secret);
var email = await api.Clean<Email>("serega@yandex/ru");
```

### [Suggest email](https://dadata.ru/api/suggest/email/)

```csharp
var api = new SuggestClientAsync(token);
var response = await api.SuggestEmail("maria@");
var email = response.suggestions[0].data;
```

## Other datasets

### [Tax office](https://dadata.ru/api/suggest/fns_unit/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Find<FnsUnit>("5257");
var unit = response.suggestions[0].data;
```

### [Metro station](https://dadata.ru/api/suggest/metro/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<MetroStation>("алек");
var station = response.suggestions[0].data;
```

Constrain by city (Saint Petersburg):

```csharp
var request = new SuggestOutwardRequest("алек")
{
    filters = new Dictionary<string, string>() { { "city", "Санкт-Петербург" } }
};
var response = await api.Suggest<MetroStation>(request);
var station = response.suggestions[0].data;
```

### [Car brand](https://dadata.ru/api/suggest/car_brand/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<CarBrand>("фо");
var brand = response.suggestions[0].data;
```

### [OKTMO](https://dadata.ru/api/suggest/oktmo/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Find<OktmoRecord>("54623425");
var record = response.suggestions[0].data;
```

### [OKVED 2](https://dadata.ru/api/suggest/okved2/)

```csharp
var api = new OutwardClientAsync(token);
var response = await api.Suggest<OkvedRecord>("космических");
var record = response.suggestions[0].data;
```

## Profile API

```csharp
var api = new ProfileClientAsync(token, secret);
```

Balance:

```csharp
var response = await api.GetBalance();
var balance = response.balance;
```

Usage stats:

```csharp
var response = await api.GetDailyStats();
var cleanCount = response.services.clean;
var suggestionsCount = response.services.suggestions;
var mergingCount = response.services.merging;
```

Dataset versions:

```csharp
var response = await api.GetVersions();
var egrulVersion = response.suggestions.resources["ЕГРЮЛ"];
var geoVersion = response.factor.resources["Геокоординаты"];
```

## Contributing

This project only accepts bug fixes.

## [Changelog](https://github.com/hflabs/dadata-csharp/releases)

This project uses [CalVer](https://calver.org/) with YY.MM.MICRO schema. See changelog for details specific to each release.

## License

[MIT](https://choosealicense.com/licenses/mit/)

<!-- Markdown link & img dfn's -->

[nuget-image]: https://img.shields.io/nuget/v/dadata?style=flat-square
[nuget-url]: https://www.nuget.org/packages/dadata/
[downloads-image]: https://img.shields.io/nuget/dt/dadata?style=flat-square
