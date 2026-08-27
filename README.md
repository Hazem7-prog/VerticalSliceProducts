# Create Product - Vertical Slice Architecture

A small ASP.NET Core Web API demonstrating one CreateProduct feature organized
using Vertical Slice Architecture.

## Project structure

The feature is located at:

src/ProductApi/Features/Products/CreateProduct/

It contains:

- CreateProductController.cs
- CreateProductRequest.cs
- CreateProductResponse.cs
- CreateProductService.cs

The controller, contracts, validation rules, business logic, and feature-owned
model are kept together. This is the main Vertical Slice idea: organize code by
business feature instead of spreading it across global Controllers, Services,
and DTOs folders.

## Run

Requirements: .NET 8 SDK.

From the VerticalSliceProducts directory, run:

    dotnet restore
    dotnet run --project src/ProductApi/ProductApi.csproj

The API runs at https://localhost:7147 and opens Swagger automatically.
The HTTP profile is also available at http://localhost:5072.

## Endpoint

POST /api/products

Example request body:

    {
      "name": "Mechanical Keyboard",
      "description": "Wireless mechanical keyboard",
      "price": 2499.99,
      "stockQuantity": 15
    }

A valid request returns 201 Created. Invalid input returns 400 Bad Request, and
a duplicate product name returns 409 Conflict.

Open ProductApi.http in Visual Studio or Rider to test all three cases.
You can also use Swagger at https://localhost:7147/swagger.

Products are stored in memory because the task focuses on feature organization.
Data resets whenever the application restarts.
