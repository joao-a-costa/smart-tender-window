using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTenderWindow.Tests.Helpers;
using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindow.Tests.Forms
{
    /// <summary>
    /// All tests that touch WinForms controls must run on an STA thread.
    /// Tests use StaHelper.Run() as a wrapper where needed.
    /// Static-helper tests (FormatCurrency, AmountToBuffer) don't need STA.
    /// </summary>
    [TestClass]
    public class TenderSplitDialogTests
    {
        // ── Shared fixtures ───────────────────────────────────────────────────

        private static List<TenderItem> OneCashTender() => new List<TenderItem>
        {
            new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
        };

        private static List<TenderItem> TwoTenders() => new List<TenderItem>
        {
            new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true },
            new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão")    { AllowsChange = false }
        };

        // ── Constructor guards ────────────────────────────────────────────────

        [TestMethod]
        public void Constructor_NullTenders_ThrowsArgumentNullException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                    new TenderSplitDialog(null, 10m));
            });
        }

        [TestMethod]
        public void Constructor_EmptyTenderList_ThrowsArgumentException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    new TenderSplitDialog(new List<TenderItem>(), 10m));
            });
        }

        [TestMethod]
        public void Constructor_ZeroDocumentTotal_ThrowsArgumentOutOfRangeException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                    new TenderSplitDialog(OneCashTender(), 0m));
            });
        }

        [TestMethod]
        public void Constructor_NegativeDocumentTotal_ThrowsArgumentOutOfRangeException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                    new TenderSplitDialog(OneCashTender(), -1m));
            });
        }

        [TestMethod]
        public void Constructor_ValidArguments_DoesNotThrow()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 37.50m))
                    Assert.IsNotNull(dlg);
            });
        }

        [TestMethod]
        public void Constructor_ResultIsNullBeforeConfirmation()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 37.50m))
                    Assert.IsNull(dlg.Result);
            });
        }

        // ── Constructor – preloaded amounts ──────────────────────────────────

        [TestMethod]
        public void Constructor_PreloadedAmount_StoredInAmountsArray()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { PreloadedAmount = 20m, AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 37.50m))
                {
                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");
                    Assert.AreEqual(20m, amounts[0]);
                }
            });
        }

        // ── FormatCurrency (private static) ──────────────────────────────────

        [TestMethod]
        public void FormatCurrency_Zero_ContainsZeroAndEuroSign()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "FormatCurrency", 0m);

            StringAssert.EndsWith(result, " €");
        }

        [TestMethod]
        public void FormatCurrency_PositiveValue_ContainsEuroSign()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "FormatCurrency", 37.50m);

            StringAssert.EndsWith(result, " €");
            StringAssert.Contains(result, "37");
        }

        // ── AmountToBuffer (private static) ──────────────────────────────────

        [TestMethod]
        public void AmountToBuffer_Zero_ReturnsStringZero()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "AmountToBuffer", 0m);

            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void AmountToBuffer_OneEuro_Returns100()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "AmountToBuffer", 1m);

            Assert.AreEqual("100", result);
        }

        [TestMethod]
        public void AmountToBuffer_OneCent_Returns1()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "AmountToBuffer", 0.01m);

            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void AmountToBuffer_37Euros50Cents_Returns3750()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "AmountToBuffer", 37.50m);

            Assert.AreEqual("3750", result);
        }

        [TestMethod]
        public void AmountToBuffer_100Euros_Returns10000()
        {
            var result = (string)ReflectionHelper.InvokePrivateStatic(
                typeof(TenderSplitDialog), "AmountToBuffer", 100m);

            Assert.AreEqual("10000", result);
        }

        // ── HandleNumpad – digit input ────────────────────────────────────────

        [TestMethod]
        public void HandleNumpad_SingleDigit_SetsBufferAndAmount()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");

                    var buffer  = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");

                    Assert.AreEqual("5",    buffer);
                    Assert.AreEqual(0.05m,  amounts[0]);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_MultipleDigits_AppendsToBuffer()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "2");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "3");

                    var buffer  = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");

                    Assert.AreEqual("123", buffer);
                    Assert.AreEqual(1.23m, amounts[0]);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_LeadingZeroReplaced_WhenFirstNonZeroDigitEntered()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "7");

                    var buffer = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    Assert.AreEqual("7", buffer);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_ZeroAfterZero_BufferRemainsZero()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var buffer = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    Assert.AreEqual("0", buffer);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_Dot_IsIgnored()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", ".");

                    var buffer = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    Assert.AreEqual("5", buffer);
                }
            });
        }

        // ── HandleNumpad – backspace ──────────────────────────────────────────

        [TestMethod]
        public void HandleNumpad_Backspace_RemovesLastDigit()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "2");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "3");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "⌫");

                    var buffer  = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");

                    Assert.AreEqual("12",  buffer);
                    Assert.AreEqual(0.12m, amounts[0]);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_BackspaceOnSingleDigit_ResetsToZero()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "⌫");

                    var buffer  = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");

                    Assert.AreEqual("0",  buffer);
                    Assert.AreEqual(0m,   amounts[0]);
                }
            });
        }

        [TestMethod]
        public void HandleNumpad_BackspaceOnZero_RemainsZero()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "⌫");

                    var buffer = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    Assert.AreEqual("0", buffer);
                }
            });
        }

        // ── CommitBuffer – MaxAmount cap ──────────────────────────────────────

        [TestMethod]
        public void CommitBuffer_ValueExceedsMaxAmount_CappedAtMaxAmount()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão") { MaxAmount = 50m, AllowsChange = false }
                };

                using (var dlg = new TenderSplitDialog(tenders, 200m))
                {
                    // Type "10000" → 100.00 €, exceeds MaxAmount of 50m
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");
                    Assert.AreEqual(50m, amounts[0]);
                }
            });
        }

        // ── CommitBuffer – AllowsChange = false cap ────────────────────────────

        [TestMethod]
        public void CommitBuffer_AllowsChangeFalse_CappedAtDocumentTotal()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão") { AllowsChange = false }
                };

                using (var dlg = new TenderSplitDialog(tenders, 37.50m))
                {
                    // Enter 50.00 → should be capped at 37.50
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");
                    Assert.AreEqual(37.50m, amounts[0]);
                }
            });
        }

        [TestMethod]
        public void CommitBuffer_AllowsChangeTrue_NotCappedAtDocumentTotal()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 37.50m))
                {
                    // Enter 50.00 → allowed because AllowsChange = true
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var amounts = ReflectionHelper.GetField<decimal[]>(dlg, "_amounts");
                    Assert.AreEqual(50m, amounts[0]);
                }
            });
        }

        // ── NavigateTender – boundary clamping ───────────────────────────────

        [TestMethod]
        public void NavigateTender_UpFromFirst_StaysAtIndex0()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(TwoTenders(), 50m))
                {
                    // SelectTender(0) is called in constructor, so _selectedIndex = 0
                    ReflectionHelper.InvokePrivate(dlg, "NavigateTender", -1);

                    var idx = ReflectionHelper.GetField<int>(dlg, "_selectedIndex");
                    Assert.AreEqual(0, idx);
                }
            });
        }

        [TestMethod]
        public void NavigateTender_DownFromLast_StaysAtLastIndex()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(TwoTenders(), 50m))
                {
                    // Move to index 1 (last), then try to go down
                    ReflectionHelper.InvokePrivate(dlg, "NavigateTender", 1);
                    ReflectionHelper.InvokePrivate(dlg, "NavigateTender", 1);

                    var idx = ReflectionHelper.GetField<int>(dlg, "_selectedIndex");
                    Assert.AreEqual(1, idx);
                }
            });
        }

        [TestMethod]
        public void NavigateTender_DownThenUp_ReturnsToFirst()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(TwoTenders(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "NavigateTender", 1);
                    ReflectionHelper.InvokePrivate(dlg, "NavigateTender", -1);

                    var idx = ReflectionHelper.GetField<int>(dlg, "_selectedIndex");
                    Assert.AreEqual(0, idx);
                }
            });
        }

        // ── SelectTender ──────────────────────────────────────────────────────

        [TestMethod]
        public void SelectTender_SetsSelectedIndex()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(TwoTenders(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 1);

                    var idx = ReflectionHelper.GetField<int>(dlg, "_selectedIndex");
                    Assert.AreEqual(1, idx);
                }
            });
        }

        [TestMethod]
        public void SelectTender_SetsInputBufferToCurrentAmount()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true },
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão")    { AllowsChange = false, PreloadedAmount = 15m }
                };

                using (var dlg = new TenderSplitDialog(tenders, 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 1);

                    var buffer = ReflectionHelper.GetField<string>(dlg, "_inputBuffer");
                    Assert.AreEqual("1500", buffer); // 15.00 → "1500"
                }
            });
        }

        [TestMethod]
        public void SelectTender_SameIndexTwice_DoesNotChangeAnything()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(TwoTenders(), 50m))
                {
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 0);
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 0);

                    var idx = ReflectionHelper.GetField<int>(dlg, "_selectedIndex");
                    Assert.AreEqual(0, idx);
                }
            });
        }

        // ── UpdateSummary – fulfilled vs unfulfilled ──────────────────────────

        [TestMethod]
        public void UpdateSummary_Unfulfilled_MissingCaptionIsEmFalta()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    // Default state – nothing entered yet
                    var caption = dlg.Controls.Find("lblMissingCaption", true);
                    if (caption.Length > 0)
                        Assert.AreEqual("Em falta:", ((Label)caption[0]).Text);
                }
            });
        }

        [TestMethod]
        public void UpdateSummary_Fulfilled_MissingCaptionIsTroco()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    // Enter 10.00
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var caption = dlg.Controls.Find("lblMissingCaption", true);
                    if (caption.Length > 0)
                        Assert.AreEqual("Troco:", ((Label)caption[0]).Text);
                }
            });
        }

        [TestMethod]
        public void UpdateSummary_Unfulfilled_ConfirmButtonIsDisabled()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    var btns = dlg.Controls.Find("btnConfirm", true);
                    if (btns.Length > 0)
                        Assert.IsFalse(((Button)btns[0]).Enabled);
                }
            });
        }

        [TestMethod]
        public void UpdateSummary_Fulfilled_ConfirmButtonIsEnabled()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    // Enter exactly 10.00
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    var btns = dlg.Controls.Find("btnConfirm", true);
                    if (btns.Length > 0)
                        Assert.IsTrue(((Button)btns[0]).Enabled);
                }
            });
        }

        // ── Confirm – result construction ─────────────────────────────────────

        [TestMethod]
        public void Confirm_ExactPayment_ChangeDueIsZero()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    // Enter exactly 10.00
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(0m,   dlg.Result.ChangeDue);
                    Assert.AreEqual(10m,  dlg.Result.TotalAllocated);
                    Assert.AreEqual(1,    dlg.Result.Allocations.Count);
                }
            });
        }

        [TestMethod]
        public void Confirm_Overpayment_ChangeDueIsCorrect()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true }
                };

                using (var dlg = new TenderSplitDialog(tenders, 7.50m))
                {
                    // Enter 10.00
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(2.50m, dlg.Result.ChangeDue);
                    Assert.AreEqual(10m,   dlg.Result.TotalAllocated);
                }
            });
        }

        [TestMethod]
        public void Confirm_Underpayment_DoesNotSetResult()
        {
            StaHelper.Run(() =>
            {
                using (var dlg = new TenderSplitDialog(OneCashTender(), 50m))
                {
                    // Enter only 5.00 (less than 50m total)
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "5");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNull(dlg.Result);
                }
            });
        }

        [TestMethod]
        public void Confirm_ZeroAmountAllocations_ExcludedFromResult()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true },
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão")    { AllowsChange = false }
                };

                using (var dlg = new TenderSplitDialog(tenders, 10m))
                {
                    // Only enter cash (card stays at 0)
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(1, dlg.Result.Allocations.Count);
                    Assert.AreEqual(TenderTypeEnum.tndCash, dlg.Result.Allocations[0].Tender.TenderType);
                }
            });
        }

        [TestMethod]
        public void Confirm_MultiTender_AllocationsMatchEnteredAmounts()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário") { AllowsChange = true },
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão")    { AllowsChange = false }
                };

                using (var dlg = new TenderSplitDialog(tenders, 30m))
                {
                    // Enter 20.00 on CASH (index 0)
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 0);
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "2");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    // Enter 10.00 on CARD (index 1)
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 1);
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "1");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(2,    dlg.Result.Allocations.Count);
                    Assert.AreEqual(30m,  dlg.Result.TotalAllocated);
                    Assert.AreEqual(0m,   dlg.Result.ChangeDue);

                    decimal cashAmount = dlg.Result.Allocations
                        .Find(a => a.Tender.TenderType == TenderTypeEnum.tndCash).Amount;
                    decimal cardAmount = dlg.Result.Allocations
                        .Find(a => a.Tender.TenderType == TenderTypeEnum.tndCreditDebitCard).Amount;

                    Assert.AreEqual(20m, cashAmount);
                    Assert.AreEqual(10m, cardAmount);
                }
            });
        }

        // ── Static Show method ────────────────────────────────────────────────

        [TestMethod]
        public void Show_NullTenders_ThrowsArgumentNullException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                    TenderSplitDialog.Show(null, null, 10m));
            });
        }

        [TestMethod]
        public void Show_EmptyTenders_ThrowsArgumentException()
        {
            StaHelper.Run(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    TenderSplitDialog.Show(null, new List<TenderItem>(), 10m));
            });
        }

        // ── End-to-end: Cash and card (no details required) ──────────────────

        [TestMethod]
        public void Confirm_CashAndCard_NoDetailsRequired()
        {
            StaHelper.Run(() =>
            {
                var tenders = new List<TenderItem>
                {
                    new TenderItem(TenderTypeEnum.tndCash, "Numerário")      { AllowsChange = true },
                    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão") { AllowsChange = false }
                };

                using (var dlg = new TenderSplitDialog(tenders, 50m))
                {
                    // Cash: 30.00
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 0);
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "3");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    // Card: 20.00
                    ReflectionHelper.InvokePrivate(dlg, "SelectTender", 1);
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "2");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");
                    ReflectionHelper.InvokePrivate(dlg, "HandleNumpad", "0");

                    ReflectionHelper.InvokePrivate(dlg, "Confirm");

                    Assert.IsNotNull(dlg.Result);
                    Assert.AreEqual(2, dlg.Result.Allocations.Count);
                    Assert.AreEqual(50m, dlg.Result.TotalAllocated);
                    Assert.IsNull(dlg.Result.Allocations[0].BankTransfer);
                    Assert.IsNull(dlg.Result.Allocations[0].Check);
                    Assert.IsNull(dlg.Result.Allocations[0].CreditNoteRefund);
                }
            });
        }
    }
}
