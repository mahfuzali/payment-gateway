FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/BankSimulator/BankSimulator.csproj", "src/BankSimulator/"]
RUN dotnet restore "src/BankSimulator/BankSimulator.csproj"
COPY . .
WORKDIR "/src/src/BankSimulator"
RUN dotnet build "BankSimulator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankSimulator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankSimulator.dll"]
