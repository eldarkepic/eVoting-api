using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly MyContext _db;

        public VotesRepository(MyContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Vote vote)
        {
            await _db.Votes.AddAsync(vote);
        }

        public IEnumerable<Vote> GetAll()
        {
            return _db.Votes;
        }

        public IEnumerable<Vote> GetAllForCandidate(string id)
        {
            return _db.Votes.Where(v => v.CandidateId == id);
        }

        public async Task<Vote> GetByIdAsync(string id)
        {
            return await _db.Votes.FindAsync(id);
        }

        public void Remove(Vote vote)
        {
            _db.Votes.Remove(vote);
        }
    }
}
