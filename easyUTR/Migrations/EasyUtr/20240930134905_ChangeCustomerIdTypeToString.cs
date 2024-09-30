using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easyUTR.Migrations.EasyUtr
{
    /// <inheritdoc />
    public partial class ChangeCustomerIdTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    addressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addressLine = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    suburb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    postcode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    region = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Address_PK", x => x.addressID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Customer_PK", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false),
                    parentCategoryID = table.Column<int>(type: "int", nullable: true),
                    categoryName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ItemCategory_PK", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    jobID = table.Column<int>(type: "int", nullable: false),
                    jobName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Job_PK", x => x.jobID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    supplierID = table.Column<int>(type: "int", nullable: false),
                    supplierName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    supplierDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    supplierURL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Supplier_PK", x => x.supplierID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    storeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    storeDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    storeImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    addressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Store_PK", x => x.storeID);
                    table.ForeignKey(
                        name: "Store_Address_FK",
                        column: x => x.addressID,
                        principalTable: "Addresses",
                        principalColumn: "addressID");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    itemDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    itemImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    categoryID = table.Column<int>(type: "int", nullable: false),
                    supplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Item_PK", x => x.itemID);
                    table.ForeignKey(
                        name: "Item_ItemCategory_FK",
                        column: x => x.categoryID,
                        principalTable: "ItemCategories",
                        principalColumn: "categoryID");
                    table.ForeignKey(
                        name: "Item_Supplier_FK",
                        column: x => x.supplierID,
                        principalTable: "Suppliers",
                        principalColumn: "supplierID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    paidTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    customerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerOrder_PK", x => x.orderID);
                    table.ForeignKey(
                        name: "CustomerOrder_Customer_FK",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "CustomerOrder_Store_FK",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    staffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false),
                    jobID = table.Column<int>(type: "int", nullable: false),
                    jobLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Staff_PK", x => x.staffID);
                    table.ForeignKey(
                        name: "Staff_Job_FK",
                        column: x => x.jobID,
                        principalTable: "Jobs",
                        principalColumn: "jobID");
                    table.ForeignKey(
                        name: "Staff_Store_FK",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID");
                });

            migrationBuilder.CreateTable(
                name: "ItemsInStore",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    numberInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ItemsInStore_PK", x => new { x.itemID, x.storeID });
                    table.ForeignKey(
                        name: "ItemsInStore_Item_FK",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "itemID");
                    table.ForeignKey(
                        name: "ItemsInStore_Store_FK",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID");
                });

            migrationBuilder.CreateTable(
                name: "ItemsInOrder",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false),
                    orderID = table.Column<int>(type: "int", nullable: false),
                    numberOf = table.Column<int>(type: "int", nullable: false),
                    totalItemCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ItemsInOrder_PK", x => new { x.itemID, x.orderID });
                    table.ForeignKey(
                        name: "ItemsInOrder_CustomerOrder_FK",
                        column: x => x.orderID,
                        principalTable: "CustomerOrders",
                        principalColumn: "orderID");
                    table.ForeignKey(
                        name: "ItemsInOrder_Item_FK",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "itemID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_customerID",
                table: "CustomerOrders",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_storeID",
                table: "CustomerOrders",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_categoryID",
                table: "Items",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_supplierID",
                table: "Items",
                column: "supplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInOrder_orderID",
                table: "ItemsInOrder",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInStore_storeID",
                table: "ItemsInStore",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_jobID",
                table: "Staff",
                column: "jobID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_storeID",
                table: "Staff",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_addressID",
                table: "Stores",
                column: "addressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsInOrder");

            migrationBuilder.DropTable(
                name: "ItemsInStore");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
