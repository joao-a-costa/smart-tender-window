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
            var tender = new TenderItem("CASH", "Numerário");
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
            var first  = new TenderItem("CASH", "Numerário");
            var second = new TenderItem("CARD", "Cartão");
            var alloc  = new TenderAllocation { Tender = first };

            alloc.Tender = second;

            Assert.AreEqual(second, alloc.Tender);
        }
    }
}
