namespace myproject
{
    partial class Admin_ManageUsers
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
            panel1 = new Panel();
            B_Update = new Button();
            CB_Role = new ComboBox();
            TB_Password = new TextBox();
            TB_Email = new TextBox();
            TB_Username = new TextBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            SearchGrid = new TextBox();
            B_DeleteUser = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(19, 15, 64);
            panel1.Controls.Add(B_Update);
            panel1.Controls.Add(CB_Role);
            panel1.Controls.Add(TB_Password);
            panel1.Controls.Add(TB_Email);
            panel1.Controls.Add(TB_Username);
            panel1.Location = new Point(0, 534);
            panel1.Name = "panel1";
            panel1.Size = new Size(1177, 129);
            panel1.TabIndex = 0;
            // 
            // B_Update
            // 
            B_Update.Enabled = false;
            B_Update.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            B_Update.Location = new Point(1009, 31);
            B_Update.Name = "B_Update";
            B_Update.Size = new Size(133, 55);
            B_Update.TabIndex = 4;
            B_Update.Text = "UPDATE";
            B_Update.UseVisualStyleBackColor = true;
            B_Update.Click += B_Update_Click;
            // 
            // CB_Role
            // 
            CB_Role.Enabled = false;
            CB_Role.FormattingEnabled = true;
            CB_Role.Items.AddRange(new object[] { "Administrator", "Staff", "User" });
            CB_Role.Location = new Point(798, 43);
            CB_Role.Name = "CB_Role";
            CB_Role.Size = new Size(182, 33);
            CB_Role.TabIndex = 3;
            CB_Role.Text = "Select Role";
            // 
            // TB_Password
            // 
            TB_Password.AccessibleDescription = "";
            TB_Password.AccessibleName = "";
            TB_Password.Enabled = false;
            TB_Password.Location = new Point(535, 43);
            TB_Password.Multiline = true;
            TB_Password.Name = "TB_Password";
            TB_Password.PlaceholderText = "New Password";
            TB_Password.Size = new Size(237, 34);
            TB_Password.TabIndex = 2;
            TB_Password.Tag = "New Password";
            TB_Password.Text = "New Password";
            // 
            // TB_Email
            // 
            TB_Email.Enabled = false;
            TB_Email.Location = new Point(244, 43);
            TB_Email.Multiline = true;
            TB_Email.Name = "TB_Email";
            TB_Email.Size = new Size(262, 34);
            TB_Email.TabIndex = 1;
            TB_Email.Tag = "Email";
            TB_Email.Text = "Email";
            // 
            // TB_Username
            // 
            TB_Username.Enabled = false;
            TB_Username.Location = new Point(32, 43);
            TB_Username.Multiline = true;
            TB_Username.Name = "TB_Username";
            TB_Username.Size = new Size(183, 34);
            TB_Username.TabIndex = 0;
            TB_Username.Tag = "User Name";
            TB_Username.Text = "User Name";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(32, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(1110, 427);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(32, 30);
            label1.Name = "label1";
            label1.Size = new Size(236, 32);
            label1.TabIndex = 2;
            label1.Text = "Search by user name:";
            // 
            // SearchGrid
            // 
            SearchGrid.Location = new Point(274, 33);
            SearchGrid.Name = "SearchGrid";
            SearchGrid.Size = new Size(167, 31);
            SearchGrid.TabIndex = 3;
            SearchGrid.TextChanged += SearchGrid_TextChanged;
            // 
            // B_DeleteUser
            // 
            B_DeleteUser.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            B_DeleteUser.Location = new Point(967, 27);
            B_DeleteUser.Name = "B_DeleteUser";
            B_DeleteUser.Size = new Size(175, 40);
            B_DeleteUser.TabIndex = 4;
            B_DeleteUser.Text = "Delete User";
            B_DeleteUser.UseVisualStyleBackColor = true;
            B_DeleteUser.Click += B_DeleteUser_Click;
            // 
            // Admin_ManageUsers
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1173, 658);
            Controls.Add(B_DeleteUser);
            Controls.Add(SearchGrid);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "Admin_ManageUsers";
            Text = "Admin_ManageUsers";
            Load += Admin_ManageUsers_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button B_Update;
        private ComboBox CB_Role;
        private TextBox TB_Password;
        private TextBox TB_Email;
        private TextBox TB_Username;
        private DataGridView dataGridView1;
        private Label label1;
        private TextBox SearchGrid;
        private Button B_DeleteUser;
    }
}