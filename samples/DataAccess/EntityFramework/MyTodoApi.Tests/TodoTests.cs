using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace MyTodoApi.Tests;

public class TodoTests
{
    private readonly WebApplicationFactory<Program> _factory = new();

    [Fact]
    public async Task PostReturnsCreated()
    {
        var client = _factory.CreateClient();

        var res = await client.PostAsJsonAsync("/todos", new { name = "John" });

        var newTodo = await res.Content.ReadFromJsonAsync<Todo>();
        Assert.Equal(HttpStatusCode.Created, res.StatusCode);

        Assert.NotEqual(0, newTodo!.Id);
        Assert.Equal("John", newTodo.Name);
        Assert.False(newTodo.IsComplete);
    }

    [Fact]
    public async Task PutUpdates()
    {
        var client = _factory.CreateClient();

        var postRes = await client.PostAsJsonAsync("/todos", new { name = "John" });
        var newTodo = await postRes.Content.ReadFromJsonAsync<Todo>();
        var putRes = await client.PutAsJsonAsync($"/todos/{newTodo!.Id}", new
        {
            name = "George",
            isComplete = true
        });

        Assert.Equal(HttpStatusCode.NoContent, putRes.StatusCode);

        var todo = await client.GetFromJsonAsync<Todo>($"/todos/{newTodo.Id}");
        Assert.Equal("George", todo!.Name);
        Assert.True(todo.IsComplete);
    }

    [Fact]
    public async Task DeleteRemoves()
    {
        var client = _factory.CreateClient();

        var postRes = await client.PostAsJsonAsync("/todos", new { name = "John" });
        var newTodo = await postRes.Content.ReadFromJsonAsync<Todo>();
        var deleteRes = await client.DeleteAsync($"/todos/{newTodo!.Id}");

        Assert.Equal(HttpStatusCode.OK, deleteRes.StatusCode);
    }

    [Fact]
    public async Task GetByCompleteQuery()
    {
        var client = _factory.CreateClient();

        await CreateNew("John");
        Todo newTodo2 = await CreateNew("George");

        await client.PutAsJsonAsync($"/todos/{newTodo2!.Id}", new
        {
            name = newTodo2.Name,
            isComplete = true
        });

        var completedTodos = await client.GetFromJsonAsync<Todo[]>($"/todos?complete");
        Assert.Single(completedTodos!);

        var completedTodo = completedTodos!.First();
        Assert.Equal(newTodo2.Id, completedTodo.Id);
        Assert.Equal("George", completedTodo.Name);

        async Task<Todo> CreateNew(string name)
        {
            var postRes = await client.PostAsJsonAsync("/todos", new { name });
            var newTodo = await postRes.Content.ReadFromJsonAsync<Todo>();

            return newTodo!;
        }
    }
}
