namespace myproject
{
    partial class Equipments
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
            ddlequipmentfilter = new ComboBox();
            Filter = new Button();
            dataGridView1 = new DataGridView();
            Add = new Button();
            Edit = new Button();
            Delete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // ddlequipmentfilter
            // 
            ddlequipmentfilter.FormattingEnabled = true;
            ddlequipmentfilter.Location = new Point(91, 30);
            ddlequipmentfilter.Name = "ddlequipmentfilter";
            ddlequipmentfilter.Size = new Size(151, 28);
            ddlequipmentfilter.TabIndex = 0;
            // 
            // Filter
            // 
            Filter.BackColor = Color.FromArgb(19, 15, 64);
            Filter.ForeColor = SystemColors.ControlLightLight;
            Filter.Location = new Point(704, 30);
            Filter.Name = "Filter";
            Filter.Size = new Size(94, 29);
            Filter.TabIndex = 1;
            Filter.Text = "Filter";
            Filter.UseVisualStyleBackColor = false;
            Filter.Click += Filter_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(91, 83);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(732, 263);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Add
            // 
            Add.BackColor = Color.Green;
            Add.ForeColor = SystemColors.ControlLightLight;
            Add.Location = new Point(224, 366);
            Add.Name = "Add";
            Add.Size = new Size(94, 29);
            Add.TabIndex = 3;
            Add.Text = "Add";
            Add.UseVisualStyleBackColor = false;
            Add.Click += Add_Click;
            // 
            // Edit
            // 
            Edit.BackColor = Color.FromArgb(19, 15, 64);
            Edit.ForeColor = SystemColors.ControlLightLight;
            Edit.Location = new Point(343, 366);
            Edit.Name = "Edit";
            Edit.Size = new Size(94, 29);
            Edit.TabIndex = 4;
            Edit.Text = "Edit";
            Edit.UseVisualStyleBackColor = false;
            Edit.Click += Edit_Click;
            // 
            // Delete
            // 
            Delete.BackColor = Color.Red;
            Delete.ForeColor = SystemColors.ControlLightLight;
            Delete.Location = new Point(571, 366);
            Delete.Name = "Delete";
            Delete.Size = new Size(94, 29);
            Delete.TabIndex = 5;
            Delete.Text = "Delete";
            Delete.UseVisualStyleBackColor = false;
            Delete.Click += Delete_Click;
            // 
            // Equipments
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(Delete);
            Controls.Add(Edit);
            Controls.Add(Add);
            Controls.Add(dataGridView1);
            Controls.Add(Filter);
            Controls.Add(ddlequipmentfilter);
            Name = "Equipments";
            Size = new Size(879, 402);
            Load += Equipments_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox ddlequipmentfilter;
        private Button Filter;
        private DataGridView dataGridView1;
        private Button Add;
        private Button Edit;
        private Button Delete;
    }
}
