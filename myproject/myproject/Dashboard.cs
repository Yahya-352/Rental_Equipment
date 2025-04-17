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
    public partial class Dashboard : UserControl
    {
        EquipmentDBContext dbContext;
        public Dashboard()
        {
            InitializeComponent();
            dbContext = new EquipmentDBContext();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                LblTotalEquipments.Text = dbContext.Equipment.Count().ToString();

                LblPending.Text = dbContext.RentalRequests.Where(x => x.RequestStatusId == 1).Count().ToString();

                LblTotalRentals.Text = dbContext.RentalTransactions.Count().ToString();

                LblProfit.Text = dbContext.RentalTransactions.Sum(x => x.RentalFee).ToString();

                LblLateReturns.Text = dbContext.ReturnRecords.Count().ToString();

                //LblMostRentedEquipment.Text = dbContext.Equipment.OrderByDescending(x => x.RentalTransactions.Sum(y => y.RentalFee)).FirstOrDefault().EquipmentName.ToString();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
