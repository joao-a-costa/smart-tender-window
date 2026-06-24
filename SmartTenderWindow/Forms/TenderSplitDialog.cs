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

        // ── Dynamic UI ───────────────────────────────────────────────────────
        private Panel[] _tenderRows;
        private Label[] _tenderAmountLabels;

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
            for (int i = 0; i < _tenders.Count; i++)
                _amounts[i] = _tenders[i].PreloadedAmount;

            InitializeComponent();

            lblTotalValue.Text = FormatCurrency(_documentTotal);
            lblMissingValue.Text = FormatCurrency(_documentTotal);

            BuildTenderRows();
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

        // ── Dynamic tender rows ──────────────────────────────────────────────

        private void BuildTenderRows()
        {
            _tenderRows         = new Panel[_tenders.Count];
            _tenderAmountLabels = new Label[_tenders.Count];

            panelList.SuspendLayout();

            for (int i = 0; i < _tenders.Count; i++)
            {
                int idx = i;

                var row = new Panel
                {
                    Location  = new Point(0, i * 38),
                    Size      = new Size(panelList.ClientSize.Width, 38),
                    BackColor = Color.White,
                    Cursor    = Cursors.Hand
                };

                var lblName = new Label
                {
                    Text      = _tenders[i].Name,
                    Location  = new Point(10, 0),
                    Size      = new Size(220, 38),
                    Font      = new Font("Segoe UI", 9.5f),
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent
                };

                var lblAmt = new Label
                {
                    Text      = FormatCurrency(_amounts[i]),
                    Location  = new Point(240, 0),
                    Size      = new Size(row.Width - 250, 38),
                    Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleRight,
                    BackColor = Color.Transparent,
                    Anchor    = AnchorStyles.Top | AnchorStyles.Right
                };

                row.Paint += (s, e) =>
                    e.Graphics.DrawLine(Pens.LightGray, 0, row.Height - 1, row.Width, row.Height - 1);

                row.Click     += (s, e) => SelectTender(idx);
                lblName.Click += (s, e) => SelectTender(idx);
                lblAmt.Click  += (s, e) => SelectTender(idx);

                row.Controls.Add(lblName);
                row.Controls.Add(lblAmt);

                _tenderRows[i]         = row;
                _tenderAmountLabels[i] = lblAmt;
                panelList.Controls.Add(row);
            }

            panelList.AutoScrollMinSize = new Size(0, _tenders.Count * 38);
            panelList.ClientSizeChanged += (s, e) =>
            {
                foreach (var row in _tenderRows)
                    row.Width = panelList.ClientSize.Width;
            };
            panelList.ResumeLayout();
        }

        // ── Tender selection ─────────────────────────────────────────────────

        private void SelectTender(int index)
        {
            if (index == _selectedIndex) return;
            _selectedIndex = index;
            _inputBuffer   = AmountToBuffer(_amounts[index]);

            for (int i = 0; i < _tenderRows.Length; i++)
            {
                bool sel = (i == index);
                Color bg = sel ? ClrSelected : Color.White;
                _tenderRows[i].BackColor = bg;
                foreach (Control c in _tenderRows[i].Controls)
                    c.BackColor = bg;
            }

            // Scroll selected row into view
            int rowY   = index * 38;
            int scrollY = -panelList.AutoScrollPosition.Y;
            if (rowY < scrollY)
                panelList.AutoScrollPosition = new Point(0, rowY);
            else if (rowY + 38 > scrollY + panelList.ClientSize.Height)
                panelList.AutoScrollPosition = new Point(0, rowY + 38 - panelList.ClientSize.Height);

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
            decimal value = raw / 100m;

            var tender = _tenders[_selectedIndex];

            if (tender.MaxAmount.HasValue && value > tender.MaxAmount.Value)
            {
                value = tender.MaxAmount.Value;
                _inputBuffer = AmountToBuffer(value);
            }

            if (!tender.AllowsChange && value > _documentTotal)
            {
                value = _documentTotal;
                _inputBuffer = AmountToBuffer(value);
            }

            _amounts[_selectedIndex] = value;
            _tenderAmountLabels[_selectedIndex].Text = FormatCurrency(value);
            UpdateSummary();
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

            btnConfirm.Enabled   = fulfilled;
            btnConfirm.BackColor = fulfilled ? ClrHeader : ClrDisabled;
        }

        // ── Confirm ──────────────────────────────────────────────────────────

        private void Confirm()
        {
            if (_amounts.Sum() < _documentTotal) return;

            decimal total  = _amounts.Sum();
            decimal change = total > _documentTotal ? total - _documentTotal : 0m;

            Result = new TenderSplitResult
            {
                Allocations = _tenders
                    .Select((t, i) => new TenderAllocation { Tender = t, Amount = _amounts[i] })
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
        private void btnNumOk_Click(object sender, EventArgs e)    => Confirm();
        private void btnConfirm_Click(object sender, EventArgs e)  => Confirm();

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
                case Keys.Enter:   Confirm();            break;
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
    }
}
