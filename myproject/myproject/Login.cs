using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myproject.configs;
using myproject.services;
using myproject_Library.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace myproject
{
    public partial class Login : Form
    {
        private EquipmentDBContext dbContext;
        private readonly AuthService _authService;
        public Login()
        {
            InitializeComponent();

            dbContext = new EquipmentDBContext();

            // Get the auth service from the service provider
            _authService = ServiceConfigurator.GetService<AuthService>();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable controls during login attempt
                btnConfirm.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // Validate input
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Attempt login
                bool loginSuccess = await _authService.LoginAsync(username, password);

                if (loginSuccess)
                {
                    // Check role and navigate to appropriate form
                    if (await _authService.IsInRoleAsync("Administrator"))
                    {
                        dbContext.Logs.Add(new Log
                        {
                            Action = "Successful Login",
                            Timestamp = DateTime.Now,
                            Source = "User",
                            UserId = _authService.CurrentUser.Id
                        });

                        dbContext.SaveChanges();

                        Form form1 = new Form1();

                        this.Hide();
                        form1.Show();
                    }
                    else
                    {
                        dbContext.Logs.Add(new Log
                        {
                            Action = "UnAuthorized",
                            Timestamp = DateTime.Now,
                            Source = "User",
                            Exception = "Only Admins are allowed to access this application"
                        });
                        dbContext.SaveChanges();

                        MessageBox.Show("Only Admins are allowed to access this application!", "UnAuthorized", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    dbContext.Logs.Add(new Log
                    {
                        Action = "UnSuccessful Login",
                        Timestamp = DateTime.Now,
                        Source = "User",
                        Exception = "Invalid username or password."
                    });
                    dbContext.SaveChanges();

                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                dbContext.Logs.Add(new Log
                {
                    Action = "Login Error",
                    Timestamp = DateTime.Now,
                    Source = "User",
                    Exception = ex.Message
                });
                dbContext.SaveChanges();

                Exception currentEx = ex;
                string fullErrorMessage = ex.Message;

                while (currentEx.InnerException != null)
                {
                    currentEx = currentEx.InnerException;
                    fullErrorMessage += "\r\n" + currentEx.Message;
                }
                MessageBox.Show($"Login error: {fullErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable controls after login attempt
                btnConfirm.Enabled = true;
                btnConfirm.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }
    }
}
