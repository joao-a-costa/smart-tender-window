using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartTenderWindow.Windows
{
    public partial class MainForm : Form
    {
        private static readonly string[] TenderNames =
        {
            "Numerário", "Cartão de Crédito", "Cartão de Débito", "Cheque",
            "Transferência", "Vale", "MB Way", "PayPal", "Multibanco", "Ticket"
        };

        private static readonly Random Rng = new Random();

        public MainForm()
        {
            InitializeComponent();
            nudTenderCount.Value = Rng.Next(1, 11);
            nudDocumentTotal.Value = Math.Round((decimal)(Rng.NextDouble() * 499.99 + 0.01), 2);
        }

        private void btnOpenTender_Click(object sender, EventArgs e)
        {
            int count = (int)nudTenderCount.Value;
            decimal total = nudDocumentTotal.Value;

            var pool = new List<string>(TenderNames);
            var tenders = new List<TenderItem>();

            for (int i = 0; i < count; i++)
            {
                string name;
                if (pool.Count > 0)
                {
                    int idx = Rng.Next(pool.Count);
                    name = pool[idx];
                    pool.RemoveAt(idx);
                }
                else
                {
                    name = $"Tender {i + 1}";
                }

                tenders.Add(new TenderItem($"T{i + 1}", name)
                {
                    AllowsChange = (i == 0)
                });
            }

            TenderSplitResult result = TenderSplitDialog.Show(this, tenders, total, "Pagamento");

            if (result != null)
            {
                var lines = new System.Text.StringBuilder();
                foreach (var alloc in result.Allocations)
                    lines.AppendLine($"{alloc.Tender.Name}: {alloc.Amount:N2} €");
                lines.AppendLine($"Troco: {result.ChangeDue:N2} €");
                MessageBox.Show(lines.ToString(), "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
