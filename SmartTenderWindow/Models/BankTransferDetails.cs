using System;

namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Extra data captured for a <see cref="TenderTypeEnum.tndBankWireTransfer"/> line.
    /// Field names map to the Sage 50c <c>TenderWireTransfer</c> object.
    /// </summary>
    public class BankTransferDetails
    {
        /// <summary>Beneficiary bank account ("Conta beneficiária") → <c>BankAccount</c>.</summary>
        public string BankAccountId { get; set; }

        /// <summary>Party bank account (second drop-down) → <c>PartyBankAccount</c>.</summary>
        public string PartyBankAccountId { get; set; }

        /// <summary>Transfer reference ("Referência") → <c>ContractReferenceNumber</c>.</summary>
        public string ContractReferenceNumber { get; set; }

        /// <summary>Value date ("Data Valor") → <c>AccountingDate</c>.</summary>
        public DateTime AccountingDate { get; set; }
    }
}
