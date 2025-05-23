﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using myproject.configs;
using myproject.services;
using myproject_Library.Model;

namespace myproject
{
    public partial class RentalRequests : UserControl
    {
        EquipmentDBContext dbcontext;
        AuthService _authService;

        public RentalRequests()
        {
            InitializeComponent();
            dbcontext = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();
        }

        private void RentalRequests_Load(object sender, EventArgs e)
        {
            
            try
            {
                ddlstatusfilter.DataSource = dbcontext.RequestStatuses.ToList();
                ddlstatusfilter.DisplayMember = "RequestStatusName";
                ddlstatusfilter.ValueMember = "RequestStatusId";
                ddlstatusfilter.SelectedItem = null;

            }
            catch (Exception ex)
            {
                throw;
            }
            refreshOrdersGridView();
            
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            dataGridView1.Refresh();
        }
        private void refreshOrdersGridView()
        {
            dataGridView1.DataSource = null;
            var rentalRequests = dbcontext.RentalRequests.AsQueryable();

            if (ddlstatusfilter.SelectedItem != null)
            {
                var selectedStatus = ddlstatusfilter.SelectedItem as RequestStatus;
                if (selectedStatus != null)
                {
                    rentalRequests = rentalRequests.Where(x => x.RequestStatusId == selectedStatus.RequestStatusId);
                }
            }
            if (txtfilterno.Text != "")
            {
                rentalRequests = rentalRequests.Where(x => x.User.UserName == txtfilterno.Text);
            }

            dataGridView1.DataSource = rentalRequests.Select(e => new
            {
                ID = e.RequestId,
                Start_Date = e.StartDate,
                return_date = e.ReturnDate,
                Total_cost = e.TotalCost,
                userID = e.UserId,
                equipment = e.EquipmentId,
                RequestStatusName = e.RequestStatus.RequestStatusName,
                EquipmentName = e.Equipment.EquipmentName
            }
                            )
                    .ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshOrdersGridView();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Please select a rental request to approve.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int requestId = Convert.ToInt32(dataGridView1.CurrentCell.OwningRow.Cells["ID"].Value);

                var request = dbcontext.RentalRequests.Include(r => r.Equipment)
                                                      .FirstOrDefault(r => r.RequestId == requestId);
                if (request.RequestStatus == null || request.RequestStatus.RequestStatusId != 1)
                {
                    MessageBox.Show("Invalid or missing request status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime requestedStartDate = request.StartDate.Value;
                DateTime requestedReturnDate = request.ReturnDate.Value;
                int equipmentId = request.EquipmentId.Value;

                bool hasOverlappingTransaction = dbcontext.RentalTransactions
                .Any(t => t.EquipmentId == equipmentId &&
                          t.RequestId != requestId &&
                          t.RentalStartDate <= request.ReturnDate &&
                          t.RentalReturnDate >= request.StartDate);



                if (HasOverlappingApprovedRequest(equipmentId, requestId, requestedStartDate, requestedReturnDate))
                {
                    MessageBox.Show("Equipment is already requested (Approved Request dates overlap).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                request.RequestStatusId = 2;  // Assuming 2 is the "Approved" status ID
                dbcontext.SaveChanges();
                MessageBox.Show("Request approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshOrdersGridView(); // Refresh the grid after approval
            }
            catch (Exception ex) {
                dbcontext.Logs.Add(new Log
                {
                    Action = "Approve Request Error",
                    Exception = ex.Message,
                    Timestamp = DateTime.Now,
                    Source = "Rental_Requests",
                    UserId = _authService.CurrentUser.Id,
                    AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                });

                dbcontext.SaveChanges();
            }
        }
        private bool HasOverlappingApprovedRequest(int equipmentId, int requestId, DateTime start, DateTime end)
        {
            var approvedRequests = dbcontext.RentalRequests
                .Where(r => r.EquipmentId == equipmentId &&
                            r.RequestId != requestId &&
                            r.RequestStatusId == 2)
                .ToList();

            foreach (var request in approvedRequests)
            {
                var transaction = dbcontext.RentalTransactions
                    .FirstOrDefault(t => t.RequestId == request.RequestId);

                DateTime checkStart;
                DateTime checkEnd;

                if (transaction != null)
                {
                    checkStart = transaction.RentalStartDate.Value;
                    checkEnd = transaction.RentalReturnDate.Value;
                }
                else
                {
                    checkStart = request.StartDate.Value;
                    checkEnd = request.ReturnDate.Value;
                }

                if (checkStart <= end && checkEnd >= start)
                {
                    return true; // There's an overlap
                }
            }

            return false; // No conflicts
        }

        private void Reject_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Please select a rental request to Reject.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int requestId = Convert.ToInt32(dataGridView1.CurrentCell.OwningRow.Cells["ID"].Value);

                var request = dbcontext.RentalRequests.Include(r => r.Equipment)
                                                      .FirstOrDefault(r => r.RequestId == requestId);

                if (request.RequestStatus.RequestStatusId != 1)
                {
                    MessageBox.Show("Rental request already processed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (request == null)
                {
                    MessageBox.Show("Rental request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                request.RequestStatusId = 3;  // Assuming 3 is the "Rejeced" status ID
                dbcontext.SaveChanges();
                MessageBox.Show("Request Rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshOrdersGridView();
            } catch (Exception ex) {
                dbcontext.Logs.Add(new Log
                {
                    Action = "Reject Request Error",
                    Exception = ex.Message,
                    Timestamp = DateTime.Now,
                    Source = "Rental_Requests",
                    UserId = _authService.CurrentUser.Id,
                    AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                });

                dbcontext.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Please select a rental request to create a transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int requestId = Convert.ToInt32(dataGridView1.CurrentCell.OwningRow.Cells["ID"].Value);
            var request = dbcontext.RentalRequests.FirstOrDefault(r => r.RequestId == requestId);
            if (request.RequestStatusId != 2)
            {
                MessageBox.Show("Only approved requests can have a transaction.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool hasTransaction = dbcontext.RentalTransactions.Any(t => t.RequestId == requestId);
            if (hasTransaction)
            {
                MessageBox.Show("This request already has a transaction.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CreateTransactionPage transactionPage = new CreateTransactionPage(requestId);
            transactionPage.ShowDialog();
        }


    }
}
