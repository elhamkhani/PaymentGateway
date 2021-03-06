# Payment Gateway

This project is an API based application that will allow a
merchant to offer away for their shoppers to pay for their product.
 [Repository](https://github.com/elhamkhani/PaymentGateway).

1. A merchant is able to process a payment through the payment gateway and
receive either a successful or unsuccessful response
2. A merchant is able to retrieve the details of a previously made payment


Where full URLs are provided in responses they will be rendered as if service
is running on 'http://localhost:6600/' (Swagger documentation available).

## Endpoints

Endpoints for processing a payment and retrieving the details of a previously made payment.

* Pay : `POST /v1/api/pay/`
* Retrieve : `GET /v1/api/retrieve/{identifier}`

## Postman collection

Import following collection to postman:
* [Postman collection](postman/PaymentGateway.postman_collection.json) 

## Bank Mock server

In postman collection there is a mock server that will return two statuses(successful or failed) from bank server.
The Bank Server Endpoint should be set in appsettings.json file.

## CosmosDB 

The payment records get saved in cosmos DB. 
To run the application locally, use [Azure Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=cli%2Cssl-netstd21)
Account and Key should be set in appsettings.json file.

## Other features would have done if i had more time
* Data encryption: using punlic and private keys to sign each request
* Authentication and Authorization: 
** Each merchant should authenticate using login endpoint and receive a token which passes in the header of the following requests.
** Each merchant should be able to retrieve only its own payments.
