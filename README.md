[![Actions Status](https://github.com/mahfuzali/payment-gateway/workflows/.NET%20Core/badge.svg)](https://github.com/mahfuzali/payment-gateway/actions)

# Payment Gateway

### About 
An API based application that will allow a merchant to offer a way for their shoppers to pay for their product.

Shopper -> Merchant -> Payment Gateway -> Bank
![alt text](online-payment-processes.png)
### Technologies:
- .NET 5
- PostgreSQL 
- Entity Framework Core 5
- Automapper
- Docker

### Prerequisite

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)  
2. Install the latest [PostgreSQL](https://www.postgresql.org/download/)
3. Install the latest [pgAdmin 4](https://www.pgadmin.org/download/)
3. If you want to run the application on Docker, then install the latest [Docker](https://www.docker.com/products/docker-desktop)  

#### Docker Configuration
To run the application on docker, you will need to add a temporary SSL cert and mount a volume to hold that cert.
You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https) that describe the steps required for each platform.

#### PaymentGateWay.API

This project is a .NET Core Web API that process a payment through the payment gateway. When a shopper makes a call to the merchant it subsequently makes a call to the acquiring bank to obtain the payment with unique id and status (successful or declined).

For the purpose of testing, this payment gateway makes a call to the simulated bank endpoint (see `BankSimulator` project). In production, this endpoint can be changed to actual bank endpoint. Just go to the project's *appsetting.json* and change the following part:
  
  ```console
    "Endpoints": {
      "Bank": "http://localhost:5050/api/FakeBank"
    }
  ```

To run this project, navigate to the project directory and run the following command:
```console
  > dotnet build
  > dotnet run
```
Open the browser: `https://localhost:5001/swagger/index.html`

#### BankSimulator
This project simulates a fake bank for testing purposes. When a merchant make a call, it returns a unique id and status i.e. successful or declined.

To run this project, navigate to the project directory and run the following command:
```console
  > dotnet build
  > dotnet run
```
Open the browser: `http://localhost:5050`


#### Note: In order for payment gateway to run, bank simulator also needs to be run at the same time.



#### Docker implementation
To run the application on Docker, run the following commands:

```console
  > docker-compose build
  > docker-compose up
```

#### Future improvements
* Obtaining application metrics using tool such as, Prometheus and Grafana
* Docker containerization and kubernetes orchestration
* Continous deployment to cloud environment i.e. AWS, GCP or Azure
* API authentication using OpenID Connect or OAuth 2.0 
* Load/stress testing using Gatling or Locust
* Encryption using asymmetric encryption mechanism
* Data Storage using Postgresql or MongoDB
* Apache Kafka and Spark intergration for transation streaming and processing


## License

(c) All right reserved. Mahfuz Ali
