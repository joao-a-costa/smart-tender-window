namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Extra data captured for a credit-note refund ("Nota de Crédito-Reembolso"),
    /// represented by <see cref="TenderTypeEnum.tndVoucher"/>.
    /// Field names map to the Sage 50c <c>TenderVoucher</c> object.
    /// </summary>
    public class CreditNoteRefundDetails
    {
        /// <summary>Document series ("Série") → <c>TransSerial</c>.</summary>
        public string TransSerial { get; set; }

        /// <summary>Document type ("Documento") → <c>TransDocument</c>.</summary>
        public string TransDocument { get; set; }

        /// <summary>Document number ("N.º Documento") → <c>TransDocNumber</c>.</summary>
        public long TransDocNumber { get; set; }

        /// <summary>Available amount ("Valor disponível") → <c>TotalAmount</c>.</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Amount to discount ("Valor a descontar") → <c>SpentValueAmount</c>.</summary>
        public decimal SpentValueAmount { get; set; }
    }
}
