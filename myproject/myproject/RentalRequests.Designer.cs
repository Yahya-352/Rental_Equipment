namespace myproject
{
    partial class RentalRequests
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
            Reject = new Button();
            Approve = new Button();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            ddlstatusfilter = new ComboBox();
            button2 = new Button();
            txtfilterno = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Reject
            // 
            Reject.BackColor = Color.Red;
            Reject.ForeColor = SystemColors.ControlLightLight;
            Reject.Location = new Point(273, 348);
            Reject.Name = "Reject";
            Reject.Size = new Size(94, 29);
            Reject.TabIndex = 16;
            Reject.Text = "Reject";
            Reject.UseVisualStyleBackColor = false;
            Reject.Click += Reject_Click;
            // 
            // Approve
            // 
            Approve.BackColor = Color.Green;
            Approve.ForeColor = SystemColors.ControlLightLight;
            Approve.Location = new Point(164, 348);
            Approve.Name = "Approve";
            Approve.Size = new Size(94, 29);
            Approve.TabIndex = 15;
            Approve.Text = "Approve";
            Approve.UseVisualStyleBackColor = false;
            Approve.Click += Approve_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(19, 15, 64);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(685, 46);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 14;
            button1.Text = "Filter";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(83, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(696, 261);
            dataGridView1.TabIndex = 13;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // ddlstatusfilter
            // 
            ddlstatusfilter.FormattingEnabled = true;
            ddlstatusfilter.Location = new Point(475, 44);
            ddlstatusfilter.Name = "ddlstatusfilter";
            ddlstatusfilter.Size = new Size(151, 28);
            ddlstatusfilter.TabIndex = 12;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(19, 15, 64);
            button2.ForeColor = SystemColors.ControlLightLight;
            button2.Location = new Point(622, 349);
            button2.Name = "button2";
            button2.Size = new Size(157, 29);
            button2.TabIndex = 17;
            button2.Text = "Create Transaction";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // txtfilterno
            // 
            txtfilterno.Location = new Point(199, 45);
            txtfilterno.Name = "txtfilterno";
            txtfilterno.Size = new Size(125, 27);
            txtfilterno.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(407, 48);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 19;
            label1.Text = "Status:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(115, 50);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 20;
            label2.Text = "Username:";
            // 
            // RentalRequests
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtfilterno);
            Controls.Add(button2);
            Controls.Add(Reject);
            Controls.Add(Approve);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(ddlstatusfilter);
            Name = "RentalRequests";
            Size = new Size(879, 402);
            Load += RentalRequests_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Reject;
        private Button Approve;
        private Button button1;
        private DataGridView dataGridView1;
        private ComboBox ddlstatusfilter;
        private Button button2;
        private TextBox txtfilterno;
        private Label label1;
        private Label label2;
    }
}
