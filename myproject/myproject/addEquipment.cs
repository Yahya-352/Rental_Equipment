using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myproject.configs;
using myproject.services;
using myproject_Library.Model;

namespace myproject
{
    public partial class addEquipment : Form
    {
        EquipmentDBContext dbContext;
        Equipment equipment1;
        AuthService _authService;

        public addEquipment()
        {
            InitializeComponent();
            dbContext = new EquipmentDBContext();
            _authService = ServiceConfigurator.GetService<AuthService>();

        }

        public addEquipment(Equipment _equipment)
        {
            InitializeComponent();
            dbContext = new EquipmentDBContext();
            equipment1 = _equipment;

        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (name_textbox.Text == "" || description_textbox.Text == "") {
                    MessageBox.Show("Please enter Equipment name and Description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!decimal.TryParse(fee_textbox.Text, out decimal rentalPrice))
                {
                    MessageBox.Show("Please enter a valid rental price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(textBox8.Text, out decimal penaltyPercentage))
                {
                    MessageBox.Show("Please enter a valid penalty percentage.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (availabilityComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid availability status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (categoryComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (conditionComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid condition.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (equipment1 != null)
                {
                    equipment1 = dbContext.Equipment.Find(equipment1.EquipmentId);

                    if (equipment1 != null)
                    {
                        equipment1.EquipmentName = name_textbox.Text;
                        equipment1.Description = description_textbox.Text;
                        equipment1.RentalPrice = rentalPrice;
                        equipment1.LatePenaltyPercentage = penaltyPercentage;
                        equipment1.AvailabilityStatusId = Convert.ToInt32(availabilityComboBox.SelectedValue);
                        equipment1.CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue);
                        equipment1.ConditionId = Convert.ToInt32(conditionComboBox.SelectedValue);

                        dbContext.Equipment.Update(equipment1);
                        MessageBox.Show("Updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Equipment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    Equipment equipment = new Equipment
                    {
                        EquipmentName = name_textbox.Text,
                        Description = description_textbox.Text,
                        RentalPrice = rentalPrice,
                        LatePenaltyPercentage = penaltyPercentage,
                        AvailabilityStatusId = Convert.ToInt32(availabilityComboBox.SelectedValue),
                        CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue),
                        ConditionId = Convert.ToInt32(conditionComboBox.SelectedValue)
                    };

                    dbContext.Equipment.Add(equipment);
                    MessageBox.Show("Added successfully");
                }

                dbContext.SaveChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                dbContext.Logs.Add(new Log
                {
                    Action = "Error",
                    Exception = ex.Message,
                    Timestamp = DateTime.Now,
                    Source = "Equipment",
                    UserId = _authService.CurrentUser.Id,
                    AffectedData = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 50))
                });

                dbContext.SaveChanges();
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void addEquipment_Load(object sender, EventArgs e)
        {
            try
            {
                categoryComboBox.DataSource = dbContext.Categories.ToList();
                categoryComboBox.DisplayMember = "CategoryName";
                categoryComboBox.ValueMember = "CategoryId";
                categoryComboBox.SelectedItem = null;

            }
            catch
            {
                throw;
            }
            try
            {
                conditionComboBox.DataSource = dbContext.EquipmentConditions.ToList();
                conditionComboBox.DisplayMember = "ConditionName";
                conditionComboBox.ValueMember = "ConditionId";
                conditionComboBox.SelectedItem = null;

            }
            catch
            {
                throw;
            }

            try
            {
                availabilityComboBox.DataSource = dbContext.EquipmentAvailabilities.ToList();
                availabilityComboBox.DisplayMember = "AvailabilityStatusName";
                availabilityComboBox.ValueMember = "AvailabilityStatusId";
                availabilityComboBox.SelectedItem = null;
            }
            catch
            {
                throw;
            }

            if (equipment1 != null)
            {
                equipment1 = dbContext.Equipment.Find(equipment1.EquipmentId); 
            }

            if (equipment1 != null)
            {
                name_textbox.Text = equipment1.EquipmentName;
                description_textbox.Text = equipment1.Description;
                fee_textbox.Text = equipment1.RentalPrice.ToString();
                textBox8.Text = equipment1.LatePenaltyPercentage.ToString();
                categoryComboBox.SelectedValue = equipment1.CategoryId;
                conditionComboBox.SelectedValue = equipment1.ConditionId;
                availabilityComboBox.SelectedValue = equipment1.AvailabilityStatusId;
            }
        }
    }
}
