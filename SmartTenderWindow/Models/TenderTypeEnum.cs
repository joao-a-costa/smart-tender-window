namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// Payment / tender method. Numeric values mirror the Sage 50c tender codes
    /// and must not be changed.
    /// </summary>
    public enum TenderTypeEnum
    {
        tndCash = 0,
        tndChange = 1,
        tndCreditDebitCard = 2,
        tndCheck = 4,
        tndVoucher = 5,
        tndCustomerAccount = 6,
        tndCoupon = 7,
        tndBankReceipt = 8,
        tndBillOfExchange = 9,
        tndDirectDebiting = 10,
        tndBankWireTransfer = 11,
        tndBalanceCompensation = 13,
        tndRefMB = 14,
        tndMBWay = 15
    }
}
