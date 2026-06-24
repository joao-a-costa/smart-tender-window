using System;

namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Represents a single payment tender option shown in the split dialog.
    /// </summary>
    public class TenderItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        /// <summary>Amount pre-filled by the caller. User can change it.</summary>
        public decimal PreloadedAmount { get; set; }

        /// <summary>Optional hard cap per tender. Null means no limit.</summary>
        public decimal? MaxAmount { get; set; }

        /// <summary>
        /// When true this tender may overpay (e.g. Cash) and the excess becomes change.
        /// When false the allocated value cannot push the running total above the document total.
        /// </summary>
        public bool AllowsChange { get; set; }

        public TenderItem() { }

        public TenderItem(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            Id = id;
            Name = name;
        }
    }
}
