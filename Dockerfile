FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto para habilitar el cacheo de restore
COPY ["StoreModelo.API/StoreModelo.API.csproj", "StoreModelo.API/"]
COPY ["Store.Model/Store.Model.csproj", "Store.Model/"]

RUN dotnet restore "StoreModelo.API/StoreModelo.API.csproj"

# Copiar todo el repositorio y publicar la app
COPY . .
WORKDIR /src/StoreModelo.API
RUN dotnet publish "StoreModelo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "StoreModelo.API.dll"]
