using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string VendorCode { get; set; }
        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public int POId { get; set; }
        [ForeignKey("POId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        public int GRNId { get; set; }
        [ForeignKey("GRNId")]
        public GoodsReceiptNote GRN { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public ICollection<InvoiceItem> Items { get; set; }

        public decimal TotalInvoiceAmount { get; set; }

        public string PaymentTerms { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public string ProcessedByUserId { get; set; }
        [ForeignKey("ProcessedByUserId")]
        public User ProcessedBy { get; set; }
    }
}
