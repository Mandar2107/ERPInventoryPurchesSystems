
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Models.PR;

using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Utility
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }   
        public DbSet<PurchesRequstiaon> PurchaseRequisitions { get; set; }
        public DbSet<PRItem> PRItems { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<RequestForQuotation> RequestForQuotations { get; set; }
        public DbSet<RFQItem> RFQItems { get; set; }
        public DbSet<QuotationComparison> QuotationComparisons { get; set; }
        public DbSet<QuotationComparisonItem> QuotationComparisonItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<POItem> POItems { get; set; }
        public DbSet<GoodsReceiptNote> GoodsReceiptNotes { get; set; }
        public DbSet<GRNItem> GRNItems { get; set; }
        public DbSet<QualityInspection> QualityInspections { get; set; }
        public DbSet<InspectionItem> InspectionItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
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

            modelBuilder.Entity<Vendor>()
                .HasMany(v => v.Items)
                .WithOne(i => i.PreferredVendor)
                .HasForeignKey(i => i.PreferredVendorCode)
                .OnDelete(DeleteBehavior.SetNull);

           
            modelBuilder.Entity<Vendor>()
                .Property(v => v.CreditLimit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Vendor>()
                .Property(v => v.TotalSpend)
                .HasPrecision(18, 2);

        
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

            
            modelBuilder.Entity<Approval>()
                .HasOne(a => a.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(a => a.PRId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Department)
                .WithMany()
                .HasForeignKey(a => a.DepartmentCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
