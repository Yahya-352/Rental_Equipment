using myproject.configs;
using myproject.services;
using myproject_Library.Model;

namespace myproject
{
    public partial class Form1 : Form
    {
        private AuthService _authService;
        public Form1()
        {
            InitializeComponent();
            _authService = ServiceConfigurator.GetService<AuthService>();
            LoadPage(new Dashboard());

        }

        private void LoadPage(UserControl page)
        {
            panelContainer.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(page);
        }

        private void logsAndAuditToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void returnRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPage(new Equipments());


        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage(new RentalRequests());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadPage(new TransactionsPage());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage(new Dashboard());

        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            bool isAdmin = await _authService.IsInRoleAsync("Administrator");

            // Handle ManageUsersButton - hide for non-admin users
            ManageUsersButton.Visible = isAdmin;  // Hide instead of just disable

            // Handle btnLogs - change color and enabled state based on user role
            if (isAdmin)
            {
                btnLogs.Enabled = true;
                btnLogs.BackColor = SystemColors.Control;  // Default button color
            }
            else
            {
                btnLogs.Enabled = false;
                btnLogs.BackColor = Color.Gray;  // Gray color for disabled state
                label1.Text = "Staff";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Returned_RecordPage().Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new Logs().Show();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _authService.Logout();
            Login login = new();
            this.Hide();
            login.Show();
        }

        private void ManageUsersButton_Click(object sender, EventArgs e)
        {
            var usersForm = new Admin_ManageUsers();
            usersForm.Show();
        }
    }
}
