# Car-rental-app
#### Car rental app that lets you rent Tesla cars in Majorca
## Configuration
* Open CarRentalServar in visual studio and then in Package Manager console run `update-database` command
* Run the program to load sample data to the database and start the server
* Navigate in the terminal to the folder "car-rental-client"
* Run two commands: `npm install` and `npm start`
## Tech stack
* ASP.NET Core Web API
* .NET 6
* Entity Framework Core
* Microsoft SQL Server
* JSON Web Token
* React
* TypeScript
* Axios
* Tailwind
## Database (MS SQL Server)
![car-rent-db](https://github.com/MParchan/Car-rental-app/assets/85680066/28e730e7-9dbe-46a9-b551-d86902ac26f5)
## Functionalities
* Login and registration
* Browsing and filtering car models for rent
* Checking car availability at a given location and rental date
* Creating a reservation
* Viewing your own reservations
* Cancelling of reservation by user
* Managing reservations by admin
## Reservation mechanism 
A reservation can have 4 statuses:
* Pending: the reservation has been created and is waiting for the rental start date,
* Started: the reservation has begun and the car has been picked up,
* Completed: the reservation has ended and the car has been returned,
* Cancelled: the reservation has been cancelled.

When creating a reservation, the user must be logged in and provide the car they want to rent, the rental location, the return location, the start date, and the end date. The start date cannot be earlier than the current day, and the end date cannot be earlier than the start date. It is then checked whether the selected car is available at the specified location and period. This is calculated as follows:
* Quantity of selected car at the given location - the number of reservations of the selected car at the given location and period that have the status Pending or Started. 

If the car is available, the rental cost is calculated, a reservation with the status Pending is created, and the user's email is assigned to it. If the user rents a car for at least 3 days, they get a discount of -10%, and if for at least 5 days - 20%. When the car is picked up, a user with the role of "Admin" or "Manager" changes the status to Started, and upon return, to Completed. The user can also cancel the reservation, but only if it is in the Pending status.
