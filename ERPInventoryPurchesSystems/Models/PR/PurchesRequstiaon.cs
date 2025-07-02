using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class PurchesRequstiaon
    {
        [Key]
        public int PRId { get; set; }

        [Required]
        public string PRNumber { get; set; }

        public DateTime PRDate { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string Status { get; set; }

        public string SubmittedByUserId { get; set; }
        [ForeignKey("SubmittedByUserId")]
        public User SubmittedBy { get; set; }

        public ICollection<PRItem> Items { get; set; }
    }
}
