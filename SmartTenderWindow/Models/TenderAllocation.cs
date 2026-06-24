namespace SmartTenderWindowTenderSplit.Models
{
    public class TenderAllocation
    {
        public TenderItem Tender { get; set; }
        public decimal Amount { get; set; }

        /// <summary>Set when <see cref="Tender"/> is a bank wire transfer. Otherwise null.</summary>
        public BankTransferDetails BankTransfer { get; set; }

        /// <summary>Set when <see cref="Tender"/> is a check. Otherwise null.</summary>
        public CheckDetails Check { get; set; }

        /// <summary>Set when <see cref="Tender"/> is a credit-note refund (voucher). Otherwise null.</summary>
        public CreditNoteRefundDetails CreditNoteRefund { get; set; }
    }
}
