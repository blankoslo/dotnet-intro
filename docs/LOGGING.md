# 🖋️ Logging

Ut av boksen vil en .NET app logge til stdout (aka `Console`), men det finnes flere bilbiotek og konfigurasjonsmetoder dersom man ønsker å sende dette til en annen plass.

For console apps:

```diff filename="Program.cs" copy
var builder = Host.CreateDefaultBuilder(args);
+builder.ConfigureLogging(logging => logging.AddJsonConsole());
```

For web apps:

```diff filename="Program.cs" copy
var builder = WebApplication.CreateBuilder(args);
+builder.Logging.AddJsonConsole();
```

> [!NOTE]
> Les mer om logging på [microsoft.com](https://learn.microsoft.com/>en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0)
