using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class QuotationComparison
    {
        [Key]
        public int ComparisonId { get; set; }

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string RequestedByUserId { get; set; }
        [ForeignKey("RequestedByUserId")]
        public User RequestedBy { get; set; }

        public DateTime DateOfComparison { get; set; }

        public ICollection<QuotationComparisonItem> Items { get; set; }
    }
}
