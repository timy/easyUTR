## Definition of customerised Models

### ItemBriefModel
`ItemId, ItemName, ItemDescription, ItemImage`

### ItemDetailModel
`ItemId, ItemName, ItemDescription, ItemImage, CategoryId, CategoryName, ParentCategoryId, ParentCategoryName, SupplierId, SupplierName, SupplierDescription, SupplierUrl`

### ItemStoreDetailModel
`StoreId, StoreName, StoreAddress, Price, NumberInStock`

### StoreItemDetailModel
`Detail:ItemDetailModel, Price, NumberInStock`

### StoreInventoryDetailModel
`StoreId, StoreName, StoreAddress, StoreDescription, StoreImage, Items:Dictionary<int, List<StoreItemDetailModel>>`

### StoreItemViewModel
`ParentCategories:List<ItemCategory>, Store:StoreInventoryDetailModel, SearchText, CategoryId, CategoryList, SupplierId, SupplierList`

### ItemListViewModel
`ParentCategories:List<ItemCategory>, GroupedItems:Dictionary<int, List<Item>>, SearchText, CategoryId, CategoryList, SupplierId, SupplierList`


## Required models from Views

Item.Index: "ItemListViewModel"
Item (itemID, itemName, itemDescription, itemImage) + ItemsInStore (aggregated data - price range)

Item.Detail: "StoreItemViewModel"
ItemDetailModel + ItemInStore + Store (detailed data - price of each store)

Store.Detail: "StoreItemViewModel"
ItemDetailModel + ItemInStore + Store (detailed data - price of each store)