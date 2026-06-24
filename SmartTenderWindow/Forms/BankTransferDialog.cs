using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartTenderWindowTenderSplit.Models;

namespace SmartTenderWindowTenderSplit.Forms
{
    /// <summary>
    /// "Transferência bancária" popup. Collects a <see cref="BankTransferDetails"/>.
    /// Drop-down options are taken from the supplied <see cref="TenderItem"/>.
    /// </summary>
    public partial class BankTransferDialog : Form
    {
        public BankTransferDetails Result { get; private set; }

        public BankTransferDialog()
        {
            InitializeComponent();
        }

        public BankTransferDialog(TenderItem tender, BankTransferDetails existing) : this()
        {
            if (tender == null) throw new ArgumentNullException(nameof(tender));

            PopulateCombo(cmbBeneficiary, tender.BeneficiaryAccounts, existing?.BankAccountId);
            PopulateCombo(cmbParty, tender.PartyAccounts, existing?.PartyBankAccountId);

            if (existing != null)
            {
                txtReference.Text = existing.ContractReferenceNumber;
                if (existing.AccountingDate != default(DateTime))
                    dtpDate.Value = existing.AccountingDate;
            }
        }

        public static BankTransferDetails Show(IWin32Window owner, TenderItem tender, BankTransferDetails existing)
        {
            using (var dlg = new BankTransferDialog(tender, existing))
                return dlg.ShowDialog(owner) == DialogResult.OK ? dlg.Result : null;
        }

        private void btnOk_Click(object sender, EventArgs e) => OnOk();

        private void OnOk()
        {
            Result = new BankTransferDetails
            {
                BankAccountId = (cmbBeneficiary.SelectedItem as TenderOption)?.Id,
                PartyBankAccountId = (cmbParty.SelectedItem as TenderOption)?.Id,
                ContractReferenceNumber = txtReference.Text,
                AccountingDate = dtpDate.Value.Date
            };
            DialogResult = DialogResult.OK;
            Close();
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
