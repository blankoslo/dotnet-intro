# C# Basics

## Typer

Kan defineres enten som `record` , `class`

```csharp
// C# records
public record Pokemon(string Name, Sprites Sprites);
public record Sprites(string Front_Default, string Back_Default);
```

```csharp
// C# class
public class Pokemon
{
    public string? Name { get; set; }
    public Sprites Sprites { get; set; }
}

public class Sprites
{
    public string? Front_Default { get; set; }
    public string? Back_Default { get; set; }
}
```

## Instansiering av objekter

```csharp
// C# records
var pokemon = new Pokemon("charmander", new Sprites("https://…", "https://…"));
```

For klasser kan man benytte [Object initializer syntax](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers):

```csharp
// C# class
var pokemon = new Pokemon
{
    Name = "charmander",
    Sprites = new Sprites
    {
        Front_Default = "https://…",
        Back_Default = "https://…"
    }
};
```

## Collections

Lister og andre typer collections støtter og _Object initializer_-syntaksen:

```csharp
  var list = new List<string>();
  list.Add("Hello");
  list.Add("Goobye");
  list.Add("Hi");
```

```csharp
var list = new List<string>
{
    "Hello",
    "Goobye",
    "Hi"
};
```

### Filtering & mapping

LINQ er en samling av metoder for å iterere, filtrere og collections med en funksjonell syntaks.

Gitt:

```csharp
var list = new List<string>
{
    "Hello",
    "Goobye",
    "Hi"
};
```

```csharp
var filtered = new List<string>();
foreach (var item in list)
{
    if (item.StartsWith("H"))
    {
        filtered.Add(item.ToUpper());
    }
}
```

… ser slik ut med bruk av LINQ:

```csharp
// Extension methods
var filtered = list.Where(s => s.StartsWith("H")).Select(s => $"{s.ToUpper()}");

// Query syntax
var filtered = from s in list where s.StartsWith("H") select s.ToUpper();
```
