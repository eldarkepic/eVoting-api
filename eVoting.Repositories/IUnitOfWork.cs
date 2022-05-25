using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public interface IUnitOfWork
    {
        IVotersRepository Voters { get; }
        IVotelistRepository Votelists { get; }
        IVotesRepository Votes { get; }
        IPartiesRepository Parties { get; }
        ICandidatesRepository Candidates { get; }
        Task CommitChangesAsync(string userId);


    }
}
