# Console apps

En console app er en applikasjon som kjører i en terminal. Denne formen er praktisk for å kjøre workers, CLI-verktøy, eller andre applikasjoner som ikke trenger et grensesnitt.

Scaffolding:

```bash
dotnet new console -n MyConsoleApp
```

[Outputen av et console app scaffold](../../samples/MyConsoleApp/) ser slik ut, og genererer to filer:

```
└── samples
    └── MyConsoleApp
        ├── MyConsoleApp.csproj
        └── Program.cs
```

**MyConsoleApp.csproj**

`.csproj`-filer (_prosjekter_) er XML-filer som byggesystemet i .NET, MSBuild, forstår. Disse brukes til metadata og tilsvarer `package.json` i node/npm.

```xml filename="MyConsoleApp.csproj" copy
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

**Program.cs**

Program.cs er default entryfil i .NET og har alltid en statisk `Main`-metode som tar argumenter (likt med Java).

_Program.cs_

```csharp filename="Program.cs" copy
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

I nyere versjoner .NET er entryfilen minimal – uten klasse-definisjon og main-metode:

_Program.cs_

```csharp filename="Program.cs" copy
Console.WriteLine("Hello, World!");
```

Byggesystemet sørger for at koden kompileres til en kjørbar fil.

### Kjøre appen

Bruk .NET SDKet (`dotnet`) i samme mappe som prosjektfilen:

```bash copy
dotnet run
Hello, World!
```

I motsetning til node, så er det ikke nødvendig å kjøre en restore før build/run. Dette gjøres automatisk av byggesystemet.
