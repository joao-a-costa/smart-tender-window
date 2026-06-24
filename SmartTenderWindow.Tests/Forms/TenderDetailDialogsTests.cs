using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTenderWindow.Tests.Helpers;
using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindow.Tests.Forms
{
    /// <summary>
    /// Tests for the tender-type detail popups and the way the main dialog
    /// requires / returns the captured details. All WinForms work runs on STA.
    /// </summary>
    [TestClass]
    public class TenderDetailDialogsTests
    {
        // ── RequiresPopup ─────────────────────────────────────────────────────

        [TestMethod]
        public void RequiresPopup_TrueForWireTransferCheckAndVoucher()
        {
            Assert.IsTrue(RequiresPopup(TenderTypeEnum.tndBankWireTransfer));
            Assert.IsTrue(RequiresPopup(TenderTypeEnum.tndCheck));
            Assert.IsTrue(RequiresPopup(TenderTypeEnum.tndVoucher));
        }

        [TestMethod]
        public void RequiresPopup_FalseForCashCardMBWayRefMB()
        {
            Assert.IsFalse(RequiresPopup(TenderTypeEnum.tndCash));
            Assert.IsFalse(RequiresPopup(TenderTypeEnum.tndCreditDebitCard));
            Assert.IsFalse(RequiresPopup(TenderTypeEnum.tndMBWay));
            Assert.IsFalse(RequiresPopup(TenderTypeEnum.tndRefMB));
            Assert.IsFalse(RequiresPopup(null));
        }

        private static bool RequiresPopup(TenderTypeEnum? type)
            => (bool)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "RequiresPopup", type);

        // ── FirstMissingDetailIndex ───────────────────────────────────────────

        [TestMethod]
        public void FirstMissingDetailIndex_CheckWithAmountButNoDetails_ReturnsItsIndex()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCheck, "Cheque") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SetAmount", 0, 10m);

                    int idx = (int)ReflectionHelper.InvokePrivate(dlg, "FirstMissingDetailIndex");
                    Assert.AreEqual(0, idx);
                }
            });
        }

        [TestMethod]
        public void FirstMissingDetailIndex_CashWithAmount_ReturnsMinusOne()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SetAmount", 0, 10m);

                    int idx = (int)ReflectionHelper.InvokePrivate(dlg, "FirstMissingDetailIndex");
                    Assert.AreEqual(-1, idx);
                }
            });
        }

        [TestMethod]
        public void FirstMissingDetailIndex_CheckWithDetailsPresent_ReturnsMinusOne()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCheck, "Cheque") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SetAmount", 0, 10m);
                    var details = ReflectionHelper.GetField<object[]>(dlg, "_details");
                    details[0] = new CheckDetails { CheckSequenceNumber = "12345", CheckAmount = 10m };

                    int idx = (int)ReflectionHelper.InvokePrivate(dlg, "FirstMissingDetailIndex");
                    Assert.AreEqual(-1, idx);
                }
            });
        }

        // ── Confirm returns captured details ──────────────────────────────────

        [TestMethod]
        public void Confirm_CheckWithDetails_AllocationCarriesCheckDetails()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCheck, "Cheque") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SetAmount", 0, 10m);
                    var details = ReflectionHelper.GetField<object[]>(dlg, "_details");
                    details[0] = new CheckDetails { CheckSequenceNumber = "12345", CheckAmount = 10m };

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(1, dlg.Result.Allocations.Count);
                    var check = dlg.Result.Allocations[0].Check;
                    Assert.IsNotNull(check);
                    Assert.AreEqual("12345", check.CheckSequenceNumber);
                    Assert.IsNull(dlg.Result.Allocations[0].BankTransfer);
                    Assert.IsNull(dlg.Result.Allocations[0].CreditNoteRefund);
                }
            });
        }

        // ── ClearTender (cancel popup) ────────────────────────────────────────

        [TestMethod]
        public void ClearTender_ResetsAmountAndDetails()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCheck, "Cheque") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SetAmount", 0, 10m);
                    var details = ReflectionHelper.GetField<object[]>(dlg, "_details");
                    details[0] = new CheckDetails { CheckSequenceNumber = "12345" };

                    ReflectionHelper.InvokePrivate(dlg, "ClearTender", 0);

                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");
                    details = ReflectionHelper.GetField<object[]>(dlg, "_details");
                    Assert.AreEqual(0m, amounts[0]);
                    Assert.IsNull(details[0]);
                }
            });
        }

        // ── Popup result building ─────────────────────────────────────────────

        [TestMethod]
        public void BankTransferDialog_DefaultSelectsFirstOption()
        {
            StaHelper.Run(() =>
            {
                var tender = new TenderItem(TenderTypeEnum.tndBankWireTransfer, "Transferência")
                {
                    BeneficiaryAccounts = new List<TenderOption> { new TenderOption("1", "BCP") },
                    PartyAccounts       = new List<TenderOption> { new TenderOption("9", "Cliente") }
                };

                using (var dlg = new BankTransferDialog(tender, null))
                {
                    ReflectionHelper.InvokePrivate(dlg, "OnOk");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual("1", dlg.Result.BankAccountId);
                    Assert.AreEqual("9", dlg.Result.PartyBankAccountId);
                }
            });
        }

        [TestMethod]
        public void CheckDialog_EmptyCheckNumber_DoesNotProduceResult()
        {
            StaHelper.Run(() =>
            {
                var tender = new TenderItem(TenderTypeEnum.tndCheck, "Cheque");

                using (var dlg = new CheckDialog(tender, 10m, null))
                {
                    // OnOk should bail out because the mandatory check number is empty.
                    ReflectionHelper.InvokePrivate(dlg, "OnOk");
                    Assert.IsNull(dlg.Result);
                }
            });
        }

        [TestMethod]
        public void CreditNoteRefundDialog_PrefillsSpentWithAllocationAmount()
        {
            StaHelper.Run(() =>
            {
                var tender = new TenderItem(TenderTypeEnum.tndVoucher, "Nota de Crédito")
                {
                    Series = new List<TenderOption> { new TenderOption("1", "1") }
                };

                using (var dlg = new CreditNoteRefundDialog(tender, 25m, null))
                {
                    ReflectionHelper.InvokePrivate(dlg, "OnOk");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(25m, dlg.Result.SpentValueAmount);
                    Assert.AreEqual("Nota de Crédito-Reembolso", dlg.Result.TransDocument);
                    Assert.AreEqual("1", dlg.Result.TransSerial);
                }
            });
        }
    }
}
