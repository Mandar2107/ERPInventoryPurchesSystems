using ERPInventoryPurchesSystems.Models.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class Approval
    {
        [Key]
        public int ApprovalID { get; set; }

        [Required]
        public int PurchaseRequisitionID { get; set; }
        [ForeignKey("PurchaseRequisitionID")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        [Required]
        public string ApproverUserID { get; set; }
        [ForeignKey("ApproverUserID")]
        public User Approver { get; set; }

        public string ApprovalStatus { get; set; } // Pending, Approved, Rejected
        public string Comments { get; set; }
        public DateTime ApprovalDate { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }
    }
}
