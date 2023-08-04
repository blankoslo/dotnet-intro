# Avhengigheter

Avhengigheter kalles litt forskjellige ting avh av scenario. "Shared libraries" er enten _Class libraries_ eller _NuGets_ avhengig om de er interne eller eksterne.

1. internt: En applikasjon/prosjekt har en "Project Reference" til et _Class library_.
2. eksternt: En applikasjon/prosjekt har en "Package Dependency" til en _Nuget-pakke_.

En offentlig _NuGet_-pakke kan installeres fra en public feed, f.eks. [nuget.org](https://www.nuget.org) (www.npmjs.com for .NET).

## Installere en offentlig pakke

Installere en pakke (her: `Newtonsoft.Json`) fra en public feed:

```bash copy
dotnet add package Newtonsoft.Json
```

Kommandoen vil endre csproj med et nytt entry som peker på pakken:

```diff filename="MyConsoleApp.csproj" copy
  <ItemGroup>
+    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>
```
