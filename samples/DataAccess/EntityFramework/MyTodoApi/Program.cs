using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddDbContext<Data>((ctx, opt) =>
{
    if (connStr is not null)
    {
        opt.UseNpgsql(connStr);
    }
    else
    {
        opt.UseInMemoryDatabase("Todo");
    }
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

OnStartup.MigrateDb(app.Services, connStr);

var group = app.MapGroup("/todos");

group.MapGet("/", async ([FromQuery] string? complete, Data db) =>
{
    if (complete is not null)
    {
        return await db.Todos.Where(t => t.IsComplete).ToListAsync();
    }

    return await db.Todos.ToListAsync();
});

group.MapGet("/{id}", async (int id, Data db) =>
{
    Todo? todo = await db.Todos.FindAsync(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});


group.MapPost("/", async (Todo todo, Data db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todo/{todo.Id}", todo);
});

group.MapPut("/{id}", async (int id, Todo inputTodo, Data db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

group.MapDelete("/{id}", async (int id, Data db) =>
{
    if (await db.Todos.FindAsync(id) is { } todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();

public partial class Program { }
