﻿using System;
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

            //await _authService.RegisterUserAsync("Mahdi", "Mahdi@gmail.com", "Xxxx@123", "User");
            //await _authService.RegisterUserAsync("Ahmed", "Ahmed@gmail.com", "Xxxx@123", "User");
            //await _authService.RegisterUserAsync("Rehan", "Rehan@gmail.com", "Xxxx@123", "User");
            //await _authService.RegisterUserAsync("Hassan", "Hassan@gmail.com", "Xxxx@123", "User");

            //await _authService.RegisterUserAsync("Admin2", "Admin2@gmail.com", "Xxxx@222", "Administrator");
            //await _authService.RegisterUserAsync("Admin3", "Admin3@gmail.com", "Xxxx@333", "Administrator");
            //await _authService.RegisterUserAsync("Manager", "Manager@gmail.com", "Xxxx@123", "Staff");
            //await _authService.RegisterUserAsync("Staff1", "Staff1@gmail.com", "Xxxx@123", "Staff");
            //await _authService.RegisterUserAsync("Staff2", "Staff2@gmail.com", "Xxxx@123", "Staff");
            //await _authService.RegisterUserAsync("Staff3", "Staff3@gmail.com", "Xxxx@123", "Staff");

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
                    // Check if user is Administrator or Staff
                    if (await _authService.IsInRoleAsync("Administrator") || await _authService.IsInRoleAsync("Staff") || await _authService.IsInRoleAsync("Manager"))
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
                            Exception = "Only Admins and Staff are allowed to access this application"
                        });
                        dbContext.SaveChanges();

                        MessageBox.Show("Only Admins and Staff are allowed to access this application!", "UnAuthorized", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {}

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await NotificationSender.SendNotificationAsync(
                userId: _authService.CurrentUser.Id,
                message: "Your payment was processed successfully.",
                type: "Success"
            );
        }
    }
}
