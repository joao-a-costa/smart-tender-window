using System.Collections.Generic;

namespace SmartTenderWindowTenderSplit.Models
{
    public class TenderSplitResult
    {
        /// <summary>Only tenders with Amount > 0 are included.</summary>
        public List<TenderAllocation> Allocations { get; set; }

        public decimal TotalAllocated { get; set; }

        /// <summary>Amount owed back to the customer. Zero when no overpayment.</summary>
        public decimal ChangeDue { get; set; }
    }
}
