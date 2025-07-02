using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class RequestForQuotation

    {
        [Key]
        public int RFQId { get; set; }

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        public string VendorCode { get; set; }
        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public ICollection<RFQItem> Items
        {
            get; set;
        }
    }
}
