using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        public string ApproverName { get; set; }

        public string ApprovalStatus { get; set; }

        public string Comments { get; set; }

        public DateTime ApprovalDate { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }
    }

}
