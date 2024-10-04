# easyUTR

## Prepare the database

1. Open SQL Server Management Studio (SSMS), create a new database called `EasyUTR`.
2. Right click the created database `EasyUTR`, choose "New Query" to open a new query window for the database.
~~3. Open SQL script [doc/database/DropTables.sql](doc/database/1_CreateTables.sql). Copy the content to your SSMS window and run it to drop all existing tables.~~
3. Open "Tools" -> "Nuget Package Manager" -> "Package Manager Console" (PMC), create tables that are synchronised with Models in the code:
	- run `Drop-Database -Context EasyUtrContext -Verbose`
	- run `Add-Migration InitialIdentityContext -Context EasyUtrIdentityContext`
	- run `Update-Database -Context EasyUtrIdentityContext`
	- run `Add-Migration InitialContext -Context EasyUtrContext`
	- run `Update-Database -Context EasyUtrContext`
4. Open SQL script [doc/database/2_PopulateTables.sql](doc/database/2_PopulateTables.sql). Copy the content to your SSMS window and run it to populate tables with sample data.

The design of the database can be found in UML file [doc/database/database-design.uxf](doc/database/database-design.uxf), which can be viewed using [UMLet](https://www.umlet.com/).

## Configuration file `appsettings.json`

In order to protect sensitive data in the configuration file, we remove `appsettings.json` from the respository. You should create your own `appsettings.json` using the following template.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "EasyUtrContext": "Data Source=XXXXXXXXXXX;Initial Catalog=EasyUTR;...etc for your own connection string"
  },
  "Stripe": {
    "SecretKey": "sk_test_YOUR_SECRET_KEY",
    "PublicKey": "pk_test_YOUR_PUBLIC_KEY"
  }
  "AdminSettings": {
    "Email": "admin@email.address",
    "Password":  "Admin_Password"
  }
}
```

## Run the project

Open [easyUTR/appsettings.json](easyUTR/appsettings.json), and **modify database connection string of `ConnectionStrings` -> `EasyUtrContext` to match the database built in your own local environment**.


## Main Functionality

## Customer Features
Registration and Login
- Customers can log in or register from the top right of the page to access their accounts.

Browse and Shop
- On the home page, customers can navigate through a feature guide to the "Shop" page, where they can browse available items.

- The shop includes search and filtering options to help customers refine their search based on specific needs.
- Upon selecting an item, customers are directed to a detailed item page where they can choose their preferred petrol station and adjust the quantity of the item.
- Customers can add selected items to their shopping cart, with all selected items and quantities tracked in the cart.

Checkout and Payment
- Customers can proceed to the cart and complete the checkout process once they are done shopping.
Payment is made through a secure third-party payment system.

Store Browsing
- By selecting "Our Stores," customers can view a list of all UTR petrol stations.
Clicking on a specific station will provide detailed information about that station, including the items it has available.

Order History
- Customers can view their completed orders in the "Order History" section, allowing them to track past purchases.

UTR Rewards
- On the home page, customers can participate in the UTR Rewards program to earn points and access exclusive deals.
- Details about the rewards program are available on the "Join UTR Rewards" page.

## Admin Features
Store Management
- Admins can edit all UTR petrol stations, updating the details of each station as needed.
- In the edit page, admins can add new stations by providing detailed information about the new UTR station.
- Admins can also delete stations from the list by simply clicking delete, removing the station from the system.

Staff Management
- Admins can manage staff by navigating to the Staff section, where they can add new employees by filling out their details in the Create New Staff form.
- The admin can view a complete list of all staff members and has the ability to edit staff details when necessary.

## Staff Features
Item Management
- UTR staff can log into the system and modify item details, ensuring that the information about each item is accurate and up to date.
- Staff can also add new items to the item list or delete items that are no longer available.

Store Management
- Similar to items, staff members can also edit store details, add new stores to the system, or delete stores from the list as needed.