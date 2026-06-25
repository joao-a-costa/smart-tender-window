using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindowTenderSplit.Forms
{
    public partial class TenderSplitDialog : Form
    {
        // ── Palette ──────────────────────────────────────────────────────────
        private static readonly Color ClrHeader   = Color.FromArgb(76, 175, 80);
        private static readonly Color ClrSelected = Color.FromArgb(200, 230, 201);
        private static readonly Color ClrError    = Color.FromArgb(211, 47, 47);
        private static readonly Color ClrDisabled = Color.FromArgb(180, 180, 180);

        // ── Data ─────────────────────────────────────────────────────────────
        private readonly decimal _documentTotal;
        private readonly List<TenderItem> _tenders;
        private readonly decimal[] _amounts;
        private int    _selectedIndex = -1;
        private string _inputBuffer   = "0";

        // Per-tender extra details captured via popups. Holds a BankTransferDetails,
        // CheckDetails or CreditNoteRefundDetails, or null when none / not applicable.
        private readonly object[] _details;

        private bool _updatingGrid = false;  // Flag to prevent recursive updates when programmatically changing grid cells

        public TenderSplitResult Result { get; private set; }

        // ── Constructor ──────────────────────────────────────────────────────

        public TenderSplitDialog(IEnumerable<TenderItem> tenders, decimal documentTotal)
        {
            if (tenders == null) throw new ArgumentNullException(nameof(tenders));
            if (documentTotal <= 0)
                throw new ArgumentOutOfRangeException(nameof(documentTotal), "O total do documento deve ser positivo.");

            _tenders = new List<TenderItem>(tenders);
            if (_tenders.Count == 0)
                throw new ArgumentException("A lista de meios de pagamento não pode estar vazia.", nameof(tenders));

            _documentTotal = documentTotal;
            _amounts       = new decimal[_tenders.Count];
            _details       = new object[_tenders.Count];
            for (int i = 0; i < _tenders.Count; i++)
                _amounts[i] = _tenders[i].PreloadedAmount;

            InitializeComponent();

            lblTotalValue.Text = FormatCurrency(_documentTotal);
            lblMissingValue.Text = FormatCurrency(_documentTotal);

            WireUpNumpadButtonHandlers();
            BuildTenderGrid();
            SelectTender(0);
        }

        // ── Static helper ────────────────────────────────────────────────────

        public static TenderSplitResult Show(
            IWin32Window owner,
            IEnumerable<TenderItem> tenders,
            decimal documentTotal,
            string title = null)
        {
            using (var dlg = new TenderSplitDialog(tenders, documentTotal))
            {
                if (title != null) dlg.Text = title;
                return dlg.ShowDialog(owner) == DialogResult.OK ? dlg.Result : null;
            }
        }

        // ── Numpad button setup ──────────────────────────────────────────────

        private void WireUpNumpadButtonHandlers()
        {
            // Disable TabStop on all buttons so they don't consume Enter/Tab and interfere with keyboard input
            var allButtons = new[] {
                btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnDot, btnBackspace, btnNumOk,
                btnNavUp, btnNavDown, btnConfirm, btnCancel
            };
            foreach (var btn in allButtons)
                btn.TabStop = false;

            var numpadButtons = new[] { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnDot, btnBackspace };
            foreach (var btn in numpadButtons)
                btn.KeyDown += NumpadButton_KeyDown;
        }

        // ── DataGridView setup ──────────────────────────────────────────────────

        private void BuildTenderGrid()
        {
            dgvTenders.AllowUserToDeleteRows = false;  // Prevent accidental row deletion

            // Style header
            dgvTenders.ColumnHeadersDefaultCellStyle.BackColor = ClrHeader;
            dgvTenders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTenders.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTenders.Font, FontStyle.Bold);

            // Style alternating rows
            dgvTenders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvTenders.DefaultCellStyle.BackColor = Color.White;

            dgvTenders.Rows.Clear();
            for (int i = 0; i < _tenders.Count; i++)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dgvTenders);
                row.Cells[0].Value = _tenders[i].Name;
                row.Cells[1].Value = FormatCurrency(_amounts[i]);
                dgvTenders.Rows.Add(row);
            }
            dgvTenders.CellValueChanged += DgvTenders_CellValueChanged;
            dgvTenders.SelectionChanged += DgvTenders_SelectionChanged;
            dgvTenders.PreviewKeyDown += DgvTenders_PreviewKeyDown;
            dgvTenders.KeyDown += DgvTenders_KeyDown;
        }

        private void DgvTenders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTenders.SelectedRows.Count > 0)
            {
                int index = dgvTenders.SelectedRows[0].Index;
                SelectTender(index);
            }
        }

        private void DgvTenders_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_updatingGrid || e.ColumnIndex != 1) return;  // Only care about Amount column

            int index = e.RowIndex;
            string cellValue = (dgvTenders.Rows[index].Cells[1].Value?.ToString() ?? "0").Trim();

            // Remove currency symbol if present
            cellValue = cellValue.Replace("€", "").Trim();

            // Normalize decimal separator: accept both comma and period
            cellValue = cellValue.Replace(",", ".");

            // Try to parse as decimal; if fails, reset to previous amount
            if (decimal.TryParse(cellValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
            {
                SetAmount(index, amount);
                UpdateSummary();
            }
            else
            {
                _updatingGrid = true;
                dgvTenders.Rows[index].Cells[1].Value = FormatCurrency(_amounts[index]);
                _updatingGrid = false;
            }
        }

        private void DgvTenders_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Treat numeric keys as input keys so they go to the grid instead of the form
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                e.IsInputKey = true;
            }
        }

        private void DgvTenders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvTenders.IsCurrentCellInEditMode)
            {
                if (dgvTenders.EditingControl is TextBox textBox)
                {
                    if (textBox.SelectionLength > 0)
                    {
                        textBox.SelectedText = "";
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }

            // Route number keys and special keys (Enter, Tab, Backspace) to the numpad handler
            switch (e.KeyCode)
            {
                case Keys.D0: case Keys.NumPad0: HandleNumpad("0"); e.Handled = true; break;
                case Keys.D1: case Keys.NumPad1: HandleNumpad("1"); e.Handled = true; break;
                case Keys.D2: case Keys.NumPad2: HandleNumpad("2"); e.Handled = true; break;
                case Keys.D3: case Keys.NumPad3: HandleNumpad("3"); e.Handled = true; break;
                case Keys.D4: case Keys.NumPad4: HandleNumpad("4"); e.Handled = true; break;
                case Keys.D5: case Keys.NumPad5: HandleNumpad("5"); e.Handled = true; break;
                case Keys.D6: case Keys.NumPad6: HandleNumpad("6"); e.Handled = true; break;
                case Keys.D7: case Keys.NumPad7: HandleNumpad("7"); e.Handled = true; break;
                case Keys.D8: case Keys.NumPad8: HandleNumpad("8"); e.Handled = true; break;
                case Keys.D9: case Keys.NumPad9: HandleNumpad("9"); e.Handled = true; break;
                case Keys.Back: HandleNumpad("⌫"); e.Handled = true; break;
                case Keys.Delete:
                    // Only handle Delete as backspace if NOT in edit mode; let TextBox handle it in edit mode
                    if (!dgvTenders.IsCurrentCellInEditMode)
                    {
                        HandleNumpad("⌫");
                        e.Handled = true;
                    }
                    break;
                case Keys.Up: NavigateTender(-1); e.Handled = true; break;
                case Keys.Down: NavigateTender(1); e.Handled = true; break;
                //case Keys.Tab: FinalizeInputAndOpenDetailsIfNeeded(); e.Handled = true; break;
                //case Keys.Enter: FinalizeInputAndOpenDetailsIfNeeded(); if (FirstMissingDetailIndex() < 0) Confirm(); e.Handled = true; break;
                case Keys.Escape: btnCancel_Click(sender, e); e.Handled = true; break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete && dgvTenders.IsCurrentCellInEditMode)
            {
                if (dgvTenders.EditingControl is TextBox textBox)
                {
                    if (textBox.SelectionLength > 0)
                    {
                        // Delete selected text
                        textBox.SelectedText = "";
                        return true;
                    }
                    else if (textBox.SelectionStart < textBox.Text.Length)
                    {
                        // Delete character at cursor position (to the right)
                        textBox.Select(textBox.SelectionStart, 1);
                        textBox.SelectedText = "";
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ── Tender selection ─────────────────────────────────────────────────

        private void SelectTender(int index)
        {
            if (index == _selectedIndex) return;

            // Finalize input for the tender being left: auto-open details if needed.
            FinalizeInputAndOpenDetailsIfNeeded();

            _selectedIndex = index;
            _inputBuffer   = AmountToBuffer(_amounts[index]);

            // Select the row in the grid
            dgvTenders.ClearSelection();
            if (index >= 0 && index < dgvTenders.Rows.Count)
            {
                dgvTenders.Rows[index].Selected = true;
            }

            if (btnDetails != null)
                btnDetails.Visible = RequiresPopup(_tenders[index].TenderType);

            // Ensure the form has focus so keyboard input works
            this.Focus();

            UpdateSummary();
        }

        private void NavigateTender(int delta)
        {
            int next = Math.Max(0, Math.Min(_tenders.Count - 1, _selectedIndex + delta));
            SelectTender(next);
        }

        // ── Numpad input ─────────────────────────────────────────────────────

        private void HandleNumpad(string key)
        {
            switch (key)
            {
                case "⌫":
                    _inputBuffer = _inputBuffer.Length > 1
                        ? _inputBuffer.Substring(0, _inputBuffer.Length - 1)
                        : "0";
                    break;
                case ".":
                    break; // 2 decimal places implied — ignore explicit dot
                default:
                    _inputBuffer = _inputBuffer == "0" ? key : _inputBuffer + key;
                    break;
            }

            CommitBuffer();
        }

        private void CommitBuffer()
        {
            if (!long.TryParse(_inputBuffer, out long raw)) raw = 0;
            SetAmount(_selectedIndex, raw / 100m);
            UpdateSummary();
        }

        /// <summary>
        /// Called when the user finalizes their input (Tab or clicks another field).
        /// If the current tender has an amount > 0, requires a popup, and has no details yet,
        /// automatically open the popup.
        /// </summary>
        private void FinalizeInputAndOpenDetailsIfNeeded()
        {
            if (_selectedIndex < 0 || _selectedIndex >= _tenders.Count) return;

            var tender = _tenders[_selectedIndex];
            if (_amounts[_selectedIndex] > 0 && RequiresPopup(tender.TenderType) && _details[_selectedIndex] == null)
                OpenDetailsForSelected();
        }

        /// <summary>
        /// Applies <paramref name="value"/> to a tender, enforcing the MaxAmount cap and
        /// (for non-change tenders) the document-total cap, then refreshes the grid cell
        /// and the input buffer. Shared by numpad input and the detail popups.
        /// </summary>
        private void SetAmount(int index, decimal value)
        {
            var tender = _tenders[index];

            if (tender.MaxAmount.HasValue && value > tender.MaxAmount.Value)
                value = tender.MaxAmount.Value;

            if (!tender.AllowsChange && value > _documentTotal)
                value = _documentTotal;

            _amounts[index] = value;

            // Update the grid cell
            _updatingGrid = true;
            dgvTenders.Rows[index].Cells[1].Value = FormatCurrency(value);
            _updatingGrid = false;

            if (index == _selectedIndex)
                _inputBuffer = AmountToBuffer(value);
        }

        // ── Detail popups ─────────────────────────────────────────────────────

        /// <summary>Tender types that require an extra data popup before confirming.</summary>
        private static bool RequiresPopup(TenderTypeEnum? type)
            => type == TenderTypeEnum.tndBankWireTransfer
            || type == TenderTypeEnum.tndCheck
            || type == TenderTypeEnum.tndVoucher;

        private void OpenDetailsForSelected()
        {
            int i = _selectedIndex;
            var tender = _tenders[i];

            switch (tender.TenderType)
            {
                case TenderTypeEnum.tndBankWireTransfer:
                {
                    var d = BankTransferDialog.Show(this, tender, (BankTransferDetails)_details[i]);
                    if (d == null) ClearTender(i);
                    else _details[i] = d;
                    break;
                }
                case TenderTypeEnum.tndCheck:
                {
                    var d = CheckDialog.Show(this, tender, _amounts[i], (CheckDetails)_details[i]);
                    if (d == null) ClearTender(i);
                    else { _details[i] = d; SetAmount(i, d.CheckAmount); }
                    break;
                }
                case TenderTypeEnum.tndVoucher:
                {
                    var d = CreditNoteRefundDialog.Show(this, tender, _amounts[i], (CreditNoteRefundDetails)_details[i]);
                    if (d == null) ClearTender(i);
                    else { _details[i] = d; SetAmount(i, d.SpentValueAmount); }
                    break;
                }
            }

            UpdateSummary();
        }

        /// <summary>Cancelling a popup clears the line: amount reset to zero and details dropped.</summary>
        private void ClearTender(int index)
        {
            _details[index] = null;
            SetAmount(index, 0m);
        }

        /// <summary>
        /// Index of the first allocated tender that requires a popup but has no details yet,
        /// or -1 when all required details are present.
        /// </summary>
        private int FirstMissingDetailIndex()
        {
            for (int i = 0; i < _tenders.Count; i++)
                if (_amounts[i] > 0 && RequiresPopup(_tenders[i].TenderType) && _details[i] == null)
                    return i;
            return -1;
        }

        // ── Summary ──────────────────────────────────────────────────────────

        private void UpdateSummary()
        {
            decimal delivered = _amounts.Sum();
            decimal missing   = _documentTotal - delivered;
            bool    fulfilled = missing <= 0;

            lblDeliveredValue.Text = FormatCurrency(delivered);
            lblTotalValue.Text     = FormatCurrency(_documentTotal);

            if (fulfilled)
            {
                lblMissingCaption.Text      = "Troco:";
                lblMissingCaption.ForeColor = Color.ForestGreen;
                lblMissingValue.Text        = FormatCurrency(-missing);
                lblMissingValue.ForeColor   = Color.ForestGreen;
            }
            else
            {
                lblMissingCaption.Text      = "Em falta:";
                lblMissingCaption.ForeColor = ClrError;
                lblMissingValue.Text        = FormatCurrency(missing);
                lblMissingValue.ForeColor   = ClrError;
            }

            fulfilled = delivered == 0 || _documentTotal == delivered;

            btnConfirm.Enabled   = fulfilled;
            btnConfirm.BackColor = fulfilled ? ClrHeader : ClrDisabled;
        }

        // ── Confirm ──────────────────────────────────────────────────────────

        private void Confirm()
        {
            if (_amounts.Sum() < _documentTotal) return;

            // Block confirmation while a tender that requires extra data is missing it.
            int missing = FirstMissingDetailIndex();
            if (missing >= 0)
            {
                SelectTender(missing);
                MessageBox.Show(
                    "Preencha os detalhes do meio de pagamento selecionado antes de confirmar.",
                    "Detalhes em falta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total  = _amounts.Sum();
            decimal change = total > _documentTotal ? total - _documentTotal : 0m;

            Result = new TenderSplitResult
            {
                Allocations = _tenders
                    .Select((t, i) => new TenderAllocation
                    {
                        Tender           = t,
                        Amount           = _amounts[i],
                        BankTransfer     = _details[i] as BankTransferDetails,
                        Check            = _details[i] as CheckDetails,
                        CreditNoteRefund = _details[i] as CreditNoteRefundDetails
                    })
                    .Where(a => a.Amount > 0)
                    .ToList(),
                TotalAllocated = total,
                ChangeDue      = change
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Event handlers ────────────────────────────────────────────────────

        private void btnNavUp_Click(object sender, EventArgs e)   => NavigateTender(-1);
        private void btnNavDown_Click(object sender, EventArgs e)  => NavigateTender(1);
        private void btnNumOk_Click(object sender, EventArgs e)
        {
            // End grid editing to commit any pending cell edits before finalizing
            dgvTenders.EndEdit();

            FinalizeInputAndOpenDetailsIfNeeded();
            if (FirstMissingDetailIndex() < 0) Confirm();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // End grid editing to commit any pending cell edits before finalizing
            dgvTenders.EndEdit();

            // If nothing delivered yet and a tender is selected, fill it with the full document total
            decimal totalDelivered = _amounts.Sum();
            if (totalDelivered == 0 && _selectedIndex >= 0)
            {
                SetAmount(_selectedIndex, _documentTotal);
                UpdateSummary();
            }

            FinalizeInputAndOpenDetailsIfNeeded();
            if (FirstMissingDetailIndex() < 0) Confirm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnNumpadClick(object sender, EventArgs e)
            => HandleNumpad(((Button)sender).Text);

        private void btnBackspace_Click(object sender, EventArgs e)
            => HandleNumpad("⌫");

        /// <summary>
        /// Suppress Enter and Tab on numpad buttons so they're handled by the form's KeyDown,
        /// not by the button click handlers.
        /// </summary>
        private void NumpadButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TenderSplitDialog_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0: case Keys.NumPad0: HandleNumpad("0"); break;
                case Keys.D1: case Keys.NumPad1: HandleNumpad("1"); break;
                case Keys.D2: case Keys.NumPad2: HandleNumpad("2"); break;
                case Keys.D3: case Keys.NumPad3: HandleNumpad("3"); break;
                case Keys.D4: case Keys.NumPad4: HandleNumpad("4"); break;
                case Keys.D5: case Keys.NumPad5: HandleNumpad("5"); break;
                case Keys.D6: case Keys.NumPad6: HandleNumpad("6"); break;
                case Keys.D7: case Keys.NumPad7: HandleNumpad("7"); break;
                case Keys.D8: case Keys.NumPad8: HandleNumpad("8"); break;
                case Keys.D9: case Keys.NumPad9: HandleNumpad("9"); break;
                case Keys.Back:
                case Keys.Delete:  HandleNumpad("⌫");   break;
                case Keys.Up:      NavigateTender(-1);  break;
                case Keys.Down:    NavigateTender(1);   break;
                case Keys.Tab:     FinalizeInputAndOpenDetailsIfNeeded(); break;
                case Keys.Enter:
                    FinalizeInputAndOpenDetailsIfNeeded();
                    if (FirstMissingDetailIndex() < 0) Confirm();
                    break;
                case Keys.Escape:  btnCancel_Click(sender, e); break;
                default: return;
            }
            e.Handled = true;
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static string FormatCurrency(decimal value) => value.ToString("N2") + " €";

        private static string AmountToBuffer(decimal value)
        {
            long raw = (long)(value * 100);
            return raw == 0 ? "0" : raw.ToString();
        }

        private void OpenDetailsForSelected(object sender, EventArgs e)
        {
            OpenDetailsForSelected();
        }
    }
}
