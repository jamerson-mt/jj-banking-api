# Estágio de Build (SDK do .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copia os arquivos .csproj e restaura as dependências
COPY ["src/JJBanking.API/JJBanking.API.csproj", "src/JJBanking.API/"]
COPY ["src/JJBanking.Domain/JJBanking.Domain.csproj", "src/JJBanking.Domain/"]
COPY ["src/JJBanking.Infra/JJBanking.Infra.csproj", "src/JJBanking.Infra/"]
RUN dotnet restore "src/JJBanking.API/JJBanking.API.csproj"

# Copia o restante do código e publica
COPY . .
RUN dotnet publish "src/JJBanking.API/JJBanking.API.csproj" -c Release -o /out

# Estágio de Execução (Runtime do .NET 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out .

# Porta padrão do ASP.NET Core no container
EXPOSE 8080
ENTRYPOINT ["dotnet", "JJBanking.API.dll"]