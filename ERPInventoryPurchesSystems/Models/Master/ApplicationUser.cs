using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class ApplicationUser:IdentityUser
    {

        public string FullName { get; set; }
        public string DepartmentCode { get; set; }

        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string UserRole { get; set; } // "Admin", "User"

    }
}
