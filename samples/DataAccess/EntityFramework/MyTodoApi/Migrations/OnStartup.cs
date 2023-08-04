using Microsoft.EntityFrameworkCore;

public static class OnStartup
{
    public static void MigrateDb(IServiceProvider services, string? connStr)
    {
        var startupLogger = services.GetRequiredService<ILogger<Program>>();

        if (connStr is not null)
        {
            startupLogger.LogInformation("Using Postgres as db with {Connection}", connStr);
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Data>();

            db.Database.Migrate();

            if (db.Todos.Any())
            {
                return;
            }

            startupLogger.LogWarning("EMPTY DB. Seeding Todos!");

            db.Todos.Add(new Todo { Name = "Write C#", IsComplete = false });
            db.Todos.Add(new Todo { Name = "Read up on .NET", IsComplete = false });
            db.Todos.Add(new Todo { Name = "Watch videos on ASP.NET", IsComplete = true });
            db.Todos.Add(new Todo { Name = "Use Entity Framework in an app", IsComplete = false });
            db.SaveChanges();
        }
        else
        {
            startupLogger.LogInformation("Using in-mem db");
        }
    }
}
