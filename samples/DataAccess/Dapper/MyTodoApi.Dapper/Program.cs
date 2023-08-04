using System.Data;
using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddScoped<IDbConnection, NpgsqlConnection>(_ => new NpgsqlConnection(connStr));

var app = builder.Build();

OnStartup.MigrateDb(app.Services, connStr);

var group = app.MapGroup("/todos");

group.MapGet("/", async (string? complete, IDbConnection db) =>
{
    if (complete is not null)
    {
        var res = await db.QueryAsync<Todo>("select * from Todos where IsComplete=@IsComplete", new { IsComplete = true })!;
        return Results.Ok(res.ToArray());
    }

    var allResults = await db.QueryAsync<Todo>("Select * from Todos")!;
    return Results.Ok(allResults.ToArray());
});

group.MapGet("/{id}", async (int id, IDbConnection db) =>
{
    Todo? todo = await db.QuerySingleOrDefaultAsync<Todo>("select * from Todos where Id=@Id", new { Id = id })!;
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});


group.MapPost("/", async (Todo todo, IDbConnection db) =>
{
    int res = await db.ExecuteScalarAsync<int>("insert into Todos (Name, IsComplete) values (@Name, @IsComplete) RETURNING id", todo)!;
    todo.Id = res;
    return Results.Created($"/todo/{res}", todo);
});

group.MapPut("/{id}", async (int id, Todo inputTodo, IDbConnection db) =>
{
    Todo? todo = await db.QuerySingleOrDefaultAsync<Todo>("select * from Todos where Id=@Id", new { Id = id })!;
    if (todo is null)
    {
        return Results.NotFound();
    }

    await db.ExecuteAsync("update Todos set Name = @Name, IsComplete=@IsComplete where Id=@Id",
        new {
            Id = id,
            Name = inputTodo.Name,
            IsComplete = inputTodo.IsComplete
        })!;
    return Results.NoContent();
});

group.MapDelete("/{id}", async (int id, IDbConnection db) =>
{
    Todo? todo = await db.QuerySingleOrDefaultAsync<Todo>("select * from Todos where Id=@Id", new { Id = id })!;
    if (todo is not null)
    {
        await db.ExecuteAsync("delete from Todos where Id=@Id", new { Id = id })!;
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();

public partial class Program { }
