namespace myproject
{
    partial class Logs
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
            dgvLogs = new DataGridView();
            btnFilter = new Button();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            lblFrom = new Label();
            lblTo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // dgvLogs
            // 
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(38, 78);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.RowTemplate.Height = 29;
            dgvLogs.Size = new Size(798, 344);
            dgvLogs.TabIndex = 0;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = SystemColors.MenuText;
            btnFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnFilter.ForeColor = SystemColors.ControlLightLight;
            btnFilter.Location = new Point(700, 43);
            btnFilter.Margin = new Padding(0);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(123, 28);
            btnFilter.TabIndex = 1;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // dtpFrom
            // 
            dtpFrom.Checked = false;
            dtpFrom.Location = new Point(103, 42);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Size = new Size(250, 27);
            dtpFrom.TabIndex = 2;
            // 
            // dtpTo
            // 
            dtpTo.Checked = false;
            dtpTo.Location = new Point(423, 42);
            dtpTo.Name = "dtpTo";
            dtpTo.ShowCheckBox = true;
            dtpTo.Size = new Size(250, 27);
            dtpTo.TabIndex = 3;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblFrom.Location = new Point(51, 46);
            lblFrom.Margin = new Padding(0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(49, 20);
            lblFrom.TabIndex = 4;
            lblFrom.Text = "From:";
            lblFrom.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTo.Location = new Point(388, 47);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(29, 20);
            lblTo.TabIndex = 5;
            lblTo.Text = "To:";
            // 
            // Logs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 450);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(btnFilter);
            Controls.Add(dgvLogs);
            Name = "Logs";
            Text = "Logs";
            Load += Logs_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLogs;
        private Button btnFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblFrom;
        private Label lblTo;
    }
}