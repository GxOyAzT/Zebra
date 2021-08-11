# Zebra
Zebra is an ecommerce app.

## ProductService
Product Service is microservice responsible for managing product data and it's price. Based on onion architecture contains four layers:
* `API` 
* `Application` layer is created using `Mediator` design pattern and implemented using `MediatR` nuget.
* `Persistance` layer is created using `Repositories` for each domain model and contains database provider (`DbContext` `EF Core`)
* `Domain` contains `domain models` and `exceptions`

#### Fuctionality 
* Manage product base info
* Update product price
* Add user review for product


## Logger and LoggerDriver

#### Logger
Logger is simple ASP.NET Core web API created using monolith architecture. It has one functionality: receiving logs via `RabbitMQ` and saving them into simple table with `SQLite`.

#### LoggerDriver
Logger driver is nuget which is easy to register in DI container in each microservices. It's responsible for sending logs into RabbitMQ queue.