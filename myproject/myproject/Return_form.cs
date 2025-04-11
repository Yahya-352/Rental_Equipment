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
    public partial class Return_form : Form
    {
        RentalTransaction transc;
        EquipmentDBContext context;

        Decimal fee;
        public Return_form()
        {
            InitializeComponent();
            context = new EquipmentDBContext();
        }
        public Return_form(int TranscID)
        {
            InitializeComponent();


            context = new EquipmentDBContext();
            codition_cb.DataSource = context.EquipmentConditions.ToList();

            codition_cb.DisplayMember = "ConditionName";

            codition_cb.ValueMember = "ConditionId";


            //codition_cb.SelectedValue = 2;


            transc = context.RentalTransactions
                        .Include(t => t.Equipment)  
                        .FirstOrDefault(t => t.TransactionId == TranscID);

            textBox1.Text = transc.TransactionId.ToString();

            dateTimePicker1.Value = transc.RentalReturnDate.Value;
            calculateLateDays();

        }


        private void loadTrans()
        {

        }

        private void Return_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Create a new ReturnRecord object
                ReturnRecord returnRecord = new ReturnRecord
                {
                    // Set values from the transc (RentalTransaction) object
                    ActualReturnDate = dateTimePicker1.Value, // Actual return date from DateTimePicker
                    LateReturnFees = fee, // Late fee calculated
                   // AdditionalCharges = 0, // Set additional charges if applicable, or calculate accordingly
                    ConditionId = (int)codition_cb.SelectedValue, // Selected condition from ComboBox (must be an integer)
                    TransactionId = transc.TransactionId // The associated Transaction ID
                };

                // Add the new ReturnRecord to the context
                context.ReturnRecords.Add(returnRecord);

                // Save changes to the database
                context.SaveChanges();

                // Optionally, show a message box or perform other actions after saving
                MessageBox.Show("Return record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void calculateLateDays()
        {



            var plan_date = transc.RentalReturnDate.Value;

            var start_dat = transc.RentalStartDate.Value;

            var actual_date = dateTimePicker1.Value;

            int EarlyDays = Convert.ToInt32((plan_date.Date - start_dat.Date).Days);

            int lateDays = Convert.ToInt32((actual_date.Date - plan_date.Date).Days);


            if (Convert.ToInt32((actual_date.Date - plan_date.Date).Days) < 0)
            {
                MessageBox.Show("Invalid Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = transc.RentalReturnDate.Value;
                return;
            }
            else
            {

                Decimal late_fee = +Convert.ToDecimal((lateDays * transc.RentalFee)
                    + (lateDays * (transc.RentalFee / 100) * 20));

                Decimal Early_fee = Convert.ToDecimal(
                    (EarlyDays * transc.RentalFee));


                this.fee = Convert.ToDecimal(
                   Early_fee + late_fee);

                numearlydaystxt.Text = EarlyDays.ToString();

                numlatedaystxt.Text = lateDays.ToString();

                feeEarlytxt.Text = transc.RentalFee.ToString();

                feelatetxt.Text = Convert.ToDecimal((transc.RentalFee + transc.RentalFee / 100 * 20)).ToString();

                EarlyFeetxt.Text = Early_fee.ToString();

                latefeetxt.Text = late_fee.ToString();

                totalfeetxt.Text = fee.ToString();

            }







        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


            calculateLateDays();

        }

        private void codition_cb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void codition_cb_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (codition_cb.SelectedValue == null) return;
                if (this.transc?.Equipment == null) return;

                var selectedValue = codition_cb.SelectedValue.ToString();
                calculateLateDays();

                if (selectedValue == "2")
                {
                    //if lost customer have to pay equioment price full
                    this.fee += Convert.ToDecimal(this.transc.Equipment.Cost);


                }
                else if (selectedValue == "3")  
                {
                    //item damaged customer pays 60% of cost
                    this.fee += Convert.ToDecimal(this.transc.Equipment.Cost)*60/100;
                }
                else
                {
                    calculateLateDays();
                }

                // Update UI
                totalfeetxt.Text = this.fee.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void codition_cb_TextChanged(object sender, EventArgs e)
        {
 
        }
    }
}
