# easyUTR

## Prepare the database

1. Open SQL Server Management Studio (SSMS), create a new database called `EasyUTR`.
2. Right click the created database `EasyUTR`, choose "New Query" to open a new query window for the database.
3. Open SQL script [doc/database/DropTables.sql](doc/database/1_CreateTables.sql). Copy the content to your SSMS window and run it to drop all existing tables.
4. Open "Tools" -> "Nuget Package Manager" -> "Package Manager Console" (PMC), create tables that are synchronised with Models in the code:
	- run `Update-Database -Context EasyUtrIdentityContext`
	- run `Update-Database -Context EasyUtrContext`
5. Open SQL script [doc/database/2_PopulateTables.sql](doc/database/2_PopulateTables.sql). Copy the content to your SSMS window and run it to populate tables with sample data.

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
}
```

## Run the project

Open [easyUTR/appsettings.json](easyUTR/appsettings.json), and **modify database connection string of `ConnectionStrings` -> `EasyUtrContext` to match the database built in your own local environment**.


