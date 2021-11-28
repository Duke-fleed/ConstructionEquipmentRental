# Project

Construction equipment rental backend is ASP.NET Core web API project developed with C# 10 and .NET 6.

## Build and Test

`dotnet build` and `dotnet test` commands can be used in this directory to build and unit test the project.

To run the API separately with `dotnet run`, go to `.\Source\App\ConstructionEquipmentRental.Api\` directory and run the command from there.

API can be explored with `\swagger` endpoint, when run in development mode.

To run only API with docker, build the image with the Dockerfile located in this directory and run.

## Project structure

There are three projects in the solution:

* **ConstructionEquipmentRental.API** - executable API layer, with controllers and all API specific logic.
* **ConstructionEquipmentRental.Services** - Class library containing most of the domain logic and request handlers.
* **ConstructionEquipmentRental.Persistent.InMemory** - Project containing persistence logic (only in memory in this case).

API uses [MediatR](https://github.com/jbogard/MediatR) to communicate with the service layer. MediatR is simple request/response library for .NET which also adds pipeline behaviors to the API. Each interaction with service layer is done with `Request` objects that have corresponding `RequestHandlers`.

## Persistence

No actual persistency is implemented in the API and everything is stored in memory. To demonstrate how persistance layer can be changed easily and switched to the actual database, there is separate project `ConstructionEquipmentRental.Persistent.InMemory` which implements interface from `Services` layer. In case of need it can be switched with any database specific project e.g. `ConstructionEquipmentRental.Persistent.MSSQL` etc. and only change required in the existing code would be to update `program.cs` with new DI configuration.

Inventory list and prices for diferent types of fees are provided as a configuration values from `appsettings.json` file.

## Logging

Console logging is enabled and all `Information` level logs are logged by default, including ASP.NET Core request logging, which logs all incoming requests and their responses.





