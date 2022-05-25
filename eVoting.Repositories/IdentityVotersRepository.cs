using eVoting.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class IdentityVotersRepository : IVotersRepository
    {
        private readonly UserManager<Voter> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityVotersRepository(UserManager<Voter> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task CreateVoterAsync(Voter voter, string password, string role)
        {
            await _userManager.CreateAsync(voter, password);
            await _userManager.AddToRoleAsync(voter, role);
        }

        public async Task<Voter> GetVoterByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Voter> GetVoterByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(Voter voter, string password)
        {
            return await _userManager.CheckPasswordAsync(voter, password);
        }


        public async Task<string> GetVoterRoleAsync(Voter voter)
        {
            var roles = await _userManager.GetRolesAsync(voter);
            return roles.FirstOrDefault(); 
        }
    }
}
