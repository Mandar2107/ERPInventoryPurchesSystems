using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class GoodsReceiptNote
    {
        [Key]
        public int GRNId { get; set; }

        public string GRNNumber { get; set; }

        public DateTime GRNDate { get; set; }

        public int POId { get; set; }
        [ForeignKey("POId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        public string VendorCode { get; set; }
        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public string ReceivedByUserId { get; set; }
        [ForeignKey("ReceivedByUserId")]
        public User ReceivedBy { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public ICollection<GRNItem> Items { get; set; }

        public string DeliveryNoteNumber { get; set; }

        public string TransporterName { get; set; }

        public string VehicleNumber { get; set; }

        public string InspectionStatus { get; set; }

        public string VerifiedByUserId { get; set; }
        [ForeignKey("VerifiedByUserId")]
        public User VerifiedBy { get; set; }

        public DateTime VerificationDate { get; set; }
    }
}
