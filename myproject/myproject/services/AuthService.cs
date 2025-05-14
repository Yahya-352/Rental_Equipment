using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myproject_Library.Model;
using myproject_Library;
using Microsoft.EntityFrameworkCore;

namespace myproject.services
{
    internal class AuthService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly EquipmentDBContext _dbContext;

        // Keep track of current user
        public User CurrentUser { get; private set; }

        public AuthService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<User>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            _dbContext = new EquipmentDBContext();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // First, find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }

            // Check the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                _dbContext.Roles.Where((r) => r.Id == user.RoleId).Load();
                CurrentUserService.SetCurrentUser(user.Id, user.UserName);
                CurrentUser = user;
                return true;
            }

            return false;
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(
            string username,
            string email,
            string password,
            string roleName = "User")
        {
            var user = new User
            {
                UserName = username,
                Email = email
            };

            // Create the user with password
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return (false, "User was created but could not be retrieved from database");
            }

            // Check if the role exists, if not create it
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new Role { Name = roleName });
                if (!roleResult.Succeeded)
                {
                    return (false, $"Failed to create role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }

            var role = _dbContext.Roles
            .Where(r => r.NormalizedName == roleName.ToUpper())
            .FirstOrDefault();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ({user.Id}, {role.Id})";

                if (command.Connection.State != System.Data.ConnectionState.Open)
                    await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
                return (true, "User created and role assigned successfully");
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            CurrentUserService.ClearCurrentUser();
        }

        public async Task<bool> IsInRoleAsync(string roleName)
        {
            if (CurrentUser == null)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(CurrentUser, roleName);
        }

    }
}
