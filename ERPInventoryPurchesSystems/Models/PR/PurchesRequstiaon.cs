using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class PurchesRequstiaon
    {
        [Key]
        public int PurchaseRequisitionID { get; set; }

        public string PRNumber { get; set; } // Auto-generated
        public DateTime PRDate { get; set; }

        [Required]
        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string Status { get; set; } // Pending, Approved, Rejected

        [Required]
        public string SubmittedByUserID { get; set; }
        [ForeignKey("SubmittedByUserID")]
        public User SubmittedBy { get; set; }

        public ICollection<PRItem> Items { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
