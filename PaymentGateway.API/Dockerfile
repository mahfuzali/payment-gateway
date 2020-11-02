FROM mcr.microsoft.com/dotnet/core/sdk

LABEL author="Mahfuz Ali"

ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /app

COPY . .

EXPOSE 5000

ENTRYPOINT ["/bin/bash", "-c", "dotnet restore && dotnet run"]