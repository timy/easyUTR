using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using easyUTR.Models;

namespace easyUTR.Data {

    public partial class EasyUtrContext : DbContext
    {
        public EasyUtrContext()
        {
        }

        public EasyUtrContext(DbContextOptions<EasyUtrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<ItemCategory> ItemCategories { get; set; }

        public virtual DbSet<ItemsInOrder> ItemsInOrders { get; set; }

        public virtual DbSet<ItemsInStore> ItemsInStores { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("Address_PK");

                entity.Property(e => e.AddressId).HasColumnName("addressID");
                entity.Property(e => e.AddressLine)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("addressLine");
                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitude");
                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitude");
                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postcode");
                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("region");
                entity.Property(e => e.Suburb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("suburb");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("Customer_PK");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("customerID");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");
                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("CustomerOrder_PK");

                entity.Property(e => e.OrderId).HasColumnName("orderID");
                entity.Property(e => e.CustomerId).HasColumnName("customerID");
                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("orderTime");
                entity.Property(e => e.PaidTime)
                    .HasColumnType("datetime")
                    .HasColumnName("paidTime");
                entity.Property(e => e.StoreId).HasColumnName("storeID");

                entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerOrder_Customer_FK");

                entity.HasOne(d => d.Store).WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerOrder_Store_FK");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId).HasName("Item_PK");

                entity.Property(e => e.ItemId).HasColumnName("itemID");
                entity.Property(e => e.CategoryId).HasColumnName("categoryID");
                entity.Property(e => e.ItemDescription)
                    .IsUnicode(false)
                    .HasColumnName("itemDescription");
                entity.Property(e => e.ItemImage)
                    .IsUnicode(false)
                    .HasColumnName("itemImage");
                entity.Property(e => e.ItemName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("itemName");
                entity.Property(e => e.SupplierId).HasColumnName("supplierID");

                entity.HasOne(d => d.Category).WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Item_ItemCategory_FK");

                entity.HasOne(d => d.Supplier).WithMany(p => p.Items)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Item_Supplier_FK");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("ItemCategory_PK");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryID");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");
                entity.Property(e => e.ParentCategoryId).HasColumnName("parentCategoryID");
            });

            modelBuilder.Entity<ItemsInOrder>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.OrderId }).HasName("ItemsInOrder_PK");

                entity.ToTable("ItemsInOrder");

                entity.Property(e => e.ItemId).HasColumnName("itemID");
                entity.Property(e => e.OrderId).HasColumnName("orderID");
                entity.Property(e => e.NumberOf).HasColumnName("numberOf");
                entity.Property(e => e.TotalItemCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalItemCost");

                entity.HasOne(d => d.Item).WithMany(p => p.ItemsInOrders)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemsInOrder_Item_FK");

                entity.HasOne(d => d.Order).WithMany(p => p.ItemsInOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemsInOrder_CustomerOrder_FK");
            });

            modelBuilder.Entity<ItemsInStore>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.StoreId }).HasName("ItemsInStore_PK");

                entity.ToTable("ItemsInStore");

                entity.Property(e => e.ItemId).HasColumnName("itemID");
                entity.Property(e => e.StoreId).HasColumnName("storeID");
                entity.Property(e => e.NumberInStock).HasColumnName("numberInStock");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Item).WithMany(p => p.ItemsInStores)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemsInStore_Item_FK");

                entity.HasOne(d => d.Store).WithMany(p => p.ItemsInStores)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemsInStore_Store_FK");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId).HasName("Job_PK");

                entity.Property(e => e.JobId)
                    .ValueGeneratedNever()
                    .HasColumnName("jobID");
                entity.Property(e => e.JobName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("jobName");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffId).HasName("Staff_PK");

                entity.Property(e => e.StaffId).HasColumnName("staffID");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");
                entity.Property(e => e.JobId).HasColumnName("jobID");
                entity.Property(e => e.JobLevel).HasColumnName("jobLevel");
                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
                entity.Property(e => e.StoreId).HasColumnName("storeID");

                entity.HasOne(d => d.Job).WithMany(p => p.Staff)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Staff_Job_FK");

                entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Staff_Store_FK");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId).HasName("Store_PK");

                entity.Property(e => e.StoreId).HasColumnName("storeID");
                entity.Property(e => e.AddressId).HasColumnName("addressID");
                entity.Property(e => e.StoreDescription)
                    .IsUnicode(false)
                    .HasColumnName("storeDescription");
                entity.Property(e => e.StoreImage)
                    .IsUnicode(false)
                    .HasColumnName("storeImage");
                entity.Property(e => e.StoreName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("storeName");

                entity.HasOne(d => d.Address).WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Store_Address_FK");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("Supplier_PK");

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedNever()
                    .HasColumnName("supplierID");
                entity.Property(e => e.SupplierDescription)
                    .IsUnicode(false)
                    .HasColumnName("supplierDescription");
                entity.Property(e => e.SupplierName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("supplierName");
                entity.Property(e => e.SupplierUrl)
                    .IsUnicode(false)
                    .HasColumnName("supplierURL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}