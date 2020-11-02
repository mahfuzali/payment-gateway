# payment-gateway

## About 
An API based application that will allow a merchant to offer a way for their shoppers to pay for their product.

Shopper -> Merchant -> Payment Gateway -> Bank

### PaymentGateWay.API
This project is a .NET Core Web API that process a payment through the payment gateway. When a shopper makes a call to the merchant it subsequently makes a call to the acquiring bank to obtain the payment with unique id and status (successful or declined).

For the purpose of testing, it makes a call to the BankSimulator endpoint. In production, this endpoint can be changed to real bank endpoint. Just go to the project's *appsetting.json* and change the following part:
  
  `"Endpoints": {
    "Bank": "http://localhost:5050/api/FakeBank"
  }`

To run this project, navigate to the project directory and run the following command:
`dotnet run`
* Open the browser: `http://localhost:5000`

### BankSimulator
This project simulates a fake bank that for testing purposes. When a merchant make a call to it returns a unique id and status (successful or declined).

To run this project, navigate to the project directory and run the following command:
`dotnet run`
* Open the browser: `http://localhost:5050`


### Future improvement
* Obtaining application metrics using tool such as, Prometheus and Grafana
* Docker containerization and kubernetes orchestration
* Continous deployment to cloud environment i.e. AWS, GCP or Azure
* API authentication using OpenID Connect or OAuth 2.0 
* Load/stress testing using Gatling or Locust
* Encryption using asymmetric encryption mechanism
* Data Storage using Postgresql or MongoDB
* Apache Kafka and Spark intergration for transation streaming and processing
