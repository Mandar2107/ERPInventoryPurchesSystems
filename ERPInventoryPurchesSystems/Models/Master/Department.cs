using System;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Department
    {
        [Key]
        [Required]
        public string DepartmentCode { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string DepartmentType { get; set; }

        public string ParentDepartment { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public string DepartmentHead { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ExtensionNumber { get; set; }

        public string PrimaryFunction { get; set; }

        public string LinkedModules { get; set; }

        public string ApprovalAuthorityLevel { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
