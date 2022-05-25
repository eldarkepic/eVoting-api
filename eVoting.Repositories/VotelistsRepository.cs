using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class VotelistsRepository : IVotelistRepository
    {
        private readonly MyContext _db;

        public VotelistsRepository(MyContext db)
        {
            _db = db;
        }

        public async Task AddPartyToVotelistAsync(VotelistParty votelistparty)
        {
            await _db.VotelistParties.AddAsync(votelistparty);
        }

        public async Task CreateAsync(Votelist votelist)
        {
            await _db.Votelists.AddAsync(votelist);
        }

        public IEnumerable<Votelist> GetAll()
        {
            return _db.Votelists;
        }

        public IEnumerable<Party> GetAllPartiesInVotelist(string id)
        {
            return _db.VotelistParties
                                    .Include(VotelistParty => VotelistParty.Party)
                                    .Where(p => p.VotelistId == id)
                                    .Select(pc => pc.Party);
        }

        public  async Task<Votelist> GetByIdAsync(string id)
        {
            return await _db.Votelists
               .Include(c => c.VotelistParties)
               .ThenInclude(c => c.Party)
               .SingleOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Votelist votelist)
        {
            _db.Votelists.Remove(votelist);
        }

        public void RemovePartyFromVotelist(VotelistParty votelistParty)
        {
            _db.VotelistParties.Remove(votelistParty);
        }
    }
}
