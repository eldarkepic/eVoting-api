using eVoting.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface ICandidatesRepository
    {
        Task CreateAsync(Candidate candidate);
        void Remove(Candidate candidate);
        IEnumerable<Candidate> GetAll();
        Task<Candidate> GetByIdAsync(string id);
    }
}
