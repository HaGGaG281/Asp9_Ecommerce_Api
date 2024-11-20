using Asp9_Ecommerce_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<Users,IdentityRole<int> , int>
    {

        //Purpose of the Constructor:
        //The DbContext class in EF Core has a constructor that accepts DbContextOptions<TContext>,
        //where TContext is your specific context class (in this case, AppDbContext).
        //This DbContextOptions is used to configure the database connection string, 
        //other options(like lazy loading, logging, etc.), and any additional settings related to the context.
        //DbContextOptions<AppDbContext> options: This parameter allows EF Core to pass all the
        //configuration settings for the database context when initializing it. This can include things like
        //the database provider (e.g., SQL Server, SQLite, etc.),
        //the connection string, or other database-related settings.
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>().ToTable("AspNetUsers");
            // Configure the StoreItem join table
            modelBuilder.Entity<ItemsStores>()
                .HasKey(si => new { si.StoreId, si.ItemId }); // Composite key

            modelBuilder.Entity<ItemsUnits>()
              .HasKey(iu => new { iu.ItemCode, iu.UnitCode }); // Composite key

            modelBuilder.Entity<CustomerStores>()
              .HasKey(cs => new { cs.Cus_Id, cs.StoreId}); // Composite key

            modelBuilder.Entity<ShoppingCartItem>()
             .HasKey(sc => new { sc.Cus_Id, sc.Store_Id , sc.ItemCode }); // Composite key

        }
        public DbSet<Users> Users {  get; set; }
        public DbSet<Items> Items {  get; set; }
        public DbSet<Stores> Stores {  get; set; }
        public DbSet<Cities> Cities {  get; set; }
        public DbSet<Governments> Governments {  get; set; }
        public DbSet<Invoices> Invoices {  get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail {  get; set; }
        public DbSet<ItemsStores> ItemsStores {  get; set; }
        public DbSet<Units> Units {  get; set; }
        public DbSet<ItemsUnits> ItemsUnits { get; set; }
        public DbSet<CustomerStores> CustomerStores{ get; set; }
        public DbSet<IdentityRole<int>> Roles { get; set; }
        public DbSet<Classifications> Classifications { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

    }
}
