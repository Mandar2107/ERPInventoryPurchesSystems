using System;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Department
    {
        [Key]
        [Required]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Department Type")]
        public string DepartmentType { get; set; }

        [Display(Name = "Parent Department")]
        public string ParentDepartment { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Department Head")]
        public string DepartmentHead { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Extension Number")]
        public string ExtensionNumber { get; set; }

        [Display(Name = "Primary Function")]
        public string PrimaryFunction { get; set; }

        [Display(Name = "Linked Modules")]
        public string LinkedModules { get; set; }

        [Display(Name = "Approval Authority Level")]
        public string ApprovalAuthorityLevel { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
    }
}
