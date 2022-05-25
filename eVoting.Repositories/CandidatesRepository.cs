using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private readonly MyContext _db;

        public CandidatesRepository(MyContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Candidate candidate)
        {
            await _db.Candidates.AddAsync(candidate);
        }

        public IEnumerable<Candidate> GetAll()
        {
            return _db.Candidates.Include(p => p.Party);
        }

        public async Task<Candidate> GetByIdAsync(string id)
        {
            return await _db.Candidates.FindAsync(id);
        }

        public void Remove(Candidate candidate)
        {
            _db.Candidates.Remove(candidate);
        }
    }
}
