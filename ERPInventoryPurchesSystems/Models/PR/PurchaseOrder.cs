using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class PurchaseOrder
    {
        [Key]
        public int POId { get; set; }

        public string PONumber { get; set; }

        public DateTime PODate { get; set; }

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        public string VendorCode { get; set; }
        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public string VendorContact { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string RequestedByUserId { get; set; }
        [ForeignKey("RequestedByUserId")]
        public User RequestedBy { get; set; }

        public ICollection<POItem> Items { get; set; }
    }
}
