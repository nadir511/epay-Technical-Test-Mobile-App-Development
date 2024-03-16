This repository consists of two .NET/C# projects. One is the console application(with the name TaskOne_ATM) and the other project is .NET Core REST API(with the name CustomerRESTapi).

# ATM Console Application

This console application simulates an ATM machine and calculates combinations of payouts for various amounts.

## Usage

1. Clone the repository.
2. Navigate to the `ATM_Console` directory.
3. Build the application using your preferred .NET Core build method (`dotnet build`, Visual Studio, etc.).
4. Run the application (`dotnet run`).

## Functionality

- The application calculates combinations of payouts for given amounts using denominations of 10, 50, and 100 euros.
- It displays the possible payout denominations for each amount entered by the user.
- The application supports multiple amounts in one run, displaying the payout denominations for each amount.
# Customer API Project

This project implements a RESTful API for managing customer data.

## Features

- Supports POST requests to add new customers to the system.
- Validates customer data, ensuring that all required fields are provided, the age is above 18, and the ID is unique.
- Sorts customers by last name and then first name without using built-in sorting functions.
- Persists customer data so it remains available after the server restarts.
- Supports GET requests to retrieve a list of all customers in the system.

## Usage

1. Clone the repository.
2. Navigate to the `Customer_API` directory.
3. Build the application using your preferred .NET Core build method (`dotnet build`, Visual Studio, etc.).
4. Run the application (`dotnet run`).
5. Use an API testing tool such as Postman to send POST and GET requests to the API endpoints.

