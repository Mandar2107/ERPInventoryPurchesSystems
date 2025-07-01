using ERPInventoryPurchesSystems.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Utility
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department relationships
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.DepartmentCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Categories)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentCode)
                .OnDelete(DeleteBehavior.Restrict);

            // Category relationships
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.ItemCategoryCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Vendors)
                .WithOne(v => v.Category)
                .HasForeignKey(v => v.CategoryCode)
                .OnDelete(DeleteBehavior.Restrict);

            // Vendor relationships
            modelBuilder.Entity<Vendor>()
                .HasMany(v => v.Items)
                .WithOne(i => i.PreferredVendor)
                .HasForeignKey(i => i.PreferredVendorCode)
                .OnDelete(DeleteBehavior.SetNull);

            // Decimal precision for Vendor
            modelBuilder.Entity<Vendor>()
                .Property(v => v.CreditLimit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Vendor>()
                .Property(v => v.TotalSpend)
                .HasPrecision(18, 2);

            // Decimal precision for Item
            modelBuilder.Entity<Item>()
                .Property(i => i.LastPurchasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Item>()
                .Property(i => i.SellingPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Item>()
                .Property(i => i.StandardCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Item>()
                .Property(i => i.TaxRate)
                .HasPrecision(5, 2); 
        }
    }
}
