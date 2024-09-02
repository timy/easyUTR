# easyUTR

## Create the database

1. Open SQL Server Management Studio (SSMS), create a new database called `EasyUTR`.
2. Right click the created database `EasyUTR`, choose "New Query" to open a new query window for the database.
3. Open SQL script [doc/database/1_CreateTables.sql](doc/database/1_CreateTables.sql). Copy the content to your SSMS window and run it to create all tables required by our EasyUTR app.

The design of the database can be found in UML file [doc/database/database-design.uxf](doc/database/database-design.uxf), which can be viewed using [UMLet](https://www.umlet.com/).