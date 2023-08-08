# 🐸 Dependency Injection

En .NET app kommer med en DI container. Registreringer skjer via `Add…` metodene:

For console apps:

```diff filename="Program.cs" copy
var builder = Host.CreateDefaultBuilder(args);
+builder.ConfigureServices(services =>
+{
+    services.AddSingleton<INotifyToSomeWhere, MyService>();
+});
```

For web apps:

```diff filename="Program.cs" copy
var builder = WebApplication.CreateBuilder(args);
+builder.Services.AddSingleton<INotifyToSomeWhere, MyService>();

var app = builder.Build();

// Injection av en registrert type:
app.MapGet("/", (INotifyToSomeWhere service) => service.DoStuff());
```

Dersom `MyService` også har avhengigheter, så må disse også registreres i DI-containeren.

> [!NOTE]
> Les mer om DI [på microsoft.com](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
