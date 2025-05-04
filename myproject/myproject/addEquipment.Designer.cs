namespace myproject
{
    partial class addEquipment
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
            label1 = new Label();
            name_textbox = new TextBox();
            description_textbox = new TextBox();
            label2 = new Label();
            fee_textbox = new TextBox();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            availabilityComboBox = new ComboBox();
            conditionComboBox = new ComboBox();
            categoryComboBox = new ComboBox();
            Submit_btn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 32);
            label1.Name = "label1";
            label1.Size = new Size(122, 20);
            label1.TabIndex = 0;
            label1.Text = "Equipment name";
            // 
            // name_textbox
            // 
            name_textbox.Location = new Point(212, 29);
            name_textbox.Name = "name_textbox";
            name_textbox.Size = new Size(125, 27);
            name_textbox.TabIndex = 1;
            // 
            // description_textbox
            // 
            description_textbox.Location = new Point(212, 90);
            description_textbox.Name = "description_textbox";
            description_textbox.Size = new Size(125, 27);
            description_textbox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(57, 93);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 2;
            label2.Text = "Description";
            // 
            // fee_textbox
            // 
            fee_textbox.Location = new Point(212, 145);
            fee_textbox.Name = "fee_textbox";
            fee_textbox.Size = new Size(125, 27);
            fee_textbox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(57, 148);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 4;
            label3.Text = "Rental Fee";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(57, 202);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 8;
            label5.Text = "Availability";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(57, 252);
            label6.Name = "label6";
            label6.Size = new Size(74, 20);
            label6.TabIndex = 9;
            label6.Text = "Condition";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(57, 301);
            label7.Name = "label7";
            label7.Size = new Size(69, 20);
            label7.TabIndex = 10;
            label7.Text = "Category";
            // 
            // availabilityComboBox
            // 
            availabilityComboBox.FormattingEnabled = true;
            availabilityComboBox.Location = new Point(212, 194);
            availabilityComboBox.Name = "availabilityComboBox";
            availabilityComboBox.Size = new Size(151, 28);
            availabilityComboBox.TabIndex = 11;
            // 
            // conditionComboBox
            // 
            conditionComboBox.FormattingEnabled = true;
            conditionComboBox.Location = new Point(212, 249);
            conditionComboBox.Name = "conditionComboBox";
            conditionComboBox.Size = new Size(151, 28);
            conditionComboBox.TabIndex = 12;
            // 
            // categoryComboBox
            // 
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(212, 293);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(151, 28);
            categoryComboBox.TabIndex = 13;
            // 
            // Submit_btn
            // 
            Submit_btn.BackColor = Color.FromArgb(19, 15, 64);
            Submit_btn.ForeColor = SystemColors.ControlLightLight;
            Submit_btn.Location = new Point(145, 399);
            Submit_btn.Name = "Submit_btn";
            Submit_btn.Size = new Size(94, 29);
            Submit_btn.TabIndex = 17;
            Submit_btn.Text = "Submit";
            Submit_btn.UseVisualStyleBackColor = false;
            Submit_btn.Click += Submit_btn_Click;
            // 
            // addEquipment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 450);
            Controls.Add(Submit_btn);
            Controls.Add(categoryComboBox);
            Controls.Add(conditionComboBox);
            Controls.Add(availabilityComboBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(fee_textbox);
            Controls.Add(label3);
            Controls.Add(description_textbox);
            Controls.Add(label2);
            Controls.Add(name_textbox);
            Controls.Add(label1);
            Name = "addEquipment";
            Text = " E";
            Load += addEquipment_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox name_textbox;
        private TextBox description_textbox;
        private Label label2;
        private TextBox fee_textbox;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox availabilityComboBox;
        private ComboBox conditionComboBox;
        private ComboBox categoryComboBox;
        private Button Submit_btn;
    }
}