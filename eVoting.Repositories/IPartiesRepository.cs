using eVoting.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface IPartiesRepository
    {
        Task CreateAsync(Party party);
        void Remove(Party party);
        IEnumerable<Party> GetAll();
        IEnumerable<Candidate> GetAllCandidatesInParty(string id);
        void RemoveCandidateFromParty(PartyCandidate partycandiate);
        Task AddCandidateToPartyAsync(PartyCandidate partycandidate);
        Task<Party> GetByIdAsync(string id);
    }
}
