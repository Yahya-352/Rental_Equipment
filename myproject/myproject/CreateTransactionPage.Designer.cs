namespace myproject
{
    partial class CreateTransactionPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Deposit = new TextBox();
            label8 = new Label();
            Submit_btn = new Button();
            PaymentComboBox = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            txtTotalCost = new TextBox();
            label4 = new Label();
            RentalPeriodTextBox = new TextBox();
            label3 = new Label();
            txtReturnDate = new TextBox();
            label2 = new Label();
            txtStartDate = new TextBox();
            label1 = new Label();
            monthCalendar1 = new MonthCalendar();
            EquipmentTextBox = new TextBox();
            RequestTextBox = new TextBox();
            SuspendLayout();
            // 
            // Deposit
            // 
            Deposit.Location = new Point(571, 197);
            Deposit.Name = "Deposit";
            Deposit.Size = new Size(125, 27);
            Deposit.TabIndex = 51;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(436, 200);
            label8.Name = "label8";
            label8.Size = new Size(61, 20);
            label8.TabIndex = 50;
            label8.Text = "Deposit";
            // 
            // Submit_btn
            // 
            Submit_btn.BackColor = Color.FromArgb(19, 15, 64);
            Submit_btn.ForeColor = SystemColors.ControlLightLight;
            Submit_btn.Location = new Point(346, 257);
            Submit_btn.Name = "Submit_btn";
            Submit_btn.Size = new Size(94, 29);
            Submit_btn.TabIndex = 49;
            Submit_btn.Text = "Submit";
            Submit_btn.UseVisualStyleBackColor = false;
            Submit_btn.Click += Submit_btn_Click;
            // 
            // PaymentComboBox
            // 
            PaymentComboBox.FormattingEnabled = true;
            PaymentComboBox.Location = new Point(571, 31);
            PaymentComboBox.Name = "PaymentComboBox";
            PaymentComboBox.Size = new Size(151, 28);
            PaymentComboBox.TabIndex = 46;
            PaymentComboBox.SelectedIndexChanged += PaymentComboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(416, 138);
            label7.Name = "label7";
            label7.Size = new Size(81, 20);
            label7.TabIndex = 45;
            label7.Text = "Request ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(416, 89);
            label6.Name = "label6";
            label6.Size = new Size(81, 20);
            label6.TabIndex = 44;
            label6.Text = "Equipment";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(431, 34);
            label5.Name = "label5";
            label5.Size = new Size(109, 20);
            label5.TabIndex = 43;
            label5.Text = "Payment Status";
            // 
            // txtTotalCost
            // 
            txtTotalCost.Location = new Point(210, 197);
            txtTotalCost.Name = "txtTotalCost";
            txtTotalCost.ReadOnly = true;
            txtTotalCost.Size = new Size(125, 27);
            txtTotalCost.TabIndex = 42;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(70, 147);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 41;
            label4.Text = "Rental Period";
            // 
            // RentalPeriodTextBox
            // 
            RentalPeriodTextBox.Location = new Point(210, 144);
            RentalPeriodTextBox.Name = "RentalPeriodTextBox";
            RentalPeriodTextBox.ReadOnly = true;
            RentalPeriodTextBox.Size = new Size(125, 27);
            RentalPeriodTextBox.TabIndex = 40;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(89, 197);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 39;
            label3.Text = "Rental Fee";
            // 
            // txtReturnDate
            // 
            txtReturnDate.Location = new Point(210, 89);
            txtReturnDate.Name = "txtReturnDate";
            txtReturnDate.Size = new Size(125, 27);
            txtReturnDate.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 92);
            label2.Name = "label2";
            label2.Size = new Size(134, 20);
            label2.TabIndex = 37;
            label2.Text = "Rental Return Date";
            // 
            // txtStartDate
            // 
            txtStartDate.Location = new Point(210, 28);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.Size = new Size(125, 27);
            txtStartDate.TabIndex = 36;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 31);
            label1.Name = "label1";
            label1.Size = new Size(122, 20);
            label1.TabIndex = 35;
            label1.Text = "Rental Start Date";
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(263, 294);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 52;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // EquipmentTextBox
            // 
            EquipmentTextBox.Location = new Point(575, 90);
            EquipmentTextBox.Name = "EquipmentTextBox";
            EquipmentTextBox.ReadOnly = true;
            EquipmentTextBox.Size = new Size(125, 27);
            EquipmentTextBox.TabIndex = 53;
            // 
            // RequestTextBox
            // 
            RequestTextBox.Location = new Point(575, 140);
            RequestTextBox.Name = "RequestTextBox";
            RequestTextBox.ReadOnly = true;
            RequestTextBox.Size = new Size(125, 27);
            RequestTextBox.TabIndex = 54;
            // 
            // CreateTransactionPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 519);
            Controls.Add(RequestTextBox);
            Controls.Add(EquipmentTextBox);
            Controls.Add(monthCalendar1);
            Controls.Add(Deposit);
            Controls.Add(label8);
            Controls.Add(Submit_btn);
            Controls.Add(PaymentComboBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtTotalCost);
            Controls.Add(label4);
            Controls.Add(RentalPeriodTextBox);
            Controls.Add(label3);
            Controls.Add(txtReturnDate);
            Controls.Add(label2);
            Controls.Add(txtStartDate);
            Controls.Add(label1);
            Name = "CreateTransactionPage";
            Text = "CreateTransactionPage";
            Load += CreateTransactionPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Deposit;
        private Label label8;
        private Button Submit_btn;
        private ComboBox PaymentComboBox;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox txtTotalCost;
        private Label label4;
        private TextBox RentalPeriodTextBox;
        private Label label3;
        private TextBox txtReturnDate;
        private Label label2;
        private TextBox txtStartDate;
        private Label label1;
        private MonthCalendar monthCalendar1;
        private TextBox EquipmentTextBox;
        private TextBox RequestTextBox;
    }
}