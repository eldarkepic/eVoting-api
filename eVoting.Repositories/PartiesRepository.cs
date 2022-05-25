using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class PartiesRepository : IPartiesRepository
    {
        private readonly MyContext _db;

        public PartiesRepository(MyContext db)
        {
            _db = db;
        }

        public async Task AddCandidateToPartyAsync(PartyCandidate partycandidate)
        {
            await _db.PartyCandidates.AddAsync(partycandidate);
        }

        public async Task CreateAsync(Party party)
        {
            await _db.Parties.AddAsync(party);
        }

        public IEnumerable<Party> GetAll()
        {
            return _db.Parties;
        }

        public IEnumerable<Candidate> GetAllCandidatesInParty(string id)
        {
            return _db.PartyCandidates
                                    .Include(PartyCandidate => PartyCandidate.Candidate)
                                    .Where(p => p.PartyId == id)
                                    .Select(pc => pc.Candidate);
        }

        public async Task<Party> GetByIdAsync(string id)
        {
            return await _db.Parties
                .Include(c => c.PartyCandidates)
                .ThenInclude(c => c.Candidate)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Party party)
        {
            _db.Parties.Remove(party);
        }

        public void RemoveCandidateFromParty(PartyCandidate partycandiate)
        {
            _db.PartyCandidates.Remove(partycandiate);
        }
    }
}
