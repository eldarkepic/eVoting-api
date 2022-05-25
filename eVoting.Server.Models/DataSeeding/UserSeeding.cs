using eVoting.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Server.Models.DataSeeding
{
    public class UserSeeding
    {
        private readonly UserManager<Voter> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSeeding(UserManager<Voter> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            if (await _roleManager.FindByNameAsync("Admin") != null)
                return;

            // Create role 
            var adminRole = new IdentityRole { Name = "Admin" };
            await _roleManager.CreateAsync(adminRole);

            var userRole = new IdentityRole { Name = "User" };
            await _roleManager.CreateAsync(userRole);

            // Create user 
            var admin = new Voter
            {
                Email = "test@mail.com",
                UserName = "test",
                FirstName = "TestName",
                LastName = "TestSurname",
                IdCard = "A1B2C3",
                JMBG = 12345
            };
            await _userManager.CreateAsync(admin, "Test.123");

            await _userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
