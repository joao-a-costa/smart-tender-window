namespace SmartTenderWindowTenderSplit.Forms
{
    partial class CreditNoteRefundDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.Label lblDocument;
        private System.Windows.Forms.TextBox txtDocument;
        private System.Windows.Forms.Label lblDocNumber;
        private System.Windows.Forms.NumericUpDown nudDocNumber;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.NumericUpDown nudAvailable;
        private System.Windows.Forms.Label lblSpent;
        private System.Windows.Forms.NumericUpDown nudSpent;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.lblDocument = new System.Windows.Forms.Label();
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.lblDocNumber = new System.Windows.Forms.Label();
            this.nudDocNumber = new System.Windows.Forms.NumericUpDown();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.nudAvailable = new System.Windows.Forms.NumericUpDown();
            this.lblSpent = new System.Windows.Forms.Label();
            this.nudSpent = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(440, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(440, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Dados do documento";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(20, 64);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(45, 21);
            this.lblSerie.TabIndex = 1;
            this.lblSerie.Text = "Série";
            // 
            // cmbSerie
            // 
            this.cmbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerie.Location = new System.Drawing.Point(160, 60);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(260, 29);
            this.cmbSerie.TabIndex = 2;
            // 
            // lblDocument
            // 
            this.lblDocument.AutoSize = true;
            this.lblDocument.Location = new System.Drawing.Point(20, 100);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(91, 21);
            this.lblDocument.TabIndex = 3;
            this.lblDocument.Text = "Documento";
            // 
            // txtDocument
            // 
            this.txtDocument.BackColor = System.Drawing.SystemColors.Control;
            this.txtDocument.Location = new System.Drawing.Point(160, 96);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.ReadOnly = true;
            this.txtDocument.Size = new System.Drawing.Size(260, 29);
            this.txtDocument.TabIndex = 4;
            this.txtDocument.TabStop = false;
            // 
            // lblDocNumber
            // 
            this.lblDocNumber.AutoSize = true;
            this.lblDocNumber.Location = new System.Drawing.Point(20, 136);
            this.lblDocNumber.Name = "lblDocNumber";
            this.lblDocNumber.Size = new System.Drawing.Size(117, 21);
            this.lblDocNumber.TabIndex = 5;
            this.lblDocNumber.Text = "N.º Documento";
            // 
            // nudDocNumber
            // 
            this.nudDocNumber.Location = new System.Drawing.Point(160, 132);
            this.nudDocNumber.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudDocNumber.Name = "nudDocNumber";
            this.nudDocNumber.Size = new System.Drawing.Size(260, 29);
            this.nudDocNumber.TabIndex = 6;
            this.nudDocNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAvailable
            // 
            this.lblAvailable.AutoSize = true;
            this.lblAvailable.Location = new System.Drawing.Point(20, 172);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(121, 21);
            this.lblAvailable.TabIndex = 7;
            this.lblAvailable.Text = "Valor disponível";
            // 
            // nudAvailable
            // 
            this.nudAvailable.DecimalPlaces = 2;
            this.nudAvailable.Location = new System.Drawing.Point(160, 168);
            this.nudAvailable.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudAvailable.Name = "nudAvailable";
            this.nudAvailable.Size = new System.Drawing.Size(260, 29);
            this.nudAvailable.TabIndex = 8;
            this.nudAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAvailable.ThousandsSeparator = true;
            // 
            // lblSpent
            // 
            this.lblSpent.AutoSize = true;
            this.lblSpent.Location = new System.Drawing.Point(20, 208);
            this.lblSpent.Name = "lblSpent";
            this.lblSpent.Size = new System.Drawing.Size(130, 21);
            this.lblSpent.TabIndex = 9;
            this.lblSpent.Text = "Valor a descontar";
            // 
            // nudSpent
            // 
            this.nudSpent.DecimalPlaces = 2;
            this.nudSpent.Location = new System.Drawing.Point(160, 204);
            this.nudSpent.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudSpent.Name = "nudSpent";
            this.nudSpent.Size = new System.Drawing.Size(260, 29);
            this.nudSpent.TabIndex = 10;
            this.nudSpent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSpent.ThousandsSeparator = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(320, 239);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 30);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(210, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CreditNoteRefundDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(440, 282);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.cmbSerie);
            this.Controls.Add(this.lblDocument);
            this.Controls.Add(this.txtDocument);
            this.Controls.Add(this.lblDocNumber);
            this.Controls.Add(this.nudDocNumber);
            this.Controls.Add(this.lblAvailable);
            this.Controls.Add(this.nudAvailable);
            this.Controls.Add(this.lblSpent);
            this.Controls.Add(this.nudSpent);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditNoteRefundDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDocNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
