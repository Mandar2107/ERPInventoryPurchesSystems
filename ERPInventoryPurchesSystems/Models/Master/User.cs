using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class User
    {
        [Key]
        [Required]
        public string UserID { get; set; }

        [Required]
        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Designation { get; set; }

        public string DepartmentCode { get; set; }

        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string Status { get; set; }

        public string UserRole { get; set; }

        public string AccessLevel { get; set; }

        public string LoginType { get; set; }

        public string AuthenticationMethod { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public string NotificationPreferences { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime PasswordExpiryDate { get; set; }

        public bool AccountLockStatus { get; set; }

        public bool AuditLogsEnabled { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
