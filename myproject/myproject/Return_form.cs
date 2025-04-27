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

        bool Update = false;
        int return_id = -1;
        Decimal fee;
        private int _finalConditionId = -1;

        public Return_form()
        {
            InitializeComponent();
            context = new EquipmentDBContext();
        }

        public Return_form(int Return, bool update)
        {
            InitializeComponent();
            context = new EquipmentDBContext();
            this.Update = true;
            this.return_id= Return;
            loadTrans();

            transc = context.RentalTransactions
                   .Include(t => t.Equipment)
                   .ThenInclude(x => x.RentalRequests)
                   .Include(t => t.ReturnRecords)
                   .FirstOrDefault(t => t.ReturnRecords.Any(rr => rr.ReturnId == Return));

            if (transc != null)
            {
                textBox1.Text = transc.TransactionId.ToString();

                var returnRecord = transc.ReturnRecords.FirstOrDefault(rr => rr.ReturnId == Return);
                if (returnRecord != null)
                {
                    dateTimePicker1.Value = returnRecord.ActualReturnDate.Value;
                    codition_cb.SelectedValue = returnRecord.ConditionId;
                }

                calculateLateDays();
            }
        }
        public Return_form(int TranscID)
        {
            InitializeComponent();
            loadTrans();
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
            context = new EquipmentDBContext();
            codition_cb.DataSource = context.EquipmentConditions.ToList();

            codition_cb.DisplayMember = "ConditionName";

            codition_cb.ValueMember = "ConditionId";
        }

        private void Return_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnRecord returnRecord;
                if (this.Update)
                {
                    int returnId = this.return_id;

                    // Load ReturnRecord + Transaction ONLY (no Equipment here)
                    returnRecord = context.ReturnRecords
                        .Include(r => r.Transaction)
                        .FirstOrDefault(r => r.ReturnId == returnId);

                    if (returnRecord == null)
                    {
                        MessageBox.Show("Return record not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_finalConditionId == -1)
                    {
                        MessageBox.Show("Condition not selected.");
                        return;
                    }

                    // Reload Equipment separately to avoid tracking conflicts
                    var equipment = context.Equipment
                        .FirstOrDefault(e => e.EquipmentId == returnRecord.Transaction.EquipmentId);

                    if (equipment == null)
                    {
                        MessageBox.Show("Equipment not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ Apply user-chosen updates
                    returnRecord.ActualReturnDate = dateTimePicker1.Value;
                    returnRecord.LateReturnFees = fee;
                    returnRecord.ConditionId = _finalConditionId;

                    equipment.ConditionId = _finalConditionId;

                    // 🔒 Explicitly mark both as modified
                    context.Entry(returnRecord).Property(r => r.ConditionId).IsModified = true;
                    context.Entry(returnRecord).Property(r => r.ActualReturnDate).IsModified = true;
                    context.Entry(returnRecord).Property(r => r.LateReturnFees).IsModified = true;

                    context.Entry(equipment).Property(e => e.ConditionId).IsModified = true;

                    // 🧠 Optional debug tracking output
                    var tracker = context.ChangeTracker.Entries()
                        .Where(e => e.State != EntityState.Unchanged)
                        .ToList();

                    StringBuilder sb = new StringBuilder();
                    foreach (var entry in tracker)
                    {
                        sb.AppendLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                        foreach (var prop in entry.Properties)
                        {
                            if (prop.IsModified)
                            {
                                sb.AppendLine($"  {prop.Metadata.Name}: {prop.OriginalValue} → {prop.CurrentValue}");
                            }
                        }
                    }
                    MessageBox.Show(sb.ToString());

                    // ✅ Save changes
                    int rows = context.SaveChanges();
                    MessageBox.Show("Rows affected: " + rows);
                }

                else
                {
                    // Create a new return record
                    returnRecord = new ReturnRecord
                    {
                        ActualReturnDate = dateTimePicker1.Value,
                        LateReturnFees = fee,
                        ConditionId = (int)codition_cb.SelectedValue,
                        TransactionId = transc.TransactionId
                    };

                    context.ReturnRecords.Add(returnRecord);
                }

                // Save changes to the database
                context.SaveChanges();

                MessageBox.Show("Return record " + (this.Update ? "updated" : "added") + " successfully!",
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                context.Logs.Add(new Log
                {
                    Action = "Error",
                    Exception = ex.Message,
                    Timestamp = DateTime.Now,
                    Source = "Return_Record",
                    UserId = -1,
                    AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                });

                context.SaveChanges();
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
                 _finalConditionId = int.Parse(selectedValue);
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
