# React + .NET API i samme host

Dette prosjektet blir laget med `dotnet new react` og viser et eksempel på hvordan man kan co-hoste en React SPA-applikasjon sammen med et .NET HTTP API i _samme_ fysiske host. Webserveren i .NET hoster da to komponenter:

1. Et .NET HTTP API
2. Statiske filer (React-appen)

Denne mappingen sørger for alle requests som ikke matcher en API route i backend'en blir sendt til React-appen:

```csharp
app.MapFallbackToFile("index.html");
```

> [!WARNING]
> Dette oppsettet er unødvendig kronglete å jobbe med. Anbefales ikke, og hosting av statiske filer er uansett "gratis".
