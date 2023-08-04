namespace MyWorkerApp;

public class Worker : BackgroundService
{
    private readonly INotifyToSomeWhere _notifier;
    private readonly IRollDice _diceRoller;

    public Worker(INotifyToSomeWhere notifier, IRollDice diceRoller)
    {
        _notifier = notifier;
        _diceRoller = diceRoller;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var diceRoll = _diceRoller.Roll();
            FormattableString msg = $"Hello, lucky number {diceRoll}!";
            await _notifier.NotifyAsync(msg, stoppingToken);
            await Task.Delay(2000, stoppingToken);
        }
    }
}

public interface IRollDice
{
    int Roll();
}

public class RandomDiceRoller : IRollDice
{
    public int Roll()
    {
        return new Random().Next(1, 6);
    }
}

public interface INotifyToSomeWhere
{
    Task NotifyAsync(FormattableString msg, CancellationToken token);
}


public record CrouperConfig(string Name);

public class Croupier : INotifyToSomeWhere
{
    private readonly CrouperConfig _croupierConfig;
    private readonly ILogger<Croupier> _logger;

    public Croupier(CrouperConfig croupierConfig, ILogger<Croupier> logger)
    {
        _croupierConfig = croupierConfig;
        _logger = logger;
    }

    public Task NotifyAsync(FormattableString msg, CancellationToken token)
    {
        var args = msg.GetArguments();
        var firstArg = args.First(arg => arg is int);
        _logger.LogInformation("Croupier: {CroupierName}. Shoutout: {CroupierMessage} DiceRoll: {DiceRoll}. Notified at {TimeOfNotification})", _croupierConfig.Name, msg, firstArg, DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss"));
        return Task.CompletedTask;
    }
}
