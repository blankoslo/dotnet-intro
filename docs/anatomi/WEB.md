## Web apps

Eksempler på web apps er

- Serverside-rendrede UI-applikasjoner
- HTTP-APIer.

Scaffolding av et API-prosjekt:

```
dotnet new web -n MyApiApp
```

[Outputen av et web app scaffold](../../samples/MyApiApp/):

```diff
└── samples
    └── MyApiApp
        ├── MyApiApp.csproj
        ├── Program.cs
+       ├── Properties
+       │   └── launchSettings.json
+       ├── appsettings.Development.json
+       └── appsettings.json
```

**MyApiApp.csproj**

Et par ting å merke seg er at SDK-definisjonen nå inkluderer `.Web`-postfikset. Dette gir prosjektet noen defaults som alltid er nødvendige for å bygge og publisere webserver-applikasjoner i .NET.

```diff filename="MyApiApp.csproj" copy
+<Project Sdk="Microsoft.NET.Sdk.Web">
   <PropertyGroup>
     <TargetFramework>net7.0</TargetFramework>
     <Nullable>enable</Nullable>
     <ImplicitUsings>enable</ImplicitUsings>
   </PropertyGroup>
 </Project>
```

**Program.cs**

Entry-filen bygger opp en web-app host som inneholder en del defaults, og konfigurerer en route på rot ("/").

_Program.cs_

```csharp filename="Program.cs" copy
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

### Kjøre appen

Bruk .NET SDKet (`dotnet run`) i samme mappe som prosjektfilen.

```bash
dotnet run
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5049
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /Users/johnkors/kode/personlige/dotnet-intro/samples/MyApiApp
```

Loggingen til console kommer av de nevnte defaultsene i `Program.cs`, og er en del av oppsettet som skjer i `WebApplication.CreateBuilder`.
Appen skal da være tilgjengelig på http://localhost:5049, evt det portnummeret som er definert i din `launchSettings.json`.
