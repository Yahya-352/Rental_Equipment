namespace myproject
{
    partial class Return_form
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
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            codition_cb = new ComboBox();
            recordbtn = new Button();
            num_daystxt = new TextBox();
            label1 = new Label();
            numlatedaystxt = new TextBox();
            label2 = new Label();
            numearlydaystxt = new TextBox();
            EarlyFeetxt = new TextBox();
            label3 = new Label();
            label4 = new Label();
            latefeetxt = new TextBox();
            totalfeetxt = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            feeEarlytxt = new TextBox();
            feelatetxt = new TextBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(46, 63);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "ID";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(370, 27);
            textBox1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(43, 136);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(376, 27);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // codition_cb
            // 
            codition_cb.FormattingEnabled = true;
            codition_cb.Location = new Point(43, 684);
            codition_cb.Name = "codition_cb";
            codition_cb.Size = new Size(376, 28);
            codition_cb.TabIndex = 3;
            codition_cb.SelectedIndexChanged += codition_cb_SelectedIndexChanged;
            codition_cb.SelectedValueChanged += codition_cb_SelectedValueChanged;
            codition_cb.TextChanged += codition_cb_TextChanged;
            // 
            // recordbtn
            // 
            recordbtn.Location = new Point(218, 718);
            recordbtn.Name = "recordbtn";
            recordbtn.Size = new Size(94, 29);
            recordbtn.TabIndex = 4;
            recordbtn.Text = "Submit";
            recordbtn.UseVisualStyleBackColor = true;
            recordbtn.Click += button1_Click;
            // 
            // num_daystxt
            // 
            num_daystxt.Location = new Point(45, 354);
            num_daystxt.Name = "num_daystxt";
            num_daystxt.ReadOnly = true;
            num_daystxt.Size = new Size(371, 27);
            num_daystxt.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 328);
            label1.Name = "label1";
            label1.Size = new Size(373, 20);
            label1.TabIndex = 6;
            label1.Text = "Number of late days (each late day will cost 20% extra)";
            // 
            // numlatedaystxt
            // 
            numlatedaystxt.Location = new Point(43, 365);
            numlatedaystxt.Name = "numlatedaystxt";
            numlatedaystxt.ReadOnly = true;
            numlatedaystxt.Size = new Size(371, 27);
            numlatedaystxt.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 257);
            label2.Name = "label2";
            label2.Size = new Size(398, 20);
            label2.TabIndex = 8;
            label2.Text = "Number of Planned Dates (Agreed amount of days to rent)";
            // 
            // numearlydaystxt
            // 
            numearlydaystxt.Location = new Point(49, 281);
            numearlydaystxt.Name = "numearlydaystxt";
            numearlydaystxt.ReadOnly = true;
            numearlydaystxt.Size = new Size(371, 27);
            numearlydaystxt.TabIndex = 9;
            // 
            // EarlyFeetxt
            // 
            EarlyFeetxt.Location = new Point(43, 432);
            EarlyFeetxt.Name = "EarlyFeetxt";
            EarlyFeetxt.ReadOnly = true;
            EarlyFeetxt.Size = new Size(371, 27);
            EarlyFeetxt.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 406);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 11;
            label3.Text = "Early Fee";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 466);
            label4.Name = "label4";
            label4.Size = new Size(64, 20);
            label4.TabIndex = 12;
            label4.Text = "Late Fee";
            // 
            // latefeetxt
            // 
            latefeetxt.Location = new Point(44, 507);
            latefeetxt.Name = "latefeetxt";
            latefeetxt.ReadOnly = true;
            latefeetxt.Size = new Size(370, 27);
            latefeetxt.TabIndex = 13;
            // 
            // totalfeetxt
            // 
            totalfeetxt.Location = new Point(48, 584);
            totalfeetxt.Name = "totalfeetxt";
            totalfeetxt.ReadOnly = true;
            totalfeetxt.Size = new Size(366, 27);
            totalfeetxt.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(49, 552);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 15;
            label5.Text = "Total Fee";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(46, 178);
            label6.Name = "label6";
            label6.Size = new Size(102, 20);
            label6.TabIndex = 16;
            label6.Text = "Early Rate Fee";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(350, 178);
            label7.Name = "label7";
            label7.Size = new Size(98, 20);
            label7.TabIndex = 17;
            label7.Text = "Late Rate Fee";
            // 
            // feeEarlytxt
            // 
            feeEarlytxt.Location = new Point(46, 209);
            feeEarlytxt.Name = "feeEarlytxt";
            feeEarlytxt.ReadOnly = true;
            feeEarlytxt.Size = new Size(125, 27);
            feeEarlytxt.TabIndex = 18;
            // 
            // feelatetxt
            // 
            feelatetxt.Location = new Point(316, 209);
            feelatetxt.Name = "feelatetxt";
            feelatetxt.ReadOnly = true;
            feelatetxt.Size = new Size(125, 27);
            feelatetxt.TabIndex = 19;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 642);
            label8.Name = "label8";
            label8.Size = new Size(564, 20);
            label8.TabIndex = 20;
            label8.Text = "Lost equipment adds full cost to the fee; damaged equipment adds 60% of the cost.";
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(46, 93);
            label9.Name = "label9";
            label9.Size = new Size(88, 20);
            label9.TabIndex = 21;
            label9.Text = "Return Date";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(43, 30);
            label10.Name = "label10";
            label10.Size = new Size(122, 20);
            label10.TabIndex = 22;
            label10.Text = "Return Record ID";
            label10.Click += label10_Click;
            // 
            // Return_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 801);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(feelatetxt);
            Controls.Add(feeEarlytxt);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(totalfeetxt);
            Controls.Add(latefeetxt);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(EarlyFeetxt);
            Controls.Add(numearlydaystxt);
            Controls.Add(label2);
            Controls.Add(numlatedaystxt);
            Controls.Add(label1);
            Controls.Add(num_daystxt);
            Controls.Add(recordbtn);
            Controls.Add(codition_cb);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox1);
            Name = "Return_form";
            Text = "Return_form";
            Load += Return_form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private ComboBox codition_cb;
        private Button recordbtn;
        private TextBox num_daystxt;
        private Label label1;
        private TextBox numlatedaystxt;
        private Label label2;
        private TextBox numearlydaystxt;
        private TextBox EarlyFeetxt;
        private Label label3;
        private Label label4;
        private TextBox latefeetxt;
        private TextBox totalfeetxt;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox feeEarlytxt;
        private TextBox feelatetxt;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}