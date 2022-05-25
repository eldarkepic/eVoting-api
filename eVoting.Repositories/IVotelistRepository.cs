using eVoting.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface IVotelistRepository
    {
        Task CreateAsync(Votelist votelist);
        void Remove(Votelist votelist);
        IEnumerable<Votelist> GetAll();
        IEnumerable<Party> GetAllPartiesInVotelist(string id);
        void RemovePartyFromVotelist(VotelistParty votelistParty);
        Task AddPartyToVotelistAsync(VotelistParty votelistparty);
        Task<Votelist> GetByIdAsync(string id);
    }
}
