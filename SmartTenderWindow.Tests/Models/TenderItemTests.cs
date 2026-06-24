using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindow.Tests.Models
{
    [TestClass]
    public class TenderItemTests
    {
        // ── Constructor – valid ───────────────────────────────────────────────

        [TestMethod]
        public void Constructor_ValidArguments_SetsTenderTypeAndName()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário");

            Assert.AreEqual(TenderTypeEnum.tndCash, item.TenderType);
            Assert.AreEqual("Numerário", item.Name);
        }

        [TestMethod]
        public void Constructor_ValidArguments_PreloadedAmountDefaultsToZero()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário");

            Assert.AreEqual(0m, item.PreloadedAmount);
        }

        [TestMethod]
        public void Constructor_ValidArguments_MaxAmountDefaultsToNull()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário");

            Assert.IsNull(item.MaxAmount);
        }

        [TestMethod]
        public void Constructor_ValidArguments_AllowsChangeDefaultsToFalse()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário");

            Assert.IsFalse(item.AllowsChange);
        }

        // ── Constructor – invalid Name ────────────────────────────────────────

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullName_ThrowsArgumentException()
            => new TenderItem(TenderTypeEnum.tndCash, null);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName_ThrowsArgumentException()
            => new TenderItem(TenderTypeEnum.tndCash, "");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WhitespaceName_ThrowsArgumentException()
            => new TenderItem(TenderTypeEnum.tndCash, "   ");

        // ── Default constructor ───────────────────────────────────────────────

        [TestMethod]
        public void DefaultConstructor_AllPropertiesAreDefaultValues()
        {
            var item = new TenderItem();

            Assert.IsNull(item.TenderType);
            Assert.IsNull(item.Name);
            Assert.AreEqual(0m, item.PreloadedAmount);
            Assert.IsNull(item.MaxAmount);
            Assert.IsFalse(item.AllowsChange);
            Assert.IsNull(item.BeneficiaryAccounts);
            Assert.IsNull(item.PartyAccounts);
            Assert.IsNull(item.Banks);
            Assert.IsNull(item.Series);
        }

        // ── Property setters ──────────────────────────────────────────────────

        [TestMethod]
        public void Properties_CanBeSetAfterConstruction()
        {
            var item = new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão")
            {
                PreloadedAmount = 50m,
                MaxAmount       = 500m,
                AllowsChange    = true
            };

            Assert.AreEqual(50m,  item.PreloadedAmount);
            Assert.AreEqual(500m, item.MaxAmount);
            Assert.IsTrue(item.AllowsChange);
        }

        [TestMethod]
        public void AllowsChange_CanBeSetToTrue()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true };
            Assert.IsTrue(item.AllowsChange);
        }

        [TestMethod]
        public void MaxAmount_CanBeSetToZero()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário") { MaxAmount = 0m };
            Assert.AreEqual(0m, item.MaxAmount);
        }

        [TestMethod]
        public void PreloadedAmount_CanBeSetToPositiveValue()
        {
            var item = new TenderItem(TenderTypeEnum.tndCash, "Numerário") { PreloadedAmount = 100.50m };
            Assert.AreEqual(100.50m, item.PreloadedAmount);
        }

        [TestMethod]
        public void TenderType_CanBeSetToNull()
        {
            var item = new TenderItem { TenderType = null };
            Assert.IsNull(item.TenderType);
        }
    }
}
