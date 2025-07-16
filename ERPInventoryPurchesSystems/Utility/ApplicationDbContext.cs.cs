
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Models.PR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Utility
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<ItemVendor> ItemVendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemVendor>()
                    .HasOne(iv => iv.Item)
                    .WithMany(i => i.ItemVendors)
                    .HasForeignKey(iv => iv.ItemCode);

            modelBuilder.Entity<ItemVendor>()
                .HasOne(iv => iv.Vendor)
                .WithMany(v => v.ItemVendors)
                .HasForeignKey(iv => iv.VendorCode);



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
                .HasForeignKey(a => a.PurchaseRequisitionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Department)
                .WithMany()
                .HasForeignKey(a => a.DepartmentCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RFQItem>()
    .Property(r => r.QuotationAmount)
    .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseOrder>()
      .HasOne(po => po.PurchaseRequisition)
      .WithMany()
      .HasForeignKey(po => po.PRId)
      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuotationComparison>()
                .HasOne(q => q.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(q => q.PRId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceiptNote>()
                .HasOne(grn => grn.PurchaseOrder)
                .WithMany()
                .HasForeignKey(grn => grn.POId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(inv => inv.PurchaseOrder)
                .WithMany()
                .HasForeignKey(inv => inv.POId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(inv => inv.GRN)
                .WithMany()
                .HasForeignKey(inv => inv.GRNId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QualityInspection>()
                .HasOne(qi => qi.GRN)
                .WithMany()
                .HasForeignKey(qi => qi.GRNId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalInvoiceAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceItem>()
                .Property(ii => ii.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceItem>()
                .Property(ii => ii.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<POItem>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<POItem>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuotationComparisonItem>()
                .Property(q => q.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuotationComparisonItem>()
                .Property(q => q.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<GoodsReceiptNote>()
    .HasOne(grn => grn.VerifiedBy)
    .WithMany()
    .HasForeignKey(grn => grn.VerifiedByUserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceiptNote>()
                .HasOne(grn => grn.ReceivedBy)
                .WithMany()
                .HasForeignKey(grn => grn.ReceivedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<QualityInspection>()
    .HasOne(q => q.InspectedBy)
    .WithMany()
    .HasForeignKey(q => q.InspectedByUserId)
    .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<QualityInspection>()
                .HasOne(q => q.ActionTakenBy)
                .WithMany()
                .HasForeignKey(q => q.ActionTakenByUserId)
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
