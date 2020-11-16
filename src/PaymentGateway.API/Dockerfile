FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PaymentGateway.API/PaymentGateway.API.csproj", "src/PaymentGateway.API/"]
COPY ["src/PaymentGateway.Infrastructure/PaymentGateway.Infrastructure.csproj", "src/PaymentGateway.Infrastructure/"]
COPY ["src/PaymentGateway.Application/PaymentGateway.Application.csproj", "src/PaymentGateway.Application/"]
COPY ["src/PaymentGateway.Domain/PaymentGateway.Domain.csproj", "src/PaymentGateway.Domain/"]

RUN dotnet restore "src/PaymentGateway.API/PaymentGateway.API.csproj"
COPY . .
WORKDIR "/src/src/PaymentGateway.API"
RUN dotnet build "PaymentGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.API.dll"]
