using eVoting.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface IVotesRepository
    {
        Task CreateAsync(Vote vote);
        void Remove(Vote vote);
        IEnumerable<Vote> GetAllForCandidate(string id);
        Task<Vote> GetByIdAsync(string id);


    }
}
