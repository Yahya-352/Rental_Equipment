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
            refresh();


        }

        private void refresh()
        {
            dgvReturn.DataSource = context.ReturnRecords
                .Select(g => new
                {
                    ReturnID = g.ReturnId,
                    Returned_Date = g.ActualReturnDate,
                    Fee = g.LateReturnFees,
                    Codition = g.Condition.ConditionName,
                    TranscID = g.TransactionId


                }).ToList();

        }

        private void Returned_RecordPage_Load(object sender, EventArgs e)
        {



        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int selectedReturnID = (int)dgvReturn.SelectedCells[0].OwningRow.Cells[0].Value;
            new Return_form(selectedReturnID, true).Show();
            refresh();
        }

        private void deleteReturnbtn_Click(object sender, EventArgs e)
        {
            
                int selectedRecord = (int)dgvReturn.SelectedCells[0].OwningRow.Cells[0].Value;

                context.ReturnRecords.Remove((context.ReturnRecords.Find(selectedRecord)));

                context.SaveChanges();
        }
    }
}
