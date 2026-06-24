namespace SmartTenderWindowTenderSplit.Forms
{
    partial class TenderSplitDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ───────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;

        // ── Left panel ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Button btnNavUp;
        private System.Windows.Forms.Button btnNavDown;
        private System.Windows.Forms.Label lblDeliveredCaption;
        private System.Windows.Forms.Label lblDeliveredValue;
        private System.Windows.Forms.Label lblMissingCaption;
        private System.Windows.Forms.Label lblMissingValue;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;

        // ── Numpad panel ──────────────────────────────────────────────────────
        private System.Windows.Forms.Panel panelNumpad;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnBackspace;
        private System.Windows.Forms.Button btnNumOk;

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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            this.btnNavUp = new System.Windows.Forms.Button();
            this.btnNavDown = new System.Windows.Forms.Button();
            this.lblDeliveredCaption = new System.Windows.Forms.Label();
            this.lblDeliveredValue = new System.Windows.Forms.Label();
            this.lblMissingCaption = new System.Windows.Forms.Label();
            this.lblMissingValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panelNumpad = new System.Windows.Forms.Panel();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btnBackspace = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btnNumOk = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.panelLeftBottom = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelNumpad.SuspendLayout();
            this.panelLeftBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(820, 52);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(820, 52);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Indique o método de pagamento:";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.panelLeftBottom);
            this.panelLeft.Controls.Add(this.lblCategory);
            this.panelLeft.Controls.Add(this.panelList);
            this.panelLeft.Controls.Add(this.btnNavUp);
            this.panelLeft.Controls.Add(this.btnNavDown);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 52);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(538, 549);
            this.panelLeft.TabIndex = 2;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.Gray;
            this.lblCategory.Location = new System.Drawing.Point(10, 10);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(164, 19);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "MEIOS DE PAGAMENTO";
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.AutoScroll = true;
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Location = new System.Drawing.Point(10, 34);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(516, 266);
            this.panelList.TabIndex = 1;
            // 
            // btnNavUp
            // 
            this.btnNavUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNavUp.FlatAppearance.BorderSize = 0;
            this.btnNavUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUp.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNavUp.ForeColor = System.Drawing.Color.White;
            this.btnNavUp.Location = new System.Drawing.Point(10, 306);
            this.btnNavUp.Name = "btnNavUp";
            this.btnNavUp.Size = new System.Drawing.Size(90, 36);
            this.btnNavUp.TabIndex = 2;
            this.btnNavUp.Text = "▲";
            this.btnNavUp.UseVisualStyleBackColor = false;
            this.btnNavUp.Click += new System.EventHandler(this.btnNavUp_Click);
            // 
            // btnNavDown
            // 
            this.btnNavDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNavDown.FlatAppearance.BorderSize = 0;
            this.btnNavDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDown.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNavDown.ForeColor = System.Drawing.Color.White;
            this.btnNavDown.Location = new System.Drawing.Point(106, 306);
            this.btnNavDown.Name = "btnNavDown";
            this.btnNavDown.Size = new System.Drawing.Size(90, 36);
            this.btnNavDown.TabIndex = 3;
            this.btnNavDown.Text = "▼";
            this.btnNavDown.UseVisualStyleBackColor = false;
            this.btnNavDown.Click += new System.EventHandler(this.btnNavDown_Click);
            // 
            // lblDeliveredCaption
            // 
            this.lblDeliveredCaption.AutoSize = true;
            this.lblDeliveredCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDeliveredCaption.Location = new System.Drawing.Point(12, 29);
            this.lblDeliveredCaption.Name = "lblDeliveredCaption";
            this.lblDeliveredCaption.Size = new System.Drawing.Size(115, 21);
            this.lblDeliveredCaption.TabIndex = 4;
            this.lblDeliveredCaption.Text = "Valor entregue:";
            // 
            // lblDeliveredValue
            // 
            this.lblDeliveredValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeliveredValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDeliveredValue.Location = new System.Drawing.Point(133, 29);
            this.lblDeliveredValue.Name = "lblDeliveredValue";
            this.lblDeliveredValue.Size = new System.Drawing.Size(393, 20);
            this.lblDeliveredValue.TabIndex = 5;
            this.lblDeliveredValue.Text = "0,00 €";
            this.lblDeliveredValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMissingCaption
            // 
            this.lblMissingCaption.AutoSize = true;
            this.lblMissingCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMissingCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblMissingCaption.Location = new System.Drawing.Point(12, 58);
            this.lblMissingCaption.Name = "lblMissingCaption";
            this.lblMissingCaption.Size = new System.Drawing.Size(77, 21);
            this.lblMissingCaption.TabIndex = 6;
            this.lblMissingCaption.Text = "Em falta:";
            // 
            // lblMissingValue
            // 
            this.lblMissingValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMissingValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMissingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblMissingValue.Location = new System.Drawing.Point(137, 59);
            this.lblMissingValue.Name = "lblMissingValue";
            this.lblMissingValue.Size = new System.Drawing.Size(389, 20);
            this.lblMissingValue.TabIndex = 7;
            this.lblMissingValue.Text = "0,00 €";
            this.lblMissingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(10, 89);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(52, 21);
            this.lblTotalCaption.TabIndex = 8;
            this.lblTotalCaption.Text = "Total:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(137, 89);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(389, 20);
            this.lblTotalValue.TabIndex = 9;
            this.lblTotalValue.Text = "0,00 €";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancel.Location = new System.Drawing.Point(14, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 62);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnConfirm.Enabled = false;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(386, 126);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(140, 62);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panelNumpad
            // 
            this.panelNumpad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelNumpad.Controls.Add(this.btn1);
            this.panelNumpad.Controls.Add(this.btn2);
            this.panelNumpad.Controls.Add(this.btn3);
            this.panelNumpad.Controls.Add(this.btnBackspace);
            this.panelNumpad.Controls.Add(this.btn4);
            this.panelNumpad.Controls.Add(this.btn5);
            this.panelNumpad.Controls.Add(this.btn6);
            this.panelNumpad.Controls.Add(this.btnNumOk);
            this.panelNumpad.Controls.Add(this.btn7);
            this.panelNumpad.Controls.Add(this.btn8);
            this.panelNumpad.Controls.Add(this.btn9);
            this.panelNumpad.Controls.Add(this.btn0);
            this.panelNumpad.Controls.Add(this.btnDot);
            this.panelNumpad.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelNumpad.Location = new System.Drawing.Point(538, 52);
            this.panelNumpad.Name = "panelNumpad";
            this.panelNumpad.Size = new System.Drawing.Size(282, 549);
            this.panelNumpad.TabIndex = 1;
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.White;
            this.btn1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn1.ForeColor = System.Drawing.Color.Black;
            this.btn1.Location = new System.Drawing.Point(15, 252);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(79, 103);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.White;
            this.btn2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn2.ForeColor = System.Drawing.Color.Black;
            this.btn2.Location = new System.Drawing.Point(102, 252);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(79, 103);
            this.btn2.TabIndex = 1;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.Color.White;
            this.btn3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn3.ForeColor = System.Drawing.Color.Black;
            this.btn3.Location = new System.Drawing.Point(189, 252);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(79, 103);
            this.btn3.TabIndex = 2;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btnBackspace
            // 
            this.btnBackspace.BackColor = System.Drawing.Color.White;
            this.btnBackspace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnBackspace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackspace.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnBackspace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnBackspace.Location = new System.Drawing.Point(17, 365);
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.Size = new System.Drawing.Size(79, 103);
            this.btnBackspace.TabIndex = 3;
            this.btnBackspace.Text = "⌫";
            this.btnBackspace.UseVisualStyleBackColor = false;
            this.btnBackspace.Click += new System.EventHandler(this.btnBackspace_Click);
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.Color.White;
            this.btn4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn4.ForeColor = System.Drawing.Color.Black;
            this.btn4.Location = new System.Drawing.Point(15, 143);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(79, 103);
            this.btn4.TabIndex = 4;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn5
            // 
            this.btn5.BackColor = System.Drawing.Color.White;
            this.btn5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn5.ForeColor = System.Drawing.Color.Black;
            this.btn5.Location = new System.Drawing.Point(102, 143);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(79, 103);
            this.btn5.TabIndex = 5;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn6
            // 
            this.btn6.BackColor = System.Drawing.Color.White;
            this.btn6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn6.ForeColor = System.Drawing.Color.Black;
            this.btn6.Location = new System.Drawing.Point(189, 143);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(79, 103);
            this.btn6.TabIndex = 6;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btnNumOk
            // 
            this.btnNumOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnNumOk.FlatAppearance.BorderSize = 0;
            this.btnNumOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNumOk.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnNumOk.ForeColor = System.Drawing.Color.White;
            this.btnNumOk.Location = new System.Drawing.Point(17, 474);
            this.btnNumOk.Name = "btnNumOk";
            this.btnNumOk.Size = new System.Drawing.Size(251, 62);
            this.btnNumOk.TabIndex = 7;
            this.btnNumOk.Text = "OK";
            this.btnNumOk.UseVisualStyleBackColor = false;
            this.btnNumOk.Click += new System.EventHandler(this.btnNumOk_Click);
            // 
            // btn7
            // 
            this.btn7.BackColor = System.Drawing.Color.White;
            this.btn7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn7.ForeColor = System.Drawing.Color.Black;
            this.btn7.Location = new System.Drawing.Point(15, 34);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(79, 103);
            this.btn7.TabIndex = 8;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn8
            // 
            this.btn8.BackColor = System.Drawing.Color.White;
            this.btn8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn8.ForeColor = System.Drawing.Color.Black;
            this.btn8.Location = new System.Drawing.Point(102, 34);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(79, 103);
            this.btn8.TabIndex = 9;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn9
            // 
            this.btn9.BackColor = System.Drawing.Color.White;
            this.btn9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn9.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn9.ForeColor = System.Drawing.Color.Black;
            this.btn9.Location = new System.Drawing.Point(189, 34);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(79, 103);
            this.btn9.TabIndex = 10;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btn0
            // 
            this.btn0.BackColor = System.Drawing.Color.White;
            this.btn0.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn0.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btn0.ForeColor = System.Drawing.Color.Black;
            this.btn0.Location = new System.Drawing.Point(102, 365);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(79, 103);
            this.btn0.TabIndex = 11;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = false;
            this.btn0.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // btnDot
            // 
            this.btnDot.BackColor = System.Drawing.Color.White;
            this.btnDot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDot.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnDot.ForeColor = System.Drawing.Color.Black;
            this.btnDot.Location = new System.Drawing.Point(189, 365);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(79, 103);
            this.btnDot.TabIndex = 12;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = false;
            this.btnDot.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // panelLeftBottom
            // 
            this.panelLeftBottom.Controls.Add(this.lblDeliveredValue);
            this.panelLeftBottom.Controls.Add(this.lblTotalValue);
            this.panelLeftBottom.Controls.Add(this.lblTotalCaption);
            this.panelLeftBottom.Controls.Add(this.lblMissingValue);
            this.panelLeftBottom.Controls.Add(this.lblMissingCaption);
            this.panelLeftBottom.Controls.Add(this.btnConfirm);
            this.panelLeftBottom.Controls.Add(this.btnCancel);
            this.panelLeftBottom.Controls.Add(this.lblDeliveredCaption);
            this.panelLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLeftBottom.Location = new System.Drawing.Point(0, 348);
            this.panelLeftBottom.Name = "panelLeftBottom";
            this.panelLeftBottom.Size = new System.Drawing.Size(538, 201);
            this.panelLeftBottom.TabIndex = 12;
            // 
            // TenderSplitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 601);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelNumpad);
            this.Controls.Add(this.panelHeader);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "TenderSplitDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pagamento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenderSplitDialog_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelNumpad.ResumeLayout(false);
            this.panelLeftBottom.ResumeLayout(false);
            this.panelLeftBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeftBottom;
    }
}
