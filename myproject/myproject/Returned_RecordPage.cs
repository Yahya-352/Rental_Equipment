using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myproject_Library.Model;

namespace myproject
{
    public partial class Returned_RecordPage : Form
    {
        EquipmentDBContext context;

        public Returned_RecordPage()
        {
            InitializeComponent();
            context = new EquipmentDBContext();

            dgvReturn.DataSource = context.ReturnRecords.ToList();
        }

        private void Returned_RecordPage_Load(object sender, EventArgs e)
        {

        }
    }
}
