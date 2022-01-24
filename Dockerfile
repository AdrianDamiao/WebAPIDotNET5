FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
    
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MeuApp/MeuApp.csproj", "MeuApp/"]
RUN dotnet restore "MeuApp/MeuApp.csproj"
COPY ./MeuApp ./MeuApp
WORKDIR "/src/MeuApp"
RUN dotnet build "MeuApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeuApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:5000" dotnet MeuApp.dll