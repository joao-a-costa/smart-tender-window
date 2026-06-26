namespace SmartTenderWindow.Windows
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTenderCount = new System.Windows.Forms.Label();
            this.nudTenderCount = new System.Windows.Forms.NumericUpDown();
            this.lblDocumentTotal = new System.Windows.Forms.Label();
            this.nudDocumentTotal = new System.Windows.Forms.NumericUpDown();
            this.btnOpenTender = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtJsonResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTenderCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenderCount
            // 
            this.lblTenderCount.AutoSize = true;
            this.lblTenderCount.Location = new System.Drawing.Point(24, 28);
            this.lblTenderCount.Name = "lblTenderCount";
            this.lblTenderCount.Size = new System.Drawing.Size(126, 16);
            this.lblTenderCount.TabIndex = 0;
            this.lblTenderCount.Text = "Number of Tenders:";
            // 
            // nudTenderCount
            // 
            this.nudTenderCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTenderCount.Location = new System.Drawing.Point(200, 26);
            this.nudTenderCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTenderCount.Name = "nudTenderCount";
            this.nudTenderCount.Size = new System.Drawing.Size(345, 22);
            this.nudTenderCount.TabIndex = 0;
            this.nudTenderCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblDocumentTotal
            // 
            this.lblDocumentTotal.AutoSize = true;
            this.lblDocumentTotal.Location = new System.Drawing.Point(24, 68);
            this.lblDocumentTotal.Name = "lblDocumentTotal";
            this.lblDocumentTotal.Size = new System.Drawing.Size(123, 16);
            this.lblDocumentTotal.TabIndex = 1;
            this.lblDocumentTotal.Text = "Document Total (€):";
            // 
            // nudDocumentTotal
            // 
            this.nudDocumentTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDocumentTotal.DecimalPlaces = 2;
            this.nudDocumentTotal.Location = new System.Drawing.Point(200, 66);
            this.nudDocumentTotal.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            131072});
            this.nudDocumentTotal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudDocumentTotal.Name = "nudDocumentTotal";
            this.nudDocumentTotal.Size = new System.Drawing.Size(345, 22);
            this.nudDocumentTotal.TabIndex = 1;
            this.nudDocumentTotal.Value = new decimal(new int[] {
            3750,
            0,
            0,
            131072});
            // 
            // btnOpenTender
            // 
            this.btnOpenTender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenTender.Location = new System.Drawing.Point(27, 110);
            this.btnOpenTender.Name = "btnOpenTender";
            this.btnOpenTender.Size = new System.Drawing.Size(518, 30);
            this.btnOpenTender.TabIndex = 2;
            this.btnOpenTender.Text = "Open Tender Window";
            this.btnOpenTender.UseVisualStyleBackColor = true;
            this.btnOpenTender.Click += new System.EventHandler(this.btnOpenTender_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(36, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(518, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Open Test Window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtJsonResult
            // 
            this.txtJsonResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJsonResult.Location = new System.Drawing.Point(27, 146);
            this.txtJsonResult.Multiline = true;
            this.txtJsonResult.Name = "txtJsonResult";
            this.txtJsonResult.ReadOnly = true;
            this.txtJsonResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtJsonResult.Size = new System.Drawing.Size(518, 323);
            this.txtJsonResult.TabIndex = 4;
            this.txtJsonResult.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 491);
            this.Controls.Add(this.txtJsonResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTenderCount);
            this.Controls.Add(this.nudTenderCount);
            this.Controls.Add(this.lblDocumentTotal);
            this.Controls.Add(this.nudDocumentTotal);
            this.Controls.Add(this.btnOpenTender);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tender Window Tester";
            ((System.ComponentModel.ISupportInitialize)(this.nudTenderCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTenderCount;
        private System.Windows.Forms.NumericUpDown nudTenderCount;
        private System.Windows.Forms.Label lblDocumentTotal;
        private System.Windows.Forms.NumericUpDown nudDocumentTotal;
        private System.Windows.Forms.Button btnOpenTender;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtJsonResult;
    }
}
