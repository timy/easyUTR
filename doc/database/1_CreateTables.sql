/****** Customers ******/
CREATE TABLE dbo.Customers(
	customerID int NOT NULL,
	firstName varchar(100) NOT NULL,
	lastName varchar(100) NOT NULL,
	email varchar(100),
	phoneNumber varchar(15),

	CONSTRAINT Customer_PK PRIMARY KEY (customerID ASC)
);

/****** ItemCategories ******/
CREATE TABLE dbo.ItemCategories(
	categoryID int NOT NULL,
	parentCategoryID int,
	categoryName varchar(100) NOT NULL,

	CONSTRAINT ItemCategory_PK PRIMARY KEY (categoryID ASC)
);

/****** Suppliers ******/
CREATE TABLE dbo.Suppliers(
	supplierID int NOT NULL,
	supplierName varchar(100) NOT NULL,
	supplierDescription varchar(max) NOT NULL,
	supplierURL varchar(max),

	CONSTRAINT Supplier_PK PRIMARY KEY (supplierID ASC)
);

/****** Items ******/
CREATE TABLE dbo.Items(
	itemID int IDENTITY(1, 1) NOT NULL,
	itemName varchar(150) NOT NULL,
	itemDescription varchar(max) NOT NULL,
	itemImage varchar(max),
	categoryID int NOT NULL,
	supplierID int NOT NULL,

	CONSTRAINT Item_PK PRIMARY KEY (itemID ASC),
	CONSTRAINT Item_ItemCategory_FK FOREIGN KEY(categoryID) REFERENCES dbo.ItemCategories (categoryID),
	CONSTRAINT Item_Supplier_FK FOREIGN KEY(supplierID) REFERENCES dbo.Suppliers (supplierID)
);

/****** Addresses ******/
CREATE TABLE dbo.Addresses(
	addressID int IDENTITY(1, 1) NOT NULL,
	addressLine varchar(150) NOT NULL,
	suburb varchar(50) NOT NULL,
	postcode varchar(10) NOT NULL,
	region varchar(50) NOT NULL,
	latitude decimal(9, 6) NOT NULL,
	longitude decimal(9, 6) NOT NULL,

	CONSTRAINT Address_PK PRIMARY KEY (addressID ASC)
);

/****** Jobs ******/
CREATE TABLE dbo.Jobs(
	jobID int NOT NULL,
	jobName varchar(150) NOT NULL,

	CONSTRAINT Job_PK PRIMARY KEY (jobID ASC)
);

/****** Stores ******/
CREATE TABLE dbo.Stores(
	storeID int IDENTITY(1, 1) NOT NULL,
	storeName varchar(100) NOT NULL,
	storeDescription varchar(max) NOT NULL,
	storeImage varchar(max),
	addressID int NOT NULL,

	CONSTRAINT Store_PK PRIMARY KEY (storeID ASC),
	CONSTRAINT Store_Address_FK FOREIGN KEY(addressID) REFERENCES dbo.Addresses (addressID)
);

/****** Staff ******/
CREATE TABLE dbo.Staff(
	staffID int IDENTITY(1, 1) NOT NULL,
	firstName varchar(100) NOT NULL,
	lastName varchar(100) NOT NULL,
	storeID int NOT NULL,
	jobID int NOT NULL,
	jobLevel int,

	CONSTRAINT Staff_PK PRIMARY KEY (staffID ASC),
	CONSTRAINT Staff_Store_FK FOREIGN KEY(storeID) REFERENCES dbo.Stores (storeID),
	CONSTRAINT Staff_Job_FK FOREIGN KEY(jobID) REFERENCES dbo.Jobs (jobID)
);

/****** CustomerOrders ******/
CREATE TABLE dbo.CustomerOrders(
	orderID int IDENTITY(1, 1) NOT NULL,
	orderTime datetime NOT NULL,
	paidTime datetime NOT NULL,
	customerID int NOT NULL,
	storeID int NOT NULL,

	CONSTRAINT CustomerOrder_PK PRIMARY KEY (orderID ASC),
	CONSTRAINT CustomerOrder_Customer_FK FOREIGN KEY(customerID) REFERENCES dbo.Customers (customerID),
	CONSTRAINT CustomerOrder_Store_FK FOREIGN KEY(storeID) REFERENCES dbo.Stores (storeID)
);

/****** ItemsInOrder ******/
CREATE TABLE dbo.ItemsInOrder(
	itemID int NOT NULL,
	orderID int NOT NULL,
	numberOf int NOT NULL,
	totalItemCost decimal(10, 2) NOT NULL,

	CONSTRAINT ItemsInOrder_PK PRIMARY KEY (itemID, orderID),
	CONSTRAINT ItemsInOrder_Item_FK FOREIGN KEY(itemID) REFERENCES dbo.Items (itemID),
	CONSTRAINT ItemsInOrder_CustomerOrder_FK FOREIGN KEY(orderID) REFERENCES dbo.CustomerOrders (orderID)
);

/****** ItemsInStore ******/
CREATE TABLE dbo.ItemsInStore(
	itemID int NOT NULL,
	storeID int NOT NULL,
	price decimal(10, 2) NOT NULL,
	numberInStock int NOT NULL,

	CONSTRAINT ItemsInStore_PK PRIMARY KEY (itemID, storeID),
	CONSTRAINT ItemsInStore_Item_FK FOREIGN KEY(itemID) REFERENCES dbo.Items (itemID),
	CONSTRAINT ItemsInStore_Store_FK FOREIGN KEY(storeID) REFERENCES dbo.Stores (storeID)
);