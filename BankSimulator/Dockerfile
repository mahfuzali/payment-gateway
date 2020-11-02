FROM mcr.microsoft.com/dotnet/core/sdk

LABEL author="Mahfuz Ali"

ENV ASPNETCORE_URLS=http://+:5050

WORKDIR /app

COPY . .

EXPOSE 5050

ENTRYPOINT ["/bin/bash", "-c", "dotnet restore && dotnet run"]