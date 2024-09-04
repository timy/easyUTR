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

/****** Suppliers ******/

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (1, 'Viva Energy Australia', 'Major fuel supplier in South Australia, providing fuel and petroleum products.', 'https://www.vivaenergy.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (2, 'BP Australia', 'One of the largest fuel and energy suppliers in Australia, with stations across South Australia.', 'https://www.bp.com/en_au/australia.html');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (3, 'Caltex Australia', 'Provider of fuel and petroleum products across Australia.', 'https://www.caltex.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (4, 'Grinders Coffee', 'Supplier of freshly brewed coffee and espresso beverages.', 'https://www.grinderscoffee.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (5, 'Coles Supermarkets', 'Australian supermarket chain supplying a wide variety of fresh and packaged foods.', 'https://www.coles.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (6, 'Woolworths Supermarkets', 'Leading supermarket chain in Australia providing food and household items.', 'https://www.woolworths.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (7, 'Berri Juice', 'Supplier of fresh fruit juices, including orange juice.', 'https://www.berrijuice.com.au');

INSERT INTO Suppliers (supplierID, supplierName, supplierDescription, supplierURL)
VALUES (8, 'Coca-Cola Amatil', 'Supplier of soft drinks and other beverage products in Australia.', 'https://www.ccamatil.com');

/****** Items ******/

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('91#unleaded', 'Regular unleaded petrol', 'https://www.bp.com/content/dam/bp/country-sites/en_au/australia/home/products-services/fuels/bp-regular-fuel-image.jpg.img.1920.medium.jpg', 2, 1);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('95#unleaded', 'Premium unleaded petrol', 'https://www.bp.com/content/dam/bp/country-sites/en_au/australia/home/products-services/fuels/bp-unleaded-95.jpg.img.2350.medium.jpg', 3, 2);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('0#', 'Diesel fuel', 'https://www.bp.com/content/dam/bp/country-sites/en_au/australia/home/products-services/fuels/bp-diesel-promo-box.jpg.img.2350.medium.jpg', 4, 3);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Coffee', 'Freshly brewed coffee', 'https://media-cldnry.s-nbcnews.com/image/upload/newscms/2019_33/2203981/171026-better-coffee-boost-se-329p.jpg', 6, 4);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Sandwich', 'Fresh sandwich with various fillings', 'https://www.watermelon.org/wp-content/uploads/2023/02/Sandwich_2023.jpg', 5, 5);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Donut', 'Freshly baked donut', 'https://hips.hearstapps.com/hmg-prod/images/glazed-donut-recipe-1-65008ab2b45fb.jpg?crop=0.566xw:1.00xh;0.269xw,0&resize=1200:*', 8, 6);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Croissant', 'Buttery and flaky croissant', 'https://everydaypie.com/wp-content/uploads/2023/05/Homemade-Croissants-119.jpg', 8, 6);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Muffin', 'Chocolate chip muffin', 'https://thefirstyearblog.com/wp-content/uploads/2020/05/Chocolate-Chip-Muffins-2023-Square.png', 8, 6);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Juice', 'Fresh orange juice', 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Orangejuice.jpg/1200px-Orangejuice.jpg', 7, 7);

INSERT INTO Items (itemName, itemDescription, itemImage, categoryID, supplierID)
VALUES ('Soft Drink', 'Cold soft drink', 'https://t3.ftcdn.net/jpg/03/69/56/02/360_F_369560255_ze7zKUVKic1yQKzmXOSym2shcEyGqKPg.jpg', 7, 8);

