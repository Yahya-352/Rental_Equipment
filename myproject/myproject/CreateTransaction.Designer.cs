namespace myproject
{
    partial class CreateTransaction
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Submit_btn = new Button();
            categoryComboBox = new ComboBox();
            conditionComboBox = new ComboBox();
            availabilityComboBox = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            textBox8 = new TextBox();
            label4 = new Label();
            fee_textbox = new TextBox();
            label3 = new Label();
            description_textbox = new TextBox();
            label2 = new Label();
            name_textbox = new TextBox();
            label1 = new Label();
            label8 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // Submit_btn
            // 
            Submit_btn.BackColor = Color.FromArgb(19, 15, 64);
            Submit_btn.ForeColor = SystemColors.ControlLightLight;
            Submit_btn.Location = new Point(350, 275);
            Submit_btn.Name = "Submit_btn";
            Submit_btn.Size = new Size(94, 29);
            Submit_btn.TabIndex = 32;
            Submit_btn.Text = "Submit";
            Submit_btn.UseVisualStyleBackColor = false;
            // 
            // categoryComboBox
            // 
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(575, 148);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(151, 28);
            categoryComboBox.TabIndex = 31;
            // 
            // conditionComboBox
            // 
            conditionComboBox.FormattingEnabled = true;
            conditionComboBox.Location = new Point(575, 104);
            conditionComboBox.Name = "conditionComboBox";
            conditionComboBox.Size = new Size(151, 28);
            conditionComboBox.TabIndex = 30;
            // 
            // availabilityComboBox
            // 
            availabilityComboBox.FormattingEnabled = true;
            availabilityComboBox.Location = new Point(575, 49);
            availabilityComboBox.Name = "availabilityComboBox";
            availabilityComboBox.Size = new Size(151, 28);
            availabilityComboBox.TabIndex = 29;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(420, 156);
            label7.Name = "label7";
            label7.Size = new Size(81, 20);
            label7.TabIndex = 28;
            label7.Text = "Request ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(420, 107);
            label6.Name = "label6";
            label6.Size = new Size(81, 20);
            label6.TabIndex = 27;
            label6.Text = "Equipment";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(435, 52);
            label5.Name = "label5";
            label5.Size = new Size(109, 20);
            label5.TabIndex = 26;
            label5.Text = "Payment Status";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(214, 215);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(125, 27);
            textBox8.TabIndex = 25;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(74, 165);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 24;
            label4.Text = "Rental Period";
            // 
            // fee_textbox
            // 
            fee_textbox.Location = new Point(214, 162);
            fee_textbox.Name = "fee_textbox";
            fee_textbox.Size = new Size(125, 27);
            fee_textbox.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(93, 215);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 22;
            label3.Text = "Rental Fee";
            // 
            // description_textbox
            // 
            description_textbox.Location = new Point(214, 107);
            description_textbox.Name = "description_textbox";
            description_textbox.Size = new Size(125, 27);
            description_textbox.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 110);
            label2.Name = "label2";
            label2.Size = new Size(134, 20);
            label2.TabIndex = 20;
            label2.Text = "Rental Return Date";
            // 
            // name_textbox
            // 
            name_textbox.Location = new Point(214, 46);
            name_textbox.Name = "name_textbox";
            name_textbox.Size = new Size(125, 27);
            name_textbox.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 49);
            label1.Name = "label1";
            label1.Size = new Size(122, 20);
            label1.TabIndex = 18;
            label1.Text = "Rental Start Date";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(440, 218);
            label8.Name = "label8";
            label8.Size = new Size(61, 20);
            label8.TabIndex = 33;
            label8.Text = "Deposit";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(575, 215);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 34;
            // 
            // CreateTransaction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox1);
            Controls.Add(label8);
            Controls.Add(Submit_btn);
            Controls.Add(categoryComboBox);
            Controls.Add(conditionComboBox);
            Controls.Add(availabilityComboBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox8);
            Controls.Add(label4);
            Controls.Add(fee_textbox);
            Controls.Add(label3);
            Controls.Add(description_textbox);
            Controls.Add(label2);
            Controls.Add(name_textbox);
            Controls.Add(label1);
            Name = "CreateTransaction";
            Size = new Size(856, 509);
            Load += CreateTransaction_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Submit_btn;
        private ComboBox categoryComboBox;
        private ComboBox conditionComboBox;
        private ComboBox availabilityComboBox;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox textBox8;
        private Label label4;
        private TextBox fee_textbox;
        private Label label3;
        private TextBox description_textbox;
        private Label label2;
        private TextBox name_textbox;
        private Label label1;
        private Label label8;
        private TextBox textBox1;
    }
}
