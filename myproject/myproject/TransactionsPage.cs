using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace myproject
{
    public partial class TransactionsPage : UserControl
    {
        EquipmentDBContext dbcontext;
        public TransactionsPage()
        {
            InitializeComponent();
        }

        private void TransactionsPage_Load(object sender, EventArgs e)
        {

            dbcontext = new EquipmentDBContext();
            refreshTransactionsGridView();
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.Refresh();

            try
            {
                ddlequipmentfilter.DataSource = dbcontext.Equipment.ToList();
                ddlequipmentfilter.DisplayMember = "EquipmentName";
                ddlequipmentfilter.ValueMember = "EquipmentId";
                ddlequipmentfilter.SelectedItem = null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void refreshTransactionsGridView()
        {
            dataGridView1.DataSource = null;
            var rentalTransaction = dbcontext.RentalTransactions.AsQueryable();
            if (txtfilterno.Text != "")
            {
                rentalTransaction = rentalTransaction.Where(x => x.RequestId == Convert.ToInt32(txtfilterno.Text));
            }
            if (ddlequipmentfilter.SelectedValue != null)
            {
                rentalTransaction = rentalTransaction.Where(x => x.EquipmentId == Convert.ToInt32(ddlequipmentfilter.SelectedValue));
            }

            dataGridView1.DataSource = rentalTransaction.Select(e => new
            {
                Transaction_ID = e.TransactionId,
                Rental_Start_Date = e.RentalStartDate,
                Rental_Return_Date = e.RentalReturnDate,
                Rental_Period = e.RentalPeriod,
                Rental_Fee = e.RentalFee,
                Deposit = e.Deposit,
                Equipment_ID = e.EquipmentId,
                Equipment_Name = e.Equipment != null ? e.Equipment.EquipmentName : "N/A",
                Request_ID = e.RequestId,
                Payment_Status = e.PaymentStatus.PaymentStatusName
            }
                            )
                    .ToList();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Please select a rental request to create a transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int firstcell = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value);
            RentalTransaction transaction = dbcontext.RentalTransactions.Single(x => x.TransactionId == firstcell);
            CreateTransactionPage transactionPage = new CreateTransactionPage(transaction);
            transactionPage.ShowDialog();
            refreshTransactionsGridView();

        }

        private void txtfilterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void Filter_Click(object sender, EventArgs e)
        {
            refreshTransactionsGridView();
        }
        private void btn_return_Click(object sender, EventArgs e)
        {
            int selectedTransc = (int) dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;

            Return_form return_Form = new Return_form(selectedTransc);

            return_Form.ShowDialog();



        }
    }
}
