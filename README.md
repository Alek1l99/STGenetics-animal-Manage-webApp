# Animal Web Application
This is a web application built using ASP.NET Core 6.0 framework with MVC (Model-View-Controller) or Blazor. 
It allows users to manage animal data, apply filters, perform CRUD operations, and calculate purchase amounts 
based on specified business rules.
## Requirements
- ASP.NET Core 6.0
- MVC or Blazor framework
- .NET 6 or .NET 7

## Getting Started
To run this web application locally, follow these steps:

1. Clone the repository:

```
git clone https://github.com/your-username/animal-web-app.git

```

2. Open the project in Visual Studio or your preferred code editor.

3. Build the project to restore NuGet packages and compile the code.

4. Set up the database:
  - Create a local or in-memory database.
  - Update the connection string in the appsettings.json file to point to your database.
5. Seed the database with fictional animal data:
  - Run the provided SQL script (seed.sql) or use a data migration strategy.
6. Run the application:
  - Press F5 in Visual Studio or use the appropriate command in your code editor to start the application.
  - The application will launch in your default web browser.
7. Explore the web application:
- Navigate to the default page to access the main menu.
- Use the menu to switch between the Default and Animal pages.
- Perform various actions such as filtering data, adding, updating, and deleting animal records.

## Features
- Default Page:

  -Provides a main page with a navigation menu for easy access to different sections of the web application.
- Animal Page:

  - Form:
    - Allows users to enter and filter animal data based on different fields.
    - Provides a "Filter" button to apply the specified filters.
- Grid:
   - Displays animal data in a grid format.
   - Supports pagination with 10 animals per page.
   - Enables CRUD operations (Insert, Update, Delete) on the animal records.
   - Includes a footer displaying the sum of values in the Price column.
   - Provides a checkbox column to select animals for further processing.
   - Includes a dropdown menu in each row for selecting items from a list.
- Page to Display Selected Animals:

 - Groups selected animals by breed.
 - Calculates total purchase amounts based on selected animals and applies discounts as per business rules.
 - Displays discount percentage and shipping amount based on the total number of animals.

## Business Rules
-  If a customer adds an animal with a quantity greater than 5 in the cart, a 5% discount will be applied to the value of that animal.
-  If a customer buys more than 10 animals in the order, an additional 3% discount will be added to the total purchase price.
-  If a customer buys more than 20 animals in the order, the freight value will be free; otherwise, a $1,000.00 freight charge will apply.
-  Duplicate animals are not allowed in the order. If a duplicate animal is identified, an error message will be displayed.

## Technologies Used

- ASP.NET Core 6.0
- MVC or Blazor framework
- Entity Framework Core (for database operations)
- HTML, CSS, and JavaScript (or Razor syntax for Blazor)

## License
This project is licensed under the MIT License.

Feel free to modify and enhance the application according to your needs.

## Acknowledgements
This project was developed as a fictitious web application for demonstration purposes.
