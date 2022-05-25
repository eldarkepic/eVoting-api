using eVoting.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface IVotersRepository
    {
        Task<Voter> GetVoterByIdAsync(string id);

        Task<Voter> GetVoterByEmailAsync(string email);

        Task CreateVoterAsync(Voter voter, string password, string role);

        Task<bool> CheckPasswordAsync(Voter voter, string password);

        Task<string> GetVoterRoleAsync(Voter voter);
    }
}
