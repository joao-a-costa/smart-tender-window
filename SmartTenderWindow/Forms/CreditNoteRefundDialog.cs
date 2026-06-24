using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindowTenderSplit.Forms
{
    /// <summary>
    /// "Dados do documento" / credit-note refund popup ("Nota de Crédito-Reembolso").
    /// Collects a <see cref="CreditNoteRefundDetails"/>. Maps to the Sage 50c voucher.
    /// </summary>
    public partial class CreditNoteRefundDialog : Form
    {
        private const string DocumentName = "Nota de Crédito-Reembolso";

        public CreditNoteRefundDetails Result { get; private set; }

        public CreditNoteRefundDialog()
        {
            InitializeComponent();
            txtDocument.Text = DocumentName;
        }

        public CreditNoteRefundDialog(TenderItem tender, decimal amount, CreditNoteRefundDetails existing) : this()
        {
            if (tender == null) throw new ArgumentNullException(nameof(tender));

            PopulateCombo(cmbSerie, tender.Series, existing?.TransSerial);
            if (existing != null)
            {
                nudDocNumber.Value = ClampToRange(existing.TransDocNumber, nudDocNumber);
                nudAvailable.Value = ClampToRange(existing.TotalAmount, nudAvailable);
                nudSpent.Value = ClampToRange(existing.SpentValueAmount, nudSpent);
            }
            else
            {
                nudSpent.Value = ClampToRange(amount, nudSpent);
            }
        }

        public static CreditNoteRefundDetails Show(IWin32Window owner, TenderItem tender, decimal amount, CreditNoteRefundDetails existing)
        {
            using (var dlg = new CreditNoteRefundDialog(tender, amount, existing))
                return dlg.ShowDialog(owner) == DialogResult.OK ? dlg.Result : null;
        }

        private void btnOk_Click(object sender, EventArgs e) => OnOk();

        private void OnOk()
        {
            Result = new CreditNoteRefundDetails
            {
                TransSerial = (cmbSerie.SelectedItem as TenderOption)?.Id,
                TransDocument = DocumentName,
                TransDocNumber = (long)nudDocNumber.Value,
                TotalAmount = nudAvailable.Value,
                SpentValueAmount = nudSpent.Value
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
