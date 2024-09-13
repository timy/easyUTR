## Definition of customerised Models

"ItemBriefModel" :=
(ItemId, ItemName, )

"ItemDetailModel" :=
(itemId, itemName, itemDescription, itemImage, 
categoryId, categoryName, parentCategoryId, parentCategoryName,
supplierId, supplierName, supplierDescription, supplierUrl)

"StoreItemDetailModel" :=
(ItemDetailModel detail, price, numberInStock)

"StoreInventoryDetailModel" :=
(storeID, storeName, storeAddress, storeDescription, storeImage, 
Dictionary<int, List<StoreItemDetailModel>> items)

"StoreItemViewModel" :=
(StoreInventoryDetailModel store, 
searchText, categoryID, categoryList, supplierID, supplierList)


## Required models from Views

Item.Index: "ItemListViewModel"
Item (itemID, itemName, itemDescription, itemImage) + ItemsInStore (aggregated data - price range)

Item.Detail: "StoreItemViewModel"
ItemDetailModel + ItemInStore + Store (detailed data - price of each store)

Store.Detail: "StoreItemViewModel"
ItemDetailModel + ItemInStore + Store (detailed data - price of each store)