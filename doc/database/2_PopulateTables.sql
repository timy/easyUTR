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

/****** Addresses ******/

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('58 West Terrace', 'Adelaide', '5000', 'SA', -34.9290, 138.5897);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('120 Goodwood Rd', 'Wayville', '5034', 'SA', -34.9507, 138.5924);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('235 North East Road', 'Hampstead Gardens', '5086', 'SA', -34.8673, 138.6218);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('23 Main South Rd', 'Morphett Vale', '5162', 'SA', -35.1346, 138.5131);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('170 Unley Rd', 'Unley', '5061', 'SA', -34.9488, 138.6063);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('300 Magill Rd', 'Kensington Park', '5068', 'SA', -34.9276, 138.6485);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('501 Portrush Rd', 'Glenunga', '5064', 'SA', -34.9484, 138.6306);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('97 Henley Beach Rd', 'Mile End', '5031', 'SA', -34.9227, 138.5695);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('301 South Rd', 'Mile End', '5031', 'SA', -34.9352, 138.5764);

INSERT INTO Addresses (addressLine, suburb, postcode, region, latitude, longitude)
VALUES ('88 Payneham Rd', 'Stepney', '5069', 'SA', -34.9147, 138.6219);

/****** Jobs ******/

INSERT INTO Jobs (jobID, jobName)
VALUES (1, 'Store Admin');

INSERT INTO Jobs (jobID, jobName)
VALUES (2, 'Store Assistant');

INSERT INTO Jobs (jobID, jobName)
VALUES (3, 'Cashier');

INSERT INTO Jobs (jobID, jobName)
VALUES (4, 'Stock Manager');

/****** Stores ******/

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR West Terrace', 'Convenience store and petrol station located in Adelaide city.', 'https://t3.ftcdn.net/jpg/05/42/17/70/360_F_542177036_v12rYhGPt06ZrbVyLHenryRtsHc6r2S5.jpg', 1);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Goodwood Rd', 'Convenience store and petrol station located in Wayville.', 'https://t3.ftcdn.net/jpg/06/23/26/90/360_F_623269044_OiBgP53WjYHgmldEGwv8AauCemtiaCqS.jpg', 2);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR North East Rd', 'Convenience store and petrol station in Hampstead Gardens.', 'https://i.pinimg.com/1200x/34/05/8b/34058b59d45608c5bd639bbc811250c4.jpg', 3);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Main South Rd', 'Convenience store and petrol station located in Morphett Vale.', 'https://www.businessnews.com.lb/cms/Portals/2/Business/gasstationsout10092018.jpg', 4);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Unley Rd', 'Convenience store and petrol station in Unley.', 'https://s.yimg.com/ny/api/res/1.2/BlGpc4_BQdqGpRw_5CF5gQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTY0MDtoPTM2MA--/https://media.zenfs.com/en/gobankingrates_644/cbef0f16285e60ae94523066be9b97ed', 5);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Magill Rd', 'Convenience store and petrol station in Kensington Park.', 'https://img.freepik.com/premium-photo/gas-station-with-red-light-side_771426-7513.jpg', 6);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Portrush Rd', 'Convenience store and petrol station in Glenunga.', 'https://www.lbcgroup.tv/uploadImages/DocumentImages/News-P-679887-638078279555529942.jpg', 7);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Henley Beach Rd', 'Convenience store and petrol station in Mile End.', 'https://t4.ftcdn.net/jpg/06/63/74/75/360_F_663747502_2QPA0TIAJhSTwTPCQmvf37oLFyAEB60R.jpg', 8);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR South Rd', 'Convenience store and petrol station in Mile End.', 'https://img.freepik.com/premium-photo/gas-station-with-clouds-blue-sky_908344-13524.jpg', 9);

INSERT INTO Stores (storeName, storeDescription, storeImage, addressID)
VALUES ('UTR Payneham Rd', 'Convenience store and petrol station in Stepney.', 'https://img.freepik.com/premium-photo/gas-station-cars-oil-gas-industry-petrol-station_75563-29918.jpg', 10);




/****** ItemsInStore ******/

/****** Store 1 (UTR West Terrace) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (1, 1, 1.55, 500);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (1, 2, 1.75, 400);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (1, 3, 1.60, 300); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (1, 4, 3.50, 150);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (1, 5, 5.00, 200); 

/****** Store 2 (UTR Goodwood Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (2, 1, 1.60, 450);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (2, 2, 1.80, 350);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (2, 3, 1.70, 320);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (2, 6, 3.00, 120);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (2, 7, 4.50, 100); 

/****** Store 3 (UTR North East Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (3, 1, 1.65, 400);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (3, 2, 1.85, 300);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (3, 3, 1.75, 280);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (3, 8, 3.75, 90);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (3, 5, 4.75, 160);  

/****** Store 4 (UTR Main South Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (4, 1, 1.50, 520);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (4, 2, 1.70, 380);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (4, 3, 1.65, 310);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (4, 9, 3.25, 110);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (4, 6, 2.75, 180);  

/****** Store 5 (UTR Unley Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (5, 1, 1.55, 490);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (5, 2, 1.70, 360);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (5, 3, 1.60, 330); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (5, 4, 3.50, 140);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (5, 5, 5.00, 130);  

/****** Store 6 (UTR Magill Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (6, 1, 1.55, 520);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (6, 2, 1.75, 410);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (6, 3, 1.60, 340);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (6, 7, 4.00, 180);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (6, 8, 3.50, 150); 

/****** Store 7 (UTR Portrush Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (7, 1, 1.60, 530);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (7, 2, 1.80, 450);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (7, 3, 1.70, 360);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (7, 4, 3.60, 160);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (7, 9, 4.00, 120); 

/****** Store 8 (UTR Henley Beach Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (8, 1, 1.50, 540); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (8, 2, 1.70, 460); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (8, 3, 1.65, 380); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (8, 6, 2.90, 140); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (8, 7, 4.50, 170);  

/****** Store 9 (UTR South Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (9, 1, 1.55, 560); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (9, 2, 1.80, 470); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (9, 3, 1.70, 370); 

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (9, 4, 3.40, 150);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (9, 5, 5.00, 120); 

/****** Store 10 (UTR Payneham Rd) ******/
INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (10, 1, 1.60, 500);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (10, 2, 1.85, 400);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (10, 3, 1.65, 330);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (10, 8, 3.75, 140);  

INSERT INTO ItemsInStore (storeID, itemID, price, numberInStock)
VALUES (10, 7, 4.25, 130);