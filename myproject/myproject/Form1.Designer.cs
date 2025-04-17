namespace myproject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            panelContainer = new Panel();
            panel2 = new Panel();
            btnLogs = new Button();
            btnLog = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(19, 15, 64);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1097, 68);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(75, 21);
            label2.Name = "label2";
            label2.Size = new Size(230, 25);
            label2.TabIndex = 1;
            label2.Text = "Equipment Rental System";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(694, 21);
            label1.Name = "label1";
            label1.Size = new Size(68, 25);
            label1.TabIndex = 0;
            label1.Text = "Admin";
            // 
            // panelContainer
            // 
            panelContainer.Location = new Point(0, 102);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1097, 402);
            panelContainer.TabIndex = 2;
            panelContainer.Paint += panelContainer_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDarkDark;
            panel2.Controls.Add(btnLogs);
            panel2.Controls.Add(btnLog);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Location = new Point(0, 68);
            panel2.Name = "panel2";
            panel2.Size = new Size(1097, 35);
            panel2.TabIndex = 3;
            // 
            // btnLogs
            // 
            btnLogs.Location = new Point(923, -1);
            btnLogs.Name = "btnLogs";
            btnLogs.Size = new Size(174, 36);
            btnLogs.TabIndex = 5;
            btnLogs.Text = "Logs";
            btnLogs.UseVisualStyleBackColor = true;
            btnLogs.Click += button5_Click_1;
            // 
            // btnLog
            // 
            btnLog.Location = new Point(740, -1);
            btnLog.Name = "btnLog";
            btnLog.Size = new Size(177, 36);
            btnLog.TabIndex = 4;
            btnLog.Text = "Return Requests";
            btnLog.UseVisualStyleBackColor = true;
            btnLog.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(555, -1);
            button4.Name = "button4";
            button4.Size = new Size(179, 36);
            button4.TabIndex = 3;
            button4.Text = "Transactions";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(370, -1);
            button3.Name = "button3";
            button3.Size = new Size(179, 36);
            button3.TabIndex = 2;
            button3.Text = "Requests";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(185, -1);
            button2.Name = "button2";
            button2.Size = new Size(179, 36);
            button2.TabIndex = 1;
            button2.Text = "Equipments";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(0, -1);
            button1.Name = "button1";
            button1.Size = new Size(179, 36);
            button1.TabIndex = 0;
            button1.Text = "Dashboard";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 502);
            Controls.Add(panel2);
            Controls.Add(panelContainer);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Label label1;
        private Panel panelContainer;
        private Panel panel2;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button btnLog;
        private Button btnLogs;
    }
}
