using System.Text.Json;

using MyWorkerApp;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureLogging(logging =>
{
    logging.AddJsonConsole(o => o.JsonWriterOptions = new JsonWriterOptions { Indented = true });
});
builder.ConfigureServices((ctx, services) =>
{
    var crouperConfig = ctx.Configuration.GetRequiredSection("Croupier").Get<CrouperConfig>()!;
    services.AddSingleton(crouperConfig);
    services.AddSingleton<IRollDice, RandomDiceRoller>();
    services.AddSingleton<INotifyToSomeWhere, Croupier>();
    services.AddHostedService<Worker>();
});
var host = builder.Build();

host.Run();
