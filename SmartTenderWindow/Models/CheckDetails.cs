using System;

namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Extra data captured for a <see cref="TenderTypeEnum.tndCheck"/> line.
    /// Field names map to the Sage 50c <c>TenderCheck</c> object.
    /// </summary>
    public class CheckDetails
    {
        /// <summary>Check number ("Nº do Cheque", mandatory) → <c>CheckSequenceNumber</c>.</summary>
        public string CheckSequenceNumber { get; set; }

        /// <summary>Bank ("Banco") → <c>BankID</c>.</summary>
        public string BankId { get; set; }

        /// <summary>Check date ("Data") → <c>CheckDeferredDate</c>.</summary>
        public DateTime CheckDeferredDate { get; set; }

        /// <summary>Check amount ("Valor") → <c>CheckAmount</c>.</summary>
        public decimal CheckAmount { get; set; }
    }
}
