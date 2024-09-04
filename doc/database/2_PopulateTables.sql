/****** Customers ******/
INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (1, 'James', 'Brown', 'james.brown@gmail.com', '0412345678');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (2, 'Olivia', 'Wilson', 'olivia.wilson@gmail.com', '0498765432');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (3, 'Liam', 'Smith', 'liam.smith@gmail.com', '0401234567');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (4, 'Sophia', 'Taylor', 'sophia.taylor@gmail.com', '0410987654');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (5, 'William', 'Anderson', 'william.anderson@gmail.com', '0423456789');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (6, 'Emily', 'Brown', 'emily.brown@gmail.com', '0432567890');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (7, 'Benjamin', 'Moore', 'benjamin.moore@gmail.com', '0445678901');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (8, 'Charlotte', 'Martin', 'charlotte.martin@gmail.com', '0456789012');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (9, 'Mia', 'White', 'mia.white@gmail.com', '0467890123');

INSERT INTO Customers (customerID, firstName, lastName, email, phoneNumber)
VALUES (10, 'Lucas', 'Lee', 'lucas.lee@gmail.com', '0478901234');


/****** ItemCategories ******/
INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (1, NULL, 'Fuel');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (2, 1, 'Regular Unleaded');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (3, 1, 'Premium Unleaded');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (4, 1, 'Diesel');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (5, NULL, 'Food & Beverages');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (6, 5, 'Coffee');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (7, 5, 'Juices');

INSERT INTO ItemCategories (categoryID, parentCategoryID, categoryName)
VALUES (8, 5, 'Bakery');




