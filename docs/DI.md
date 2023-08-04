# 🐸 Dependency Injection

En .NET app kommer med en DI container. Registreringer skjer via `AddX` metodene:

For console apps:

```diff filename="Program.cs" copy
var builder = Host.CreateDefaultBuilder(args);
+builder.ConfigureServices(services =>
+{
+    services.AddSingleton<INotifyToSomeWheere, MyService>();
+    services.AddHostedService<Worker>();
+});
```

For web apps:

```diff filename="Program.cs" copy
var builder = WebApplication.CreateBuilder(args);
+builder.Services.AddSingleton<INotifyToSomeWheere, MyService>();
```

> [!NOTE]
> Les mer om DI [på microsoft.com](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
