using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using myproject.configs;
using myproject.services;
using myproject_Library.Model;

namespace myproject
{
    public partial class CreateTransactionPage : Form
    {

        private int _requestId;
        private EquipmentDBContext dbcontext;
        RentalTransaction transaction;
        private decimal rentalPrice;
        private AuthService _authService;

        public CreateTransactionPage(int requestid)
        {
            InitializeComponent();
            _requestId = requestid;
            dbcontext = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();
        }

        public CreateTransactionPage(RentalTransaction rentalTransaction)
        {
            InitializeComponent();
            dbcontext = new EquipmentDBContext();
            transaction = dbcontext.RentalTransactions
            .Include(t => t.Equipment)
            .Include(t => t.PaymentStatus)
            .FirstOrDefault(t => t.TransactionId == rentalTransaction.TransactionId);
        }

        private void CreateTransactionPage_Load(object sender, EventArgs e)
        {
            try
            {
                PaymentComboBox.DataSource = dbcontext.PaymentStatuses.ToList();
                PaymentComboBox.DisplayMember = "PaymentStatusName";
                PaymentComboBox.ValueMember = "PaymentStatusId";
                PaymentComboBox.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payment statuses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (transaction != null)
            {
                txtStartDate.Text = transaction.RentalStartDate?.ToString("yyyy-MM-dd") ?? "N/A";
                txtReturnDate.Text = transaction.RentalReturnDate?.ToString("yyyy-MM-dd") ?? "N/A";
                RentalPeriodTextBox.Text = transaction.RentalPeriod?.ToString() ?? "0";
                txtTotalCost.Text = transaction.RentalFee?.ToString("C") ?? "0";
                Deposit.Text = transaction.Deposit?.ToString() ?? "0";
                EquipmentTextBox.Text = transaction.EquipmentId.ToString();
                RequestTextBox.Text = transaction.RequestId.ToString();
                PaymentComboBox.SelectedValue = transaction.PaymentStatusId;

                if (transaction.Equipment != null)
                {
                    rentalPrice = transaction.Equipment.RentalPrice.GetValueOrDefault();
                }

                if (transaction.EquipmentId != null)
                    HighlightRentedDatesForEquipment(transaction.EquipmentId.Value);
            }
            else
            {
                var request = dbcontext.RentalRequests.FirstOrDefault(r => r.RequestId == _requestId);
                if (request != null)
                {

                    txtStartDate.Text = request.StartDate?.ToString("yyyy-MM-dd") ?? "N/A";
                    txtReturnDate.Text = request.ReturnDate?.ToString("yyyy-MM-dd") ?? "N/A";
                    txtTotalCost.Text = request.TotalCost?.ToString("C") ?? "N/A";
                    EquipmentTextBox.Text = request.EquipmentId?.ToString() ?? "N/A";
                    RequestTextBox.Text = request.RequestStatusId?.ToString() ?? "N/A";
                    Deposit.Text = "0";

                    var equipment = dbcontext.Equipment.FirstOrDefault(e => e.EquipmentId == request.EquipmentId);
                    if (equipment != null && request.StartDate.HasValue && request.ReturnDate.HasValue)
                    {
                        TimeSpan rentalPeriod = request.ReturnDate.Value - request.StartDate.Value;
                        RentalPeriodTextBox.Text = rentalPeriod.Days.ToString();
                        rentalPrice = equipment.RentalPrice.GetValueOrDefault();


                        decimal rentalFee = equipment.RentalPrice.GetValueOrDefault() * rentalPeriod.Days;
                        txtTotalCost.Text = rentalFee.ToString("C");
                    }
                    if (request.EquipmentId != null)
                        HighlightRentedDatesForEquipment(request.EquipmentId.Value);
                }
            }
        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
        private void HighlightRentedDatesForEquipment(int equipmentId)
        {
            try
            {
                var approvedRequests = dbcontext.RentalRequests
                                                .Where(r => r.EquipmentId == equipmentId && r.RequestStatusId == 2) // todo: make it as enum
                                                .ToList();

                List<DateTime> rentedDates = new List<DateTime>();

                foreach (var request in approvedRequests)
                {
                    var transaction = dbcontext.RentalTransactions.Where(t => t.RequestId == request.RequestId).FirstOrDefault();
                    if (transaction != null)
                    {
                        rentedDates.AddRange(Enumerable.Range(0, (transaction.RentalReturnDate.Value - transaction.RentalStartDate.Value).Days + 1)
                                                           .Select(d => transaction.RentalStartDate.Value.AddDays(d)));
                    }
                    else
                    {
                        DateTime startDate = request.StartDate.Value;
                        DateTime returnDate = request.ReturnDate.Value;

                        rentedDates.AddRange(Enumerable.Range(0, (returnDate - startDate).Days + 1)
                                                           .Select(d => startDate.AddDays(d)));

                    }
                }

                if (rentedDates.Any())
                {
                    monthCalendar1.BoldedDates = rentedDates.Distinct().ToArray();
                    monthCalendar1.Update();
                }
                else
                {
                    //This is pointless
                    //MessageBox.Show("No rented dates found for the selected equipment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PaymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(EquipmentTextBox.Text, out int equipmentId))
                {
                    MessageBox.Show("Invalid equipment ID.");
                    return;
                }

                var equipment = dbcontext.Equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);

                if (!int.TryParse(RentalPeriodTextBox.Text, out int rentalPeriod))
                {
                    MessageBox.Show("Invalid rental period.");
                    return;
                }

                decimal rentalFee = rentalPrice * rentalPeriod;

                if (!(PaymentComboBox.SelectedValue is int paymentStatusId))
                {
                    MessageBox.Show("Please select a payment status.");
                    return;
                }

                decimal deposit = 0;
                decimal.TryParse(Deposit.Text, out deposit);

                var newTransaction = new RentalTransaction
                {
                    RequestId = _requestId,
                    RentalStartDate = DateTime.Parse(txtStartDate.Text),
                    RentalReturnDate = DateTime.Parse(txtReturnDate.Text),
                    RentalPeriod = rentalPeriod,
                    RentalFee = rentalFee,
                    Deposit = deposit,
                    EquipmentId = equipmentId,
                    PaymentStatusId = paymentStatusId,
                    AmountPaid = 0,
                    TotalFee = rentalFee
                };

                if (transaction != null)
                {
                    if (!validateTransaction(equipmentId, transaction.RequestId ?? 0))
                        return;

                    var existingTransaction = dbcontext.RentalTransactions
                        .FirstOrDefault(t => t.TransactionId == transaction.TransactionId);

                    if (existingTransaction != null)
                    {
                        existingTransaction.RentalStartDate = DateTime.Parse(txtStartDate.Text);
                        existingTransaction.RentalReturnDate = DateTime.Parse(txtReturnDate.Text);
                        existingTransaction.RentalPeriod = rentalPeriod;
                        existingTransaction.RentalFee = rentalFee;
                        existingTransaction.Deposit = deposit;
                        existingTransaction.EquipmentId = equipmentId;
                        existingTransaction.PaymentStatusId = paymentStatusId;

                        if (int.TryParse(RequestTextBox.Text, out int reqId))
                            existingTransaction.RequestId = reqId;

                        dbcontext.Entry(existingTransaction).State = EntityState.Modified;
                        MessageBox.Show("Transaction updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (!validateTransaction(equipmentId, _requestId))
                        return;

                    dbcontext.RentalTransactions.Add(newTransaction);
                    MessageBox.Show("Transaction added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dbcontext.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                dbcontext.Logs.Add(new Log
                {
                    Action = "Error",
                    Exception = ex.Message,
                    Timestamp = DateTime.Now,
                    Source = "Rental_Transaction",
                    UserId = _authService.CurrentUser?.Id ?? 0,
                    AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                });

                dbcontext.SaveChanges();
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validateTransaction(int equipmentId, int requestId)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtReturnDate.Text) ||
                    string.IsNullOrEmpty(RentalPeriodTextBox.Text) || string.IsNullOrEmpty(EquipmentTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (PaymentComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select valid values from the payment combobox.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime startDate = DateTime.Parse(txtStartDate.Text);
            DateTime returnDate = DateTime.Parse(txtReturnDate.Text);

            if (startDate > returnDate)
            {
                MessageBox.Show("Start date cannot be after end date");
                return false;
            }

            var approvedRequests = dbcontext.RentalRequests
                .Where(r => r.EquipmentId == equipmentId &&
                r.RequestId != requestId &&
                r.RequestStatusId == 2)
                .ToList();

            foreach (var request in approvedRequests)
            {
                var transaction = dbcontext.RentalTransactions
                    .FirstOrDefault(t => t.RequestId == request.RequestId);

                DateTime checkStartDate;
                DateTime checkEndDate;

                if (transaction != null)
                {
                    checkStartDate = transaction.RentalStartDate.Value;
                    checkEndDate = transaction.RentalReturnDate.Value;
                }
                else
                {
                    checkStartDate = request.StartDate.Value;
                    checkEndDate = request.ReturnDate.Value;
                }

                if (checkStartDate <= returnDate && checkEndDate >= startDate)
                {
                    MessageBox.Show("Equipment is already reserved during this period.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;

        }
        private void AutoUpdateRentalInfo()
        {
            if (DateTime.TryParse(txtStartDate.Text, out DateTime startDate) &&
                DateTime.TryParse(txtReturnDate.Text, out DateTime returnDate))
            {
                if (returnDate >= startDate)
                {
                    int days = (returnDate - startDate).Days;
                    RentalPeriodTextBox.Text = days.ToString();
                    txtTotalCost.Text = (rentalPrice * days).ToString("C");
                }
            }
        }

        private void txtReturnDate_TextChanged(object sender, EventArgs e)
        {
            AutoUpdateRentalInfo();
        }

        private void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            AutoUpdateRentalInfo();
        }
    }
}
