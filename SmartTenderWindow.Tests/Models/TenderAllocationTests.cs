using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindow.Tests.Models
{
    [TestClass]
    public class TenderAllocationTests
    {
        [TestMethod]
        public void DefaultConstructor_TenderIsNullAndAmountIsZero()
        {
            var alloc = new TenderAllocation();

            Assert.IsNull(alloc.Tender);
            Assert.AreEqual(0m, alloc.Amount);
        }

        [TestMethod]
        public void Properties_SetAndGetCorrectly()
        {
            var tender = new TenderItem(TenderTypeEnum.tndCash, "Numerário");
            var alloc  = new TenderAllocation { Tender = tender, Amount = 37.50m };

            Assert.AreEqual(tender, alloc.Tender);
            Assert.AreEqual(37.50m, alloc.Amount);
        }

        [TestMethod]
        public void Amount_CanBeZero()
        {
            var alloc = new TenderAllocation { Amount = 0m };
            Assert.AreEqual(0m, alloc.Amount);
        }

        [TestMethod]
        public void Amount_CanBeHighPrecisionDecimal()
        {
            var alloc = new TenderAllocation { Amount = 999.99m };
            Assert.AreEqual(999.99m, alloc.Amount);
        }

        [TestMethod]
        public void Tender_CanBeReassigned()
        {
            var first  = new TenderItem(TenderTypeEnum.tndCash, "Numerário");
            var second = new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão");
            var alloc  = new TenderAllocation { Tender = first };

            alloc.Tender = second;

            Assert.AreEqual(second, alloc.Tender);
        }

        [TestMethod]
        public void BankTransfer_DefaultsToNull()
        {
            var alloc = new TenderAllocation();
            Assert.IsNull(alloc.BankTransfer);
        }

        [TestMethod]
        public void BankTransfer_CanBeSetAndRetrieved()
        {
            var details = new BankTransferDetails
            {
                BankAccountId = "ACCT001",
                PartyBankAccountId = "PARTY001",
                ContractReferenceNumber = "REF123"
            };
            var alloc = new TenderAllocation { BankTransfer = details };

            Assert.AreEqual(details, alloc.BankTransfer);
            Assert.AreEqual("ACCT001", alloc.BankTransfer.BankAccountId);
        }

        [TestMethod]
        public void Check_DefaultsToNull()
        {
            var alloc = new TenderAllocation();
            Assert.IsNull(alloc.Check);
        }

        [TestMethod]
        public void Check_CanBeSetAndRetrieved()
        {
            var details = new CheckDetails
            {
                CheckSequenceNumber = "CHK12345",
                CheckAmount = 100m,
                BankId = "BANK001"
            };
            var alloc = new TenderAllocation { Check = details };

            Assert.AreEqual(details, alloc.Check);
            Assert.AreEqual("CHK12345", alloc.Check.CheckSequenceNumber);
        }

        [TestMethod]
        public void CreditNoteRefund_DefaultsToNull()
        {
            var alloc = new TenderAllocation();
            Assert.IsNull(alloc.CreditNoteRefund);
        }

        [TestMethod]
        public void CreditNoteRefund_CanBeSetAndRetrieved()
        {
            var details = new CreditNoteRefundDetails
            {
                TransSerial = "S1",
                TransDocument = "Nota de Crédito-Reembolso",
                TransDocNumber = 1000,
                TotalAmount = 50m,
                SpentValueAmount = 25m
            };
            var alloc = new TenderAllocation { CreditNoteRefund = details };

            Assert.AreEqual(details, alloc.CreditNoteRefund);
            Assert.AreEqual(50m, alloc.CreditNoteRefund.TotalAmount);
        }

        [TestMethod]
        public void Multiple_DetailTypes_CanCoexist_OnlyOneIsPopulated()
        {
            var bankDetails = new BankTransferDetails { BankAccountId = "ACCT001" };
            var alloc = new TenderAllocation
            {
                BankTransfer = bankDetails,
                Check = null,
                CreditNoteRefund = null
            };

            Assert.IsNotNull(alloc.BankTransfer);
            Assert.IsNull(alloc.Check);
            Assert.IsNull(alloc.CreditNoteRefund);
        }
    }
}
