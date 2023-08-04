# Oppgave 1 - Hello, Charmander!

<p align="center">
<img src=https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/4.png width=200 />
</p>

Denne oppgaven er for å bli kjent med ulike primitiver i .NET.

### 1.1 Hent bilder av Charmander

- [ ] Lag en `console` app ved hjelp av CLI-et (`dotnet new`)
- [ ] Få appen til å hente og lagre to bilder av _Charmander_ til en folder på disk

```
images
└── charmander
    ├── front_default.png
    └── back_default.png
```

Pokemon API docs 👉 https://pokeapi.co 👈

<details>
  <summary>Hint 1 (http)</summary>

---

Bruk [`HttpClient`](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient) for å gjøre HTTP-kall:

```diff
+using System.Net.Http;

var httpClient = new HttpClient();
HttpResponseMessage response = await httpClient.GetAsync(url);
```

---

</details>

<details>
  <summary>Hint 2 (helpers)</summary>

---

> [!NOTE] > `using System.Net.Http.Json` gir `HttpClient` ekstra metoder for å håndtere JSON i `GetFromJsonAsync()`

```diff
using System.Net.Http;
+using System.Net.Http.Json;

var httpClient = new HttpClient();
+Something myThing = await httpClient.GetFromJsonAsync<Something>(url);
```

---

</details>

<details>
  <summary>Hint 3 (filer)</summary>

---

`GetByteArrayAsync` gir en `byte[]`, som kan skrives til fil:

```csharp
byte[] bytes = await httpClient.GetByteArrayAsync(url);
File.WriteAllBytes(fileName, bytes);
```

---

</details>

### 1.2 Argumenter

- [ ] Refaktorer appen til å ta inn et argument fra kommandolinjen som sier hvilken pokemon som skal hentes ned

Eks:

```
$ dotnet run bulbasaur pidgey squirtle
```

skal gi følgende filstruktur:

```
images
├── bulbasaur
│   ├── front_default.png
│   └── back_default.png
├── pidgey
│   ├── front_default.png
│   └── back_default.png
└── squirtle
    ├── front_default.png
    └── back_default.png
```

### 1.3 Bytt til et bibliotek

- [ ] Installer Pokemon SDK: 👉 https://github.com/mtrdp642/PokeApiNet 👈
- [ ] Bruk SDKet til å hente Pokemon-data

<details>
  <summary>Hint: (installere avhengigheter)</summary>

```
dotnet add package PokeApiNet
```
</details>
