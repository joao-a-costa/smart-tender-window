using System;
using System.Collections.Generic;

namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Represents a single payment tender option shown in the split dialog.
    /// </summary>
    public class TenderItem
    {
        /// <summary>
        /// Payment method. Nullable so callers using the default constructor may
        /// leave it unset, but every tender line that is allocated an amount is
        /// expected to carry a type.
        /// </summary>
        public TenderTypeEnum? TenderType { get; set; }

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

        // ── Caller-supplied drop-down sources for the detail popups ────────────

        /// <summary>Beneficiary bank accounts (1st drop-down of the bank-transfer popup).</summary>
        public List<TenderOption> BeneficiaryAccounts { get; set; }

        /// <summary>Party bank accounts (2nd drop-down of the bank-transfer popup).</summary>
        public List<TenderOption> PartyAccounts { get; set; }

        /// <summary>Banks (drop-down of the check popup).</summary>
        public List<TenderOption> Banks { get; set; }

        /// <summary>Document series (drop-down of the credit-note-refund popup).</summary>
        public List<TenderOption> Series { get; set; }

        public TenderItem() { }

        public TenderItem(TenderTypeEnum tenderType, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            TenderType = tenderType;
            Name = name;
        }
    }
}
