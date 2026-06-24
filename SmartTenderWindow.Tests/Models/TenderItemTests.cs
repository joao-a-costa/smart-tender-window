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
        public void Constructor_ValidArguments_SetsIdAndName()
        {
            var item = new TenderItem("CASH", "Numerário");

            Assert.AreEqual("CASH", item.Id);
            Assert.AreEqual("Numerário", item.Name);
        }

        [TestMethod]
        public void Constructor_ValidArguments_PreloadedAmountDefaultsToZero()
        {
            var item = new TenderItem("CASH", "Numerário");

            Assert.AreEqual(0m, item.PreloadedAmount);
        }

        [TestMethod]
        public void Constructor_ValidArguments_MaxAmountDefaultsToNull()
        {
            var item = new TenderItem("CASH", "Numerário");

            Assert.IsNull(item.MaxAmount);
        }

        [TestMethod]
        public void Constructor_ValidArguments_AllowsChangeDefaultsToFalse()
        {
            var item = new TenderItem("CASH", "Numerário");

            Assert.IsFalse(item.AllowsChange);
        }

        // ── Constructor – invalid Id ──────────────────────────────────────────

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullId_ThrowsArgumentException()
            => new TenderItem(null, "Numerário");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyId_ThrowsArgumentException()
            => new TenderItem("", "Numerário");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WhitespaceId_ThrowsArgumentException()
            => new TenderItem("   ", "Numerário");

        // ── Constructor – invalid Name ────────────────────────────────────────

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullName_ThrowsArgumentException()
            => new TenderItem("CASH", null);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName_ThrowsArgumentException()
            => new TenderItem("CASH", "");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WhitespaceName_ThrowsArgumentException()
            => new TenderItem("CASH", "   ");

        // ── Default constructor ───────────────────────────────────────────────

        [TestMethod]
        public void DefaultConstructor_AllPropertiesAreDefaultValues()
        {
            var item = new TenderItem();

            Assert.IsNull(item.Id);
            Assert.IsNull(item.Name);
            Assert.AreEqual(0m, item.PreloadedAmount);
            Assert.IsNull(item.MaxAmount);
            Assert.IsFalse(item.AllowsChange);
        }

        // ── Property setters ──────────────────────────────────────────────────

        [TestMethod]
        public void Properties_CanBeSetAfterConstruction()
        {
            var item = new TenderItem("CARD", "Cartão")
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
            var item = new TenderItem("CASH", "Numerário") { AllowsChange = true };
            Assert.IsTrue(item.AllowsChange);
        }

        [TestMethod]
        public void MaxAmount_CanBeSetToZero()
        {
            var item = new TenderItem("CASH", "Numerário") { MaxAmount = 0m };
            Assert.AreEqual(0m, item.MaxAmount);
        }

        [TestMethod]
        public void PreloadedAmount_CanBeSetToPositiveValue()
        {
            var item = new TenderItem("CASH", "Numerário") { PreloadedAmount = 100.50m };
            Assert.AreEqual(100.50m, item.PreloadedAmount);
        }
    }
}
