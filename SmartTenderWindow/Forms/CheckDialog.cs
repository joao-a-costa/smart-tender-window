using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindowTenderSplit.Forms
{
    /// <summary>
    /// "Dados do Cheque" popup. Collects a <see cref="CheckDetails"/>.
    /// The check number is mandatory (highlighted), mirroring the reference layout.
    /// </summary>
    public partial class CheckDialog : Form
    {
        public CheckDetails Result { get; private set; }

        public CheckDialog()
        {
            InitializeComponent();
        }

        public CheckDialog(TenderItem tender, decimal amount, CheckDetails existing) : this()
        {
            if (tender == null) throw new ArgumentNullException(nameof(tender));

            PopulateCombo(cmbBank, tender.Banks, existing?.BankId);
            nudAmount.Value = ClampToRange(existing?.CheckAmount ?? amount, nudAmount);

            if (existing != null)
            {
                txtCheckNumber.Text = existing.CheckSequenceNumber;
                if (existing.CheckDeferredDate != default(DateTime))
                    dtpDate.Value = existing.CheckDeferredDate;
            }
        }

        public static CheckDetails Show(IWin32Window owner, TenderItem tender, decimal amount, CheckDetails existing)
        {
            using (var dlg = new CheckDialog(tender, amount, existing))
                return dlg.ShowDialog(owner) == DialogResult.OK ? dlg.Result : null;
        }

        private void btnOk_Click(object sender, EventArgs e) => OnOk();

        private void OnOk()
        {
            // Check number is mandatory.
            if (string.IsNullOrWhiteSpace(txtCheckNumber.Text))
            {
                txtCheckNumber.Focus();
                return;
            }

            Result = new CheckDetails
            {
                CheckSequenceNumber = txtCheckNumber.Text.Trim(),
                BankId = (cmbBank.SelectedItem as TenderOption)?.Id,
                CheckDeferredDate = dtpDate.Value.Date,
                CheckAmount = nudAmount.Value
            };
            DialogResult = DialogResult.OK;
            Close();
        }

        private static decimal ClampToRange(decimal value, NumericUpDown nud)
        {
            if (value < nud.Minimum) return nud.Minimum;
            if (value > nud.Maximum) return nud.Maximum;
            return value;
        }

        private static void PopulateCombo(ComboBox combo, List<TenderOption> options, string selectedId)
        {
            if (options == null) return;
            foreach (var o in options)
            {
                combo.Items.Add(o);
                if (selectedId != null && o.Id == selectedId)
                    combo.SelectedItem = o;
            }
            if (combo.SelectedIndex < 0 && combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }
    }
}
