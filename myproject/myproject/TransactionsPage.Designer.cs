namespace myproject
{
    partial class TransactionsPage
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
            Edit = new Button();
            dataGridView1 = new DataGridView();
            Filter = new Button();
            ddlequipmentfilter = new ComboBox();
            txtfilterno = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Edit
            // 
            Edit.BackColor = Color.FromArgb(19, 15, 64);
            Edit.ForeColor = SystemColors.ControlLightLight;
            Edit.Location = new Point(105, 353);
            Edit.Name = "Edit";
            Edit.Size = new Size(94, 29);
            Edit.TabIndex = 10;
            Edit.Text = "Edit";
            Edit.UseVisualStyleBackColor = false;
            Edit.Click += Edit_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(73, 72);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(732, 263);
            dataGridView1.TabIndex = 8;
            // 
            // Filter
            // 
            Filter.BackColor = Color.FromArgb(19, 15, 64);
            Filter.ForeColor = SystemColors.ControlLightLight;
            Filter.Location = new Point(686, 19);
            Filter.Name = "Filter";
            Filter.Size = new Size(94, 29);
            Filter.TabIndex = 7;
            Filter.Text = "Filter";
            Filter.UseVisualStyleBackColor = false;
            Filter.Click += Filter_Click;
            // 
            // ddlequipmentfilter
            // 
            ddlequipmentfilter.FormattingEnabled = true;
            ddlequipmentfilter.Location = new Point(472, 19);
            ddlequipmentfilter.Name = "ddlequipmentfilter";
            ddlequipmentfilter.Size = new Size(151, 28);
            ddlequipmentfilter.TabIndex = 6;
            // 
            // txtfilterno
            // 
            txtfilterno.Location = new Point(207, 21);
            txtfilterno.Name = "txtfilterno";
            txtfilterno.Size = new Size(125, 27);
            txtfilterno.TabIndex = 11;
            txtfilterno.TextChanged += txtfilterno_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(382, 24);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 12;
            label1.Text = "Equipment:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(115, 24);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 13;
            label2.Text = "Request ID:";
            // 
            // TransactionsPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtfilterno);
            Controls.Add(Edit);
            Controls.Add(dataGridView1);
            Controls.Add(Filter);
            Controls.Add(ddlequipmentfilter);
            Name = "TransactionsPage";
            Size = new Size(879, 402);
            Load += TransactionsPage_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Edit;
        private DataGridView dataGridView1;
        private Button Filter;
        private ComboBox ddlequipmentfilter;
        private TextBox txtfilterno;
        private Label label1;
        private Label label2;
    }
}
