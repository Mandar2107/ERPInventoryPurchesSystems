using System;
using System.Collections.Generic;

namespace ERPInventoryPurchesSystems.Models.Reporting
{
    public class ReportViewModel
    {
        // Filters
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DepartmentCode { get; set; }
        public string VendorCode { get; set; }
        public string ItemCategory { get; set; }
        public List<string> StatusFilter { get; set; }

        // Metrics
        public int TotalPRs { get; set; }
        public int ApprovedPOs { get; set; }
        public decimal TotalSpend { get; set; }
        public double AverageDeliveryTime { get; set; }
        public double VendorPerformanceScore { get; set; }
        public int PendingInvoices { get; set; }

        // Chart Data
        public List<string> VendorNames { get; set; }
        public List<decimal> VendorSpend { get; set; }

        public List<string> Months { get; set; }
        public List<decimal> MonthlySpend { get; set; }

        public List<string> Categories { get; set; }
        public List<decimal> CategorySpend { get; set; }
    }
}
