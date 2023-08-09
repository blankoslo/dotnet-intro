# 🐳 Docker

|         | Image                              |
| ------- | ---------------------------------- |
| SDK     | _mcr.microsoft.com/dotnet/sdk_     |
| Runtime | _mcr.microsoft.com/dotnet/runtime_ |

Det finnes også enkelte images optimalisert for f.eks. web (ASP.NET Core):

|              | Image                                 |
| ------------ | ------------------------------------- |
| ASP.NET Core | _mcr.microsoft.com/dotnet/aspnetcore_ |

> [!NOTE]
> Microsoft sine offisielle images var tidligere på Docker Hub, men er nå flyttet på Microsoft sitt eget registry på mcr.microsoft.com. De har likevel en konto for synlighetens skyld: https://hub.docker.com/_/microsoft-dotnet

### Eksempler

`Dockerfile` som bygger og publiserer en ASP.NET Core app, vha [multistage builds](https://docs.docker.com/build/building/multi-stage/) for minst mulig output image:

```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "MyProject.dll"]
```

# .NET SDK: Container support

Man kan også [lage containers _uten_ en Dockerfile](https://devblogs.microsoft.com/dotnet/announcing-builtin-container-support-for-the-dotnet-sdk/). Følgende `dotnet publish` bygger og publiserer et image til en lokal Docker daemon, hvor SDKet finner ut av relevante images – helt uten å måtte vedlikeholde en Dockerfile.

```
$ dotnet publish -p:PublishProfile=DefaultContainer

MSBuild version 17.7.1+971bf70db for .NET
  Determining projects to restore...
  Restored /Users/johnkors/kode/blank/dotnet-intro/samples/MyApiApp/MyApiApp.csproj (in 101 ms).
  MyApiApp -> /Users/johnkors/kode/blank/dotnet-intro/samples/MyApiApp/bin/Debug/net7.0/MyApiApp.dll
  MyApiApp -> /Users/johnkors/kode/blank/dotnet-intro/samples/MyApiApp/bin/Debug/net7.0/publish/
  Building image 'myapiapp' with tags 1.0.0 on top of base image mcr.microsoft.com/dotnet/aspnet:7.0
  Pushed container 'myapiapp:1.0.0' to local daemon

$ docker images | grep myapi

myapiapp                                      1.0.0         4b7278994041   About a minute ago   212MB
```

Det er og mulig [å customize & parameterisere ting via msbuild properties](https://github.com/dotnet/sdk-container-builds/blob/main/docs/ContainerCustomization.md#unsupported-properties), f.eks tag via `ContainerImageTags`:

```bash
 dotnet publish -p:PublishProfile=DefaultContainer -p:ContainerImageTags='"1.2.3-alpha2;latest"'
```

evt:

```diff
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
+   <ContainerImageTags>1.2.3-alpha2;latest</ContainerImageTags>
  </PropertyGroup>

</Project>
```

> [!WARNING]
> Har man mer komplekse bygg, så er kanskje ikke dette en god løsning.

> [!WARNING]
> Støtter foreløpig kun Linux-x64 containers (ingen Windows eller ARM).
