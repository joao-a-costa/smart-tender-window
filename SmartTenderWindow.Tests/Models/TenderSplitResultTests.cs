using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindow.Tests.Models
{
    [TestClass]
    public class TenderSplitResultTests
    {
        [TestMethod]
        public void DefaultConstructor_AllPropertiesAreDefaultValues()
        {
            var result = new TenderSplitResult();

            Assert.IsNull(result.Allocations);
            Assert.AreEqual(0m, result.TotalAllocated);
            Assert.AreEqual(0m, result.ChangeDue);
        }

        [TestMethod]
        public void Properties_SetAndGetCorrectly()
        {
            var tender = new TenderItem(TenderTypeEnum.tndCash, "Numerário");
            var allocs = new List<TenderAllocation>
            {
                new TenderAllocation { Tender = tender, Amount = 40m }
            };

            var result = new TenderSplitResult
            {
                Allocations    = allocs,
                TotalAllocated = 40m,
                ChangeDue      = 2.50m
            };

            Assert.AreEqual(1,     result.Allocations.Count);
            Assert.AreEqual(40m,   result.TotalAllocated);
            Assert.AreEqual(2.50m, result.ChangeDue);
        }

        [TestMethod]
        public void ChangeDue_CanBeZero_WhenExactPayment()
        {
            var result = new TenderSplitResult
            {
                TotalAllocated = 37.50m,
                ChangeDue      = 0m
            };

            Assert.AreEqual(0m, result.ChangeDue);
        }

        [TestMethod]
        public void Allocations_CanContainMultipleEntries()
        {
            var allocs = new List<TenderAllocation>
            {
                new TenderAllocation { Tender = new TenderItem(TenderTypeEnum.tndCash, "Numerário"), Amount = 20m },
                new TenderAllocation { Tender = new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão"), Amount = 17.50m }
            };

            var result = new TenderSplitResult
            {
                Allocations    = allocs,
                TotalAllocated = 37.50m,
                ChangeDue      = 0m
            };

            Assert.AreEqual(2, result.Allocations.Count);
            Assert.AreEqual(20m,    result.Allocations[0].Amount);
            Assert.AreEqual(17.50m, result.Allocations[1].Amount);
        }

        [TestMethod]
        public void TotalAllocated_ReflectsValueSet()
        {
            var result = new TenderSplitResult { TotalAllocated = 150.75m };
            Assert.AreEqual(150.75m, result.TotalAllocated);
        }
    }
}
