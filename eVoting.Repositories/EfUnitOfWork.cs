using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace eVoting.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly UserManager<Voter> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyContext _db;
        public EfUnitOfWork(UserManager<Voter> userManager,
                            RoleManager<IdentityRole> roleManager,
                            MyContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        private IVotersRepository _voters;

        public IVotersRepository Voters
        {
            get
            {
                if (_voters == null)
                    _voters = new IdentityVotersRepository(_userManager, _roleManager);
                return _voters;
            }
        }

        private IVotelistRepository _votelists;
        public IVotelistRepository Votelists
        {
            get
            {
                if (_votelists == null)
                    _votelists = new VotelistsRepository(_db);
                return _votelists;
            }
        }

        private IVotesRepository _votes;
        public IVotesRepository Votes
        {
            get
            {
                if (_votes == null)
                    _votes = new VotesRepository(_db);
                return _votes;
            }
        }


        private IPartiesRepository _parties;
        public IPartiesRepository Parties
        {
            get
            {
                if (_parties == null)
                    _parties = new PartiesRepository(_db);
                return _parties;
            }
        }


        private ICandidatesRepository _candidates;
        public ICandidatesRepository Candidates
        {
            get
            {
                if (_candidates == null)
                    _candidates = new CandidatesRepository(_db);
                return _candidates;
            }
        }

        public async Task CommitChangesAsync(string userId)
        {
            await _db.SaveChangesAsync(userId);
        }
    }
}
