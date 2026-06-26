using System;
using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvTenders = new System.Windows.Forms.DataGridView();
            this.colTenderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLeftTopRight = new System.Windows.Forms.Panel();
            this.panelLeftTopLeft = new System.Windows.Forms.Panel();
            this.panelLeftTopTop = new System.Windows.Forms.Panel();
            this.panelLeftTopBottom = new System.Windows.Forms.Panel();
            this.btnNavDown = new System.Windows.Forms.Button();
            this.btnNavUp = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.panelLeftBottom = new System.Windows.Forms.Panel();
            this.lblDeliveredValue = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblMissingValue = new System.Windows.Forms.Label();
            this.lblMissingCaption = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDeliveredCaption = new System.Windows.Forms.Label();
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
            this.panelHeader.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenders)).BeginInit();
            this.panelLeftTopBottom.SuspendLayout();
            this.panelLeftBottom.SuspendLayout();
            this.panelNumpad.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(744, 52);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(744, 52);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Indique o método de pagamento:";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.dgvTenders);
            this.panelLeft.Controls.Add(this.panelLeftTopRight);
            this.panelLeft.Controls.Add(this.panelLeftTopLeft);
            this.panelLeft.Controls.Add(this.panelLeftTopTop);
            this.panelLeft.Controls.Add(this.panelLeftTopBottom);
            this.panelLeft.Controls.Add(this.panelLeftBottom);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 52);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(461, 547);
            this.panelLeft.TabIndex = 2;
            // 
            // dgvTenders
            // 
            this.dgvTenders.AllowUserToAddRows = false;
            this.dgvTenders.AllowUserToDeleteRows = false;
            this.dgvTenders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTenders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTenders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTenderName,
            this.colAmount});
            this.dgvTenders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTenders.Location = new System.Drawing.Point(17, 36);
            this.dgvTenders.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTenders.Name = "dgvTenders";
            this.dgvTenders.RowHeadersWidth = 51;
            this.dgvTenders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTenders.Size = new System.Drawing.Size(427, 253);
            this.dgvTenders.TabIndex = 14;
            this.dgvTenders.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTenders_RowEnter);
            // 
            // colTenderName
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTenderName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTenderName.HeaderText = "Modalidade";
            this.colTenderName.MinimumWidth = 6;
            this.colTenderName.Name = "colTenderName";
            this.colTenderName.ReadOnly = true;
            // 
            // colAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.colAmount.HeaderText = "Valor (€)";
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            // 
            // panelLeftTopRight
            // 
            this.panelLeftTopRight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeftTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLeftTopRight.Location = new System.Drawing.Point(444, 36);
            this.panelLeftTopRight.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftTopRight.Name = "panelLeftTopRight";
            this.panelLeftTopRight.Size = new System.Drawing.Size(17, 253);
            this.panelLeftTopRight.TabIndex = 18;
            // 
            // panelLeftTopLeft
            // 
            this.panelLeftTopLeft.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeftTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftTopLeft.Location = new System.Drawing.Point(0, 36);
            this.panelLeftTopLeft.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftTopLeft.Name = "panelLeftTopLeft";
            this.panelLeftTopLeft.Size = new System.Drawing.Size(17, 253);
            this.panelLeftTopLeft.TabIndex = 17;
            // 
            // panelLeftTopTop
            // 
            this.panelLeftTopTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeftTopTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftTopTop.Location = new System.Drawing.Point(0, 0);
            this.panelLeftTopTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftTopTop.Name = "panelLeftTopTop";
            this.panelLeftTopTop.Size = new System.Drawing.Size(461, 36);
            this.panelLeftTopTop.TabIndex = 16;
            // 
            // panelLeftTopBottom
            // 
            this.panelLeftTopBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeftTopBottom.Controls.Add(this.btnNavDown);
            this.panelLeftTopBottom.Controls.Add(this.btnNavUp);
            this.panelLeftTopBottom.Controls.Add(this.btnDetails);
            this.panelLeftTopBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLeftTopBottom.Location = new System.Drawing.Point(0, 289);
            this.panelLeftTopBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftTopBottom.Name = "panelLeftTopBottom";
            this.panelLeftTopBottom.Size = new System.Drawing.Size(461, 57);
            this.panelLeftTopBottom.TabIndex = 15;
            // 
            // btnNavDown
            // 
            this.btnNavDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNavDown.FlatAppearance.BorderSize = 0;
            this.btnNavDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDown.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNavDown.ForeColor = System.Drawing.Color.White;
            this.btnNavDown.Location = new System.Drawing.Point(112, 11);
            this.btnNavDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNavDown.Name = "btnNavDown";
            this.btnNavDown.Size = new System.Drawing.Size(91, 36);
            this.btnNavDown.TabIndex = 3;
            this.btnNavDown.Text = "▼";
            this.btnNavDown.UseVisualStyleBackColor = false;
            this.btnNavDown.Click += new System.EventHandler(this.btnNavDown_Click);
            // 
            // btnNavUp
            // 
            this.btnNavUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNavUp.FlatAppearance.BorderSize = 0;
            this.btnNavUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUp.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNavUp.ForeColor = System.Drawing.Color.White;
            this.btnNavUp.Location = new System.Drawing.Point(16, 11);
            this.btnNavUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNavUp.Name = "btnNavUp";
            this.btnNavUp.Size = new System.Drawing.Size(91, 36);
            this.btnNavUp.TabIndex = 2;
            this.btnNavUp.Text = "▲";
            this.btnNavUp.UseVisualStyleBackColor = false;
            this.btnNavUp.Click += new System.EventHandler(this.btnNavUp_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnDetails.FlatAppearance.BorderSize = 0;
            this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetails.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnDetails.ForeColor = System.Drawing.Color.White;
            this.btnDetails.Location = new System.Drawing.Point(317, 11);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(127, 36);
            this.btnDetails.TabIndex = 13;
            this.btnDetails.Text = "Detalhes...";
            this.btnDetails.UseVisualStyleBackColor = false;
            this.btnDetails.Click += new System.EventHandler(this.OpenDetailsForSelected);
            // 
            // panelLeftBottom
            // 
            this.panelLeftBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeftBottom.Controls.Add(this.lblDeliveredValue);
            this.panelLeftBottom.Controls.Add(this.lblTotalValue);
            this.panelLeftBottom.Controls.Add(this.lblTotalCaption);
            this.panelLeftBottom.Controls.Add(this.lblMissingValue);
            this.panelLeftBottom.Controls.Add(this.lblMissingCaption);
            this.panelLeftBottom.Controls.Add(this.btnConfirm);
            this.panelLeftBottom.Controls.Add(this.btnCancel);
            this.panelLeftBottom.Controls.Add(this.lblDeliveredCaption);
            this.panelLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLeftBottom.Location = new System.Drawing.Point(0, 346);
            this.panelLeftBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLeftBottom.Name = "panelLeftBottom";
            this.panelLeftBottom.Size = new System.Drawing.Size(461, 201);
            this.panelLeftBottom.TabIndex = 12;
            // 
            // lblDeliveredValue
            // 
            this.lblDeliveredValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeliveredValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDeliveredValue.Location = new System.Drawing.Point(133, 30);
            this.lblDeliveredValue.Name = "lblDeliveredValue";
            this.lblDeliveredValue.Size = new System.Drawing.Size(316, 20);
            this.lblDeliveredValue.TabIndex = 5;
            this.lblDeliveredValue.Text = "0,00 €";
            this.lblDeliveredValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(137, 89);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(312, 20);
            this.lblTotalValue.TabIndex = 9;
            this.lblTotalValue.Text = "0,00 €";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(11, 89);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(52, 21);
            this.lblTotalCaption.TabIndex = 8;
            this.lblTotalCaption.Text = "Total:";
            // 
            // lblMissingValue
            // 
            this.lblMissingValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMissingValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMissingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblMissingValue.Location = new System.Drawing.Point(137, 59);
            this.lblMissingValue.Name = "lblMissingValue";
            this.lblMissingValue.Size = new System.Drawing.Size(312, 20);
            this.lblMissingValue.TabIndex = 7;
            this.lblMissingValue.Text = "0,00 €";
            this.lblMissingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(309, 127);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(135, 62);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancel.Location = new System.Drawing.Point(17, 127);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 60);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDeliveredCaption
            // 
            this.lblDeliveredCaption.AutoSize = true;
            this.lblDeliveredCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDeliveredCaption.Location = new System.Drawing.Point(12, 30);
            this.lblDeliveredCaption.Name = "lblDeliveredCaption";
            this.lblDeliveredCaption.Size = new System.Drawing.Size(115, 21);
            this.lblDeliveredCaption.TabIndex = 4;
            this.lblDeliveredCaption.Text = "Valor entregue:";
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
            this.panelNumpad.Location = new System.Drawing.Point(461, 52);
            this.panelNumpad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelNumpad.Name = "panelNumpad";
            this.panelNumpad.Size = new System.Drawing.Size(283, 547);
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
            this.btn1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn2.Location = new System.Drawing.Point(101, 252);
            this.btn2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btnBackspace.Location = new System.Drawing.Point(17, 366);
            this.btnBackspace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn5.Location = new System.Drawing.Point(101, 143);
            this.btn5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btnNumOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn8.Location = new System.Drawing.Point(101, 34);
            this.btn8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn0.Location = new System.Drawing.Point(101, 366);
            this.btn0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btnDot.Location = new System.Drawing.Point(189, 366);
            this.btnDot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(79, 103);
            this.btnDot.TabIndex = 12;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = false;
            this.btnDot.Click += new System.EventHandler(this.OnNumpadClick);
            // 
            // TenderSplitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 599);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelNumpad);
            this.Controls.Add(this.panelHeader);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.Name = "TenderSplitDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pagamento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenderSplitDialog_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenders)).EndInit();
            this.panelLeftTopBottom.ResumeLayout(false);
            this.panelLeftBottom.ResumeLayout(false);
            this.panelLeftBottom.PerformLayout();
            this.panelNumpad.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeftBottom;
        private System.Windows.Forms.Button btnDetails;
        private DataGridView dgvTenders;
        private Panel panelLeftTopBottom;
        private DataGridViewTextBoxColumn colTenderName;
        private DataGridViewTextBoxColumn colAmount;
        private Panel panelLeftTopTop;
        private Panel panelLeftTopRight;
        private Panel panelLeftTopLeft;
    }
}
