# HTTP

Importer relevante namespaces for `HttpClient`:

```csharp
using System.Net.Http; // HttpClient
using System.Net.Http.Json; // Extension-metoder for HttpClient
using System.Text.Json; // JsonSerializer
```

### DYI

```csharp
var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/charmander");
var body = await response.Content.ReadAsStringAsync();
var pokemon1 = JsonSerializer.Deserialize<Pokemon>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));
```

### Lese body som string:

```csharp
var httpClient = new HttpClient();
var resString = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon/charmander");
var pokemon2 = JsonSerializer.Deserialize<Pokemon>(resString, new JsonSerializerOptions(JsonSerializerDefaults.Web));
```

### Json-Helpers:

```csharp
var httpClient = new HttpClient();
var pokemon3 = await httpClient.GetFromJsonAsync<PokemonClass>("https://pokeapi.co/api/v2/pokemon/charmander");
```

# Filer

```csharp
var httpClient = new HttpClient();
var pokemon3 = await httpClient.GetFromJsonAsync<PokemonClass>("https://pokeapi.co/api/v2/pokemon/charmander");


var fileInfo = new FileInfo(pokemon3.Sprites.Front_Default!);
var frontImg = $"{nameof(pokemon3.Sprites.Front_Default).ToLower()}{fileInfo.Extension}";
var backImg = $"{nameof(pokemon3.Sprites.Back_Default).ToLower()}{fileInfo.Extension}";


var dir = $"./images/{pokemon3.Name}";
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
else
{
    Directory.Delete("./images", true);
    Directory.CreateDirectory(dir);
}

var bytes = await httpClient.GetByteArrayAsync(pokemon3.Sprites.Front_Default);
File.WriteAllBytes($"{dir}/{frontImg}", bytes);
File.WriteAllBytes($"{dir}/{backImg}", bytes);
```

# Command line args

```csharp
using System.Net;
using System.Net.Http.Json;

var httpClient = new HttpClient();

foreach (var arg in args)
{
    Console.WriteLine($"Handling arg '{arg}'");
    var p = await GetPokemon(arg);
    if (p is not null)
    {
        await WriteImages(p);
    }
}

async Task<PokemonClass?> GetPokemon(string pokemonName)
{
    var url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";
    try
    {
        return await httpClient.GetFromJsonAsync<PokemonClass>(url);
    }
    catch (HttpRequestException hre) when (hre is { StatusCode: HttpStatusCode.NotFound })
    {
        Console.WriteLine($"Skipping! Could not find pokemon '{pokemonName}'");
        return null;
    }
}

async Task WriteImages(PokemonClass pokemon)
{
    var dir = $"./images/{pokemon.Name}";
    if (!Directory.Exists(dir))
    {
        Directory.CreateDirectory(dir);
    }

    await Write(pokemon.Sprites.Front_Default!, nameof(pokemon.Sprites.Front_Default).ToLower());
    await Write(pokemon.Sprites.Back_Default!, nameof(pokemon.Sprites.Back_Default).ToLower());

    async Task Write(string url, string filename)
    {
        var fileInfo = new FileInfo(url);
        var img = $"{filename}{fileInfo.Extension}";
        var bytes = await httpClient.GetByteArrayAsync(url);
        File.WriteAllBytes($"{dir}/{img}", bytes);
    }
}
```
