using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace myproject
{
    public partial class Equipments : UserControl
    {
        EquipmentDBContext dbContext;
        public Equipments()
        {
            InitializeComponent();
            dbContext = new EquipmentDBContext();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Equipments_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.Refresh();
            try
            {
                ddlequipmentfilter.DataSource = dbContext.Categories.ToList();
                ddlequipmentfilter.DisplayMember = "CategoryName";
                ddlequipmentfilter.ValueMember = "CategoryId";
                ddlequipmentfilter.SelectedItem = null;

            }
            catch (Exception x)
            {
                throw;
            }
            refreshOrdersGridView();
        }
        private void refreshOrdersGridView()
        {
            dataGridView1.DataSource = null;
            var equipmenttoshow = dbContext.Equipment.AsQueryable();

            if (ddlequipmentfilter.SelectedItem != null)
            {
                var selectedCategory = ddlequipmentfilter.SelectedItem as Category;
                if (selectedCategory != null)
                {
                    equipmenttoshow = equipmenttoshow.Where(x => x.CategoryId == selectedCategory.CategoryId);
                }
            }

            dataGridView1.DataSource = equipmenttoshow.Select(e => new
            {
                ID = e.EquipmentId,
                Name = e.EquipmentName,
                Description = e.Description,
                Price = e.RentalPrice,
                Status = e.AvailabilityStatus.AvailabilityStatusName,
                Category = e.Category.CategoryName,
                condition = e.Condition.ConditionName,
                penalty = e.LatePenaltyPercentage
            }
                            )

                    .ToList();
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            refreshOrdersGridView();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            addEquipment addingequipment = new addEquipment();
            addingequipment.ShowDialog();
            refreshOrdersGridView();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            int firstcell = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value);
            Equipment equipment = dbContext.Equipment.Single(x => x.EquipmentId == firstcell);
            addEquipment form = new addEquipment(equipment);
            form.ShowDialog();
            refreshOrdersGridView();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int firstcell = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value);
            Equipment equipment = dbContext.Equipment.Single(x => x.EquipmentId == firstcell);
            dbContext.Remove(equipment);
            dbContext.SaveChanges();
            refreshOrdersGridView();
        }
    }
}
