using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SmartTenderWindow.Windows
{
    public partial class MainForm : Form
    {
        private static readonly (TenderTypeEnum Type, string Name)[] TenderPool =
        {
            (TenderTypeEnum.tndCash, "Numerário"),
            (TenderTypeEnum.tndCreditDebitCard, "Cartão"),
            (TenderTypeEnum.tndCheck, "Cheque"),
            (TenderTypeEnum.tndBankWireTransfer, "Transferência"),
            (TenderTypeEnum.tndVoucher, "Nota de Crédito-Reembolso"),
            (TenderTypeEnum.tndMBWay, "MB Way"),
            (TenderTypeEnum.tndRefMB, "Multibanco"),
            (TenderTypeEnum.tndCoupon, "Cupão"),
            (TenderTypeEnum.tndCustomerAccount, "Conta Corrente"),
            (TenderTypeEnum.tndBankReceipt, "Recibo Bancário")
        };

        private static readonly Random Rng = new Random();

        public MainForm()
        {
            InitializeComponent();
            nudTenderCount.Value = Rng.Next(10, 99);
            nudDocumentTotal.Value = Math.Round((decimal)(Rng.NextDouble() * 499.99 + 0.01), 2);
        }

        private void btnOpenTender_Click(object sender, EventArgs e)
        {
            int count = (int)nudTenderCount.Value;
            decimal total = nudDocumentTotal.Value;

            var pool = new List<(TenderTypeEnum Type, string Name)>(TenderPool);
            var tenders = new List<TenderItem>();

            for (int i = 0; i < count; i++)
            {
                TenderTypeEnum type;
                string name;
                if (pool.Count > 0)
                {
                    int idx = Rng.Next(pool.Count);
                    (type, name) = pool[idx];
                    pool.RemoveAt(idx);
                }
                else
                {
                    type = TenderTypeEnum.tndCash;
                    name = $"Tender {i + 1}";
                }

                tenders.Add(new TenderItem(type, name)
                {
                    AllowsChange = (i == 0),
                    BeneficiaryAccounts = SampleOptions("Banco Comercial Português", "Caixa Geral de Depósitos"),
                    PartyAccounts = SampleOptions("Conta Cliente A", "Conta Cliente B"),
                    Banks = SampleOptions("Millennium BCP", "CGD", "Novo Banco"),
                    Series = SampleOptions("1", "2")
                });
            }

            TenderSplitResult result = TenderSplitDialog.Show(this, tenders, total, "Pagamento");

            if (result != null)
            {
                var summary = new System.Text.StringBuilder();
                summary.AppendLine("═══════════════════════════════════════════════════");
                summary.AppendLine("TENDER SPLIT SUMMARY");
                summary.AppendLine("═══════════════════════════════════════════════════");
                summary.AppendLine();
                summary.AppendLine($"Document Total:        €{total:N2}");
                summary.AppendLine($"Total Allocated:       €{result.TotalAllocated:N2}");
                summary.AppendLine($"Change Due:            €{result.ChangeDue:N2}");
                summary.AppendLine();
                summary.AppendLine("─────────────────────────────────────────────────");
                summary.AppendLine("ALLOCATIONS:");
                summary.AppendLine("─────────────────────────────────────────────────");

                foreach (var alloc in result.Allocations)
                {
                    summary.AppendLine($"  {alloc.Tender.Name,-30} €{alloc.Amount:N2}");

                    if (alloc.BankTransfer != null)
                        summary.AppendLine($"    └─ Bank Transfer: {alloc.BankTransfer.ContractReferenceNumber}");
                    else if (alloc.Check != null)
                        summary.AppendLine($"    └─ Check #: {alloc.Check.CheckSequenceNumber}");
                    else if (alloc.CreditNoteRefund != null)
                        summary.AppendLine($"    └─ Credit Note: {alloc.CreditNoteRefund.TransDocument} #{alloc.CreditNoteRefund.TransDocNumber}");
                }

                summary.AppendLine();
                summary.AppendLine("═══════════════════════════════════════════════════");
                summary.AppendLine("JSON DATA:");
                summary.AppendLine("═══════════════════════════════════════════════════");
                summary.AppendLine();

                string json = JsonConvert.SerializeObject(result, Formatting.Indented);
                summary.Append(json);

                txtJsonResult.Text = summary.ToString();
            }
            else
            {
                txtJsonResult.Text = "";
            }
        }

        private static List<TenderOption> SampleOptions(params string[] labels)
        {
            var list = new List<TenderOption>();
            for (int i = 0; i < labels.Length; i++)
                list.Add(new TenderOption((i + 1).ToString(), labels[i]));
            return list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new Form1())
            {
                form.ShowDialog(this);
            }
        }
    }
}
