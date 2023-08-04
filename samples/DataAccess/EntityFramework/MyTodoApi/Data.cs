using Microsoft.EntityFrameworkCore;

public class Data : DbContext
{
    public Data(DbContextOptions<Data> options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}
