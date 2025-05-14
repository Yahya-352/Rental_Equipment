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
using myproject.configs;
using myproject.services;
using myproject_Library.Model;

namespace myproject
{
    public partial class Return_form : Form
    {
        RentalTransaction transc;
        EquipmentDBContext context;
        private AuthService _authService;

        bool Update = false;
        int return_id = -1;
        Decimal fee;
        private int _finalConditionId = -1;

        public Return_form()
        {
            InitializeComponent();
            context = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();
        }

        public Return_form(int Return, bool update)
        {
            InitializeComponent();
            context = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();
            this.Update = true;
            this.return_id = Return;
            loadTrans();

            try
            {
                transc = context.RentalTransactions
                       .Include(t => t.Equipment)
                       .Include(t => t.ReturnRecords)
                       .FirstOrDefault(t => t.ReturnRecords.Any(rr => rr.ReturnId == Return));

                if (transc != null)
                {
                    textBox1.Text = transc.TransactionId.ToString();

                    var returnRecord = transc.ReturnRecords.FirstOrDefault(rr => rr.ReturnId == Return);
                    if (returnRecord != null)
                    {
                        dateTimePicker1.Value = returnRecord.ActualReturnDate ?? DateTime.Now;
                        if (returnRecord.ConditionId.HasValue)
                        {
                            _finalConditionId = returnRecord.ConditionId.Value;
                        }
                    }
                    codition_cb.SelectedItem = transc.Equipment.ConditionId.Value;


                    if (_finalConditionId != -1)
                    {
                        codition_cb.SelectedValue = _finalConditionId;
                    }

                    calculateLateDays();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Return_form(int TranscID)
        {
            InitializeComponent();
            context = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();

            try
            {
                transc = context.RentalTransactions
                            .Include(t => t.Equipment)
                            .FirstOrDefault(t => t.TransactionId == TranscID);

                if (transc != null)
                {
                    textBox1.Text = transc.TransactionId.ToString();
                    dateTimePicker1.Value = transc.RentalReturnDate ?? DateTime.Now;

                    loadTrans();
                    calculateLateDays();
                }
                else
                {
                    MessageBox.Show("Transaction not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadTrans()
        {
            try
            {
                var conditions = context.EquipmentConditions.ToList();
                codition_cb.DataSource = conditions;
                codition_cb.DisplayMember = "ConditionName";
                codition_cb.ValueMember = "ConditionId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading conditions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Return_form_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_finalConditionId == -1)
                {
                    MessageBox.Show("Please select a condition.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (transc == null)
                {
                    MessageBox.Show("Transaction not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var equipment = context.Equipment
                    .FirstOrDefault(eq => eq.EquipmentId == transc.EquipmentId);

                if (equipment == null)
                {
                    MessageBox.Show("Equipment not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.Update)
                {
                    int returnId = this.return_id;

                    var returnRecord = context.ReturnRecords
                        .FirstOrDefault(r => r.ReturnId == returnId);

                    if (returnRecord == null)
                    {
                        MessageBox.Show("Return record not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    returnRecord.ActualReturnDate = dateTimePicker1.Value;
                    returnRecord.LateReturnFees = fee;
                    returnRecord.ConditionId = _finalConditionId;

                    equipment.AvailabilityStatusId = _finalConditionId;
                    equipment.ConditionId = _finalConditionId;

                    context.SaveChanges();

                    MessageBox.Show("Return record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var returnRecord = new ReturnRecord
                    {
                        ActualReturnDate = dateTimePicker1.Value,
                        LateReturnFees = fee,
                        ConditionId = _finalConditionId,
                        TransactionId = transc.TransactionId
                    };

                    equipment.ConditionId = _finalConditionId;
                    equipment.AvailabilityStatusId = 1;

                    context.ReturnRecords.Add(returnRecord);
                    context.SaveChanges();

                    MessageBox.Show("Return record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    context.Logs.Add(new Log
                    {
                        Action = "Error",
                        Exception = ex.Message,
                        Timestamp = DateTime.Now,
                        Source = "Return_Record",
                        UserId = _authService?.CurrentUser?.Id ?? 0,
                        AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                    });

                    context.SaveChanges();
                }
                catch
                {
                    // If logging fails, don't crash the application
                }

                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void calculateLateDays()
        {
            try
            {
                if (transc == null || !transc.RentalReturnDate.HasValue || !transc.RentalStartDate.HasValue)
                {
                    return;
                }

                var plan_date = transc.RentalReturnDate.Value;
                var start_dat = transc.RentalStartDate.Value;
                var actual_date = dateTimePicker1.Value;

                int EarlyDays = Math.Max(0, (plan_date.Date - start_dat.Date).Days);
                int lateDays = Math.Max(0, (actual_date.Date - plan_date.Date).Days);

                if (actual_date.Date < start_dat.Date)
                {
                    MessageBox.Show("Return date cannot be before rental start date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dateTimePicker1.Value = plan_date;
                    return;
                }

                Decimal rentalFee = transc.RentalFee.HasValue ? transc.RentalFee.Value : 0;

                Decimal late_fee = lateDays * rentalFee +
                                  (lateDays * (rentalFee / 100) * 20);

                Decimal Early_fee = EarlyDays * rentalFee;

                this.fee = Early_fee + late_fee;

                if (numearlydaystxt != null) numearlydaystxt.Text = EarlyDays.ToString();
                if (numlatedaystxt != null) numlatedaystxt.Text = lateDays.ToString();
                if (feeEarlytxt != null) feeEarlytxt.Text = transc.RentalFee.ToString();
                if (feelatetxt != null) feelatetxt.Text = (transc.RentalFee + transc.RentalFee / 100 * 20).ToString();
                if (EarlyFeetxt != null) EarlyFeetxt.Text = Early_fee.ToString();
                if (latefeetxt != null) latefeetxt.Text = late_fee.ToString();
                if (totalfeetxt != null) totalfeetxt.Text = fee.ToString();

                if (_finalConditionId > 0)
                {
                    UpdateFeeBasedOnCondition();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating fees: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFeeBasedOnCondition()
        {
            if (transc?.Equipment == null) return;

            if (_finalConditionId == 2)
            {
                this.fee += transc.Equipment.Cost.HasValue ? transc.Equipment.Cost.Value : 0;
            }
            else if (_finalConditionId == 3)
            {
                decimal equipmentCost = transc.Equipment.Cost.HasValue ? transc.Equipment.Cost.Value : 0;
                this.fee += equipmentCost * 0.6m;
            }

            if (totalfeetxt != null)
            {
                totalfeetxt.Text = this.fee.ToString();
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
                if (transc?.Equipment == null) return;

                if (codition_cb.SelectedValue is int selectedInt)
                {
                    _finalConditionId = selectedInt;
                }
                else
                {
                    if (int.TryParse(codition_cb.SelectedValue.ToString(), out int parsedValue))
                    {
                        _finalConditionId = parsedValue;
                    }
                    else
                    {
                        return;
                    }
                }

                calculateLateDays();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing condition change: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void codition_cb_TextChanged(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
