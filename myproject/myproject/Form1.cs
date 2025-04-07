using myproject_Library.Model;

namespace myproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadPage(UserControl page)
        {
            panelContainer.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(page);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
