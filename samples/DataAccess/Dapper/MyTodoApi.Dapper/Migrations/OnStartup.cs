using System.Data;
using Dapper;

public static class OnStartup
{
    public static void MigrateDb(IServiceProvider services, string? connStr)
    {
        var startupLogger = services.GetRequiredService<ILogger<Program>>();

        if (connStr is not null)
        {
            startupLogger.LogInformation("Dapper: Using Postgres as db with {Connection} ", connStr);
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetService<IDbConnection>();

            var createTables = """
                                CREATE TABLE IF NOT EXISTS Todos (
                                 Id  SERIAL PRIMARY KEY,
                                 Name TEXT NOT NULL,
                                 IsComplete BOOLEAN DEFAULT False NOT NULL
                                )
                                """;
            db.Execute(createTables);

            var todos = db.ExecuteScalar<int>("select count(*) from Todos;");
            if (todos > 0)
            {
                return;
            }

            startupLogger.LogWarning("EMPTY DB. Seeding Todos!");

            Seed(db, "Use Dapper", false);
            Seed(db, "Learn Postgres", true);
            Seed(db, "Write Dockerfiles", false);
            Seed(db, "Deploy to the Cloud!", false);
        }

        void Seed(IDbConnection? db, string name, bool isComplete)
        {
            db.Execute("insert into Todos (Name, IsComplete) values (@Name, @IsComplete)",
                new { Name = name, IsComplete = isComplete });
        }
    }
}
