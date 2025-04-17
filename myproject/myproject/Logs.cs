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
    public partial class Logs : Form
    {
        EquipmentDBContext dBContext;

        public Logs()
        {
            InitializeComponent();
            dBContext = new EquipmentDBContext();  
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            dgvLogs.DataSource = dBContext.Logs.Select((l) => new { 
                LogId = l.LogId.ToString(),
                Username = l.User != null ? l.User.Username : "-",
                Source = l.Source != null ? l.Source.ToString() : "-",
                Action = l.Action != null ? l.Action.ToString() : "-",
                Date = l.Timestamp.HasValue ? l.Timestamp.Value.ToString("dd-MM-yyyy") : string.Empty,
            }).ToList();
        }
    }
}
