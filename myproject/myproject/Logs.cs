using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Internal;
using myproject_Library.Model;

namespace myproject
{
    public partial class Logs : Form
    {
        EquipmentDBContext dbContext;

        public Logs()
        {
            InitializeComponent();
            dbContext = new EquipmentDBContext();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                // Start with a base query
                IQueryable<Log> logsQuery = dbContext.Logs.AsQueryable();

                // Apply date filters if they have values
                if (dtpFrom.Checked)
                {
                    DateTime fromDate = dtpFrom.Value.Date; // Start of day
                    logsQuery = logsQuery.Where(log => log.Timestamp >= fromDate);
                }

                if (dtpTo.Checked)
                {
                    DateTime toDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1); // End of day
                    logsQuery = logsQuery.Where(log => log.Timestamp <= toDate);
                }

                // Order by timestamp descending
                logsQuery = logsQuery.OrderByDescending(log => log.Timestamp);

                // Select fields for the grid
                var query = logsQuery.Select(l => new
                {
                    LogId = l.LogId.ToString(),
                    Username = l.User != null ? l.User.Username : "-",
                    Source = l.Source != null ? l.Source.ToString() : "-",
                    Action = l.Action != null ? l.Action.ToString() : "-",
                    Date = l.Timestamp.HasValue ? l.Timestamp.Value : DateTime.Now,
                    Exception = l.Exception != null ? l.Exception.ToString() : "-",
                });

                // Execute the query and bind to DataGridView
                var logs = query.ToList();

                // Bind the data
                dgvLogs.DataSource = logs;

                // Format timestamp column
                dgvLogs.Columns["Date"].DefaultCellStyle.Format = "dd-MM-yyyy";

                // Auto-size columns
                dgvLogs.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Set specific column widths
                dgvLogs.Columns["LogId"].Width = 100;
                dgvLogs.Columns["Action"].Width = 120;
                dgvLogs.Columns["Exception"].Width = 200;
                dgvLogs.Columns["Date"].Width = 140;
                dgvLogs.Columns["Source"].Width = 120;
                dgvLogs.Columns["Username"].Width = 120;

                dgvLogs.ColumnHeadersDefaultCellStyle.Font = new Font(
                    dgvLogs.DefaultCellStyle.Font,
                    FontStyle.Bold
                );

                // Set row height for better readability
                dgvLogs.RowTemplate.Height = 25;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
