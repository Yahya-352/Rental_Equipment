using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using myproject_Library.Model;
using myproject_Library;

namespace myproject
{
    public partial class Admin_ManageUsers : Form
    {
        private EquipmentDBContext _dbContext;
        private User _selectedUser;
        private int _currentAdminId;
        private readonly Dictionary<string, int> _rolesDictionary = new Dictionary<string, int>();

        private static readonly byte[] _key = Encoding.UTF8.GetBytes("A1B2C3D4E5F6G7H8");
        private static readonly byte[] _iv = Encoding.UTF8.GetBytes("1H2G3F4E5D6C7B8A");

        public Admin_ManageUsers()
        {
            InitializeComponent();
            _dbContext = new EquipmentDBContext();

            var currentUserId = CurrentUserService.GetCurrentUserId();
            if (currentUserId == null)
            {
                MessageBox.Show("Unable to get current user ID.", "Error");
                Close();
                return;
            }
            _currentAdminId = (int)currentUserId;
            PopulateRolesComboBox();
            DisableFields();
        }

        private void PopulateRolesComboBox()
        {
            try
            {
                var roles = _dbContext.Roles.ToList();
                _rolesDictionary.Clear();
                CB_Role.Items.Clear();
                foreach (var role in roles)
                {
                    CB_Role.Items.Add(role.Name);
                    _rolesDictionary[role.Name] = role.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Admin_ManageUsers_Load(object sender, EventArgs e)
        {
            LoadUsers();
            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "User ID";
                dataGridView1.Columns["UserName"].HeaderText = "Username";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["RoleName"].HeaderText = "Role";
                dataGridView1.Columns["EncryptedPassword"].HeaderText = "Password";
            }
        }

        private void LoadUsers(string searchTerm = "")
        {
            try
            {
                var query = from user in _dbContext.Users
                            join ur in _dbContext.Set<IdentityUserRole<int>>() on user.Id equals ur.UserId into userRoles
                            from ur in userRoles.DefaultIfEmpty()
                            join r in _dbContext.Roles on ur.RoleId equals r.Id into roles
                            from role in roles.DefaultIfEmpty()
                            select new
                            {
                                user.Id,
                                user.UserName,
                                user.Email,
                                EncryptedPassword = Encrypt(user.PasswordHash ?? ""),
                                RoleId = role != null ? role.Id : 0,
                                RoleName = role != null ? role.Name : "No Role"
                            };

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(u => u.UserName.Contains(searchTerm));
                }

                dataGridView1.DataSource = query.ToList();
                SetColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchGrid_TextChanged(object sender, EventArgs e)
        {
            LoadUsers(SearchGrid.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EnableFields();
                var row = dataGridView1.Rows[e.RowIndex];
                int userId = Convert.ToInt32(row.Cells["Id"].Value);
                _selectedUser = _dbContext.Users.Find(userId);

                if (_selectedUser != null)
                {
                    TB_Username.Text = _selectedUser.UserName;
                    TB_Email.Text = _selectedUser.Email;
                    TB_Password.Text = string.Empty;
                    CB_Role.SelectedItem = row.Cells["RoleName"].Value.ToString();
                }
            }
        }

        private void B_Update_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Please select a user to update.", "Information");
                return;
            }

            if (_selectedUser.Id == _currentAdminId)
            {
                MessageBox.Show("You cannot update your own account while in a session.", "Access Denied");
                return;
            }

            try
            {
                string newUsername = TB_Username.Text.Trim();
                string newEmail = TB_Email.Text.Trim();
                string newPassword = TB_Password.Text.Trim();
                string newRole = CB_Role.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newEmail))
                {
                    MessageBox.Show("Username and Email are required fields.", "Validation Error");
                    return;
                }

                _selectedUser.UserName = newUsername;
                _selectedUser.Email = newEmail;

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        if (!ValidatePassword(newPassword))
                        {
                            MessageBox.Show("Password must have at least 8 characters, one uppercase, one lowercase, one special character, and end with a number.", "Validation Error");
                            return;
                        }

                        var hasher = new PasswordHasher<User>();
                        _selectedUser.PasswordHash = hasher.HashPassword(_selectedUser, newPassword);
                    }

                    if (!string.IsNullOrEmpty(newRole) && _rolesDictionary.ContainsKey(newRole))
                    {
                        int roleId = _rolesDictionary[newRole];
                        var currentRole = _dbContext.Set<IdentityUserRole<int>>().FirstOrDefault(ur => ur.UserId == _selectedUser.Id);

                        if (currentRole != null)
                        {
                            _dbContext.Set<IdentityUserRole<int>>().Remove(currentRole);
                            _dbContext.SaveChanges();
                        }

                        _dbContext.Set<IdentityUserRole<int>>().Add(new IdentityUserRole<int>
                        {
                            UserId = _selectedUser.Id,
                            RoleId = roleId
                        });

                        _selectedUser.RoleId = roleId;
                    }

                    _dbContext.Users.Update(_selectedUser);
                    _dbContext.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show("User updated successfully.", "Success");
                    LoadUsers();
                    ClearFields();
                    DisableFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error");
            }
        }

        private void EnableFields()
        {
            TB_Username.Enabled = true;
            TB_Email.Enabled = true;
            TB_Password.Enabled = true;
            CB_Role.Enabled = true;
            B_Update.Enabled = true;
        }

        private void DisableFields()
        {
            TB_Username.Enabled = false;
            TB_Email.Enabled = false;
            TB_Password.Enabled = false;
            CB_Role.Enabled = false;
            _selectedUser = null;
        }

        private void ClearFields()
        {
            TB_Username.Text = "User Name";
            TB_Email.Text = "Email";
            TB_Password.Text = "Password";
            CB_Role.SelectedIndex = -1;
            CB_Role.Text = "Select Role";
        }

        private static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new System.IO.MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                    sw.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static bool ValidatePassword(string password)
        {
            return password.Length >= 8
                && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[a-z]")
                && Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]")
                && Regex.IsMatch(password, @"\d$"); // Correctly checks if it ends with a digit
        }

        // Method for deleting a user and their related data
        private void B_DeleteUser_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "Information");
                return;
            }

            // Check if the current admin is trying to delete their own account
            if (_selectedUser.Id == _currentAdminId)
            {
                MessageBox.Show("You cannot delete your own account while in session.", "Access Denied");
                return;
            }

            // Get related data for confirmation
            var relatedData = GetRelatedDataForUser(_selectedUser.Id);

            // Create a confirmation message
            StringBuilder confirmationMessage = new StringBuilder();
            confirmationMessage.AppendLine("Are you sure you want to delete this user and all related data?");
            foreach (var data in relatedData)
            {
                confirmationMessage.AppendLine($"{data.TableName}: {data.RowCount} row(s) will be deleted.");
            }

            var dialogResult = MessageBox.Show(confirmationMessage.ToString(), "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // Begin transaction to ensure atomic deletion
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        // Delete the related data first
                        foreach (var data in relatedData)
                        {
                            DeleteRelatedData(data.TableName, _selectedUser.Id);
                        }

                        // Finally, delete the user
                        _dbContext.Users.Remove(_selectedUser);
                        _dbContext.SaveChanges();
                        transaction.Commit();

                        MessageBox.Show("User and related data deleted successfully.", "Success");

                        // Refresh the users list and clear the fields
                        LoadUsers();
                        ClearFields();
                        DisableFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error");
                }
            }
        }

        // Method to get the related data count for the user
        private List<RelatedDataInfo> GetRelatedDataForUser(int userId)
        {
            var relatedData = new List<RelatedDataInfo>();

            // Check RentalRequests
            int rentalRequestCount = _dbContext.RentalRequests.Count(r => r.UserId == userId);
            if (rentalRequestCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "RentalRequests", RowCount = rentalRequestCount });
            }

            // Check Feedbacks
            int feedbackCount = _dbContext.Feedbacks.Count(f => f.UserId == userId);
            if (feedbackCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "Feedbacks", RowCount = feedbackCount });
            }

            // Check Logs
            int logCount = _dbContext.Logs.Count(l => l.UserId == userId);
            if (logCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "Logs", RowCount = logCount });
            }

            // Check Notifications
            int notificationCount = _dbContext.Notifications.Count(n => n.UserId == userId);
            if (notificationCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "Notifications", RowCount = notificationCount });
            }

            // Check Documents
            int documentCount = _dbContext.Documents.Count(d => d.UserId == userId);
            if (documentCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "Documents", RowCount = documentCount });
            }

            // Check AspNetUserTokens
            int tokenCount = _dbContext.Set<IdentityUserToken<int>>().Count(ut => ut.UserId == userId);
            if (tokenCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "AspNetUserTokens", RowCount = tokenCount });
            }

            // Check AspNetUserLogins
            int loginCount = _dbContext.Set<IdentityUserLogin<int>>().Count(ul => ul.UserId == userId);
            if (loginCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "AspNetUserLogins", RowCount = loginCount });
            }

            // Check AspNetUserClaims
            int claimCount = _dbContext.Set<IdentityUserClaim<int>>().Count(uc => uc.UserId == userId);
            if (claimCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "AspNetUserClaims", RowCount = claimCount });
            }

            // Check AspNetRoleClaims
            int roleClaimCount = _dbContext.Set<IdentityRoleClaim<int>>().Count(rc => rc.RoleId == userId);
            if (roleClaimCount > 0)
            {
                relatedData.Add(new RelatedDataInfo { TableName = "AspNetRoleClaims", RowCount = roleClaimCount });
            }

            return relatedData;
        }

        // Method to delete data from related tables
        private void DeleteRelatedData(string tableName, int userId)
        {
            switch (tableName)
            {
                case "RentalRequests":
                    var rentalRequests = _dbContext.RentalRequests.Where(r => r.UserId == userId).ToList();
                    _dbContext.RentalRequests.RemoveRange(rentalRequests);
                    break;

                case "Feedbacks":
                    var feedbacks = _dbContext.Feedbacks.Where(f => f.UserId == userId).ToList();
                    _dbContext.Feedbacks.RemoveRange(feedbacks);
                    break;

                case "Logs":
                    var logs = _dbContext.Logs.Where(l => l.UserId == userId).ToList();
                    _dbContext.Logs.RemoveRange(logs);
                    break;

                case "Notifications":
                    var notifications = _dbContext.Notifications.Where(n => n.UserId == userId).ToList();
                    _dbContext.Notifications.RemoveRange(notifications);
                    break;

                case "Documents":
                    var documents = _dbContext.Documents.Where(d => d.UserId == userId).ToList();
                    _dbContext.Documents.RemoveRange(documents);
                    break;

                case "AspNetUserTokens":
                    var userTokens = _dbContext.Set<IdentityUserToken<int>>().Where(ut => ut.UserId == userId).ToList();
                    _dbContext.Set<IdentityUserToken<int>>().RemoveRange(userTokens);
                    break;

                case "AspNetUserLogins":
                    var userLogins = _dbContext.Set<IdentityUserLogin<int>>().Where(ul => ul.UserId == userId).ToList();
                    _dbContext.Set<IdentityUserLogin<int>>().RemoveRange(userLogins);
                    break;

                case "AspNetUserClaims":
                    var userClaims = _dbContext.Set<IdentityUserClaim<int>>().Where(uc => uc.UserId == userId).ToList();
                    _dbContext.Set<IdentityUserClaim<int>>().RemoveRange(userClaims);
                    break;

                case "AspNetRoleClaims":
                    var roleClaims = _dbContext.Set<IdentityRoleClaim<int>>().Where(rc => rc.RoleId == userId).ToList();
                    _dbContext.Set<IdentityRoleClaim<int>>().RemoveRange(roleClaims);
                    break;

                default:
                    break;
            }

            _dbContext.SaveChanges(); // Ensure the changes are committed for each related table
        }

        // RelatedDataInfo class to store table names and row counts
        public class RelatedDataInfo
        {
            public string TableName { get; set; }
            public int RowCount { get; set; }
        }

    }
}
