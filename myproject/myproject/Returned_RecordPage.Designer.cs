namespace myproject
{
    partial class Returned_RecordPage
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
            dgvReturn = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReturn).BeginInit();
            SuspendLayout();
            // 
            // dgvReturn
            // 
            dgvReturn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReturn.Location = new Point(42, 65);
            dgvReturn.Name = "dgvReturn";
            dgvReturn.RowHeadersWidth = 51;
            dgvReturn.RowTemplate.Height = 29;
            dgvReturn.Size = new Size(688, 322);
            dgvReturn.TabIndex = 0;
            // 
            // Returned_RecordPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvReturn);
            Name = "Returned_RecordPage";
            Text = "Returned_RecordPage";
            Load += Returned_RecordPage_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReturn).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvReturn;
    }
}