# 🎛️ Konfigurasjon

Config kan hentes fra ulike kilder, og i en app med _flere_ kilder hentes de ut i prioritert rekkefølge:

1. Kommandolinje-argumenter
2. Miljøvariable
3. Filer (appsettings.json)

Man trenger ikke konfigurere noe ekstra kode for dette, dersom man bruker `DefaultBuilder`-oppsettet.

Ønsker man å mappe seksjoner med konfigurasjon til typer, anbefales `Microsoft.Extensions.Configuration.Binder`-pakken:

```bash
dotnet add package Microsoft.Extensions.Configuration.Binder
```

Denne lar deg mappe et json objekt:

```json
{
  "myStuff": {
    "myString": "Hello World!",
    "myInt": 42
  }
}
```

evt miljøvariable:

```env
MYSTUFF__MYSTRING=Hello World!
MYSTUFF__MYINT=42
```

til en type i kode:

```csharp
record MyStuff(string MyString, int MyInt);
```

via

```diff
var builder = Host.CreateDefaultBuilder(args);
+ MyStuff myStuff = builder.Configuration.GetRequiredSection("myStuff").Get<MyStuff>();
+ builder.Services.AddSingleton(myStuff);
```

> [!NOTE]
> Les mer om konfigurasjon på [microsoft.com](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)
