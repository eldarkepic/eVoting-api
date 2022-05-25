using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Models.Mappers
{
    public static class VotelistsMapper
    {

        public static VoteListDetail ToVotelistDetail(this Votelist votelist, IEnumerable<PartyDetail> votelistsParties = null,
            bool includeparties = false)
        {
            return new VoteListDetail 
            {
                Id = votelist.Id,
                Description = votelist.Description,
                Name = votelist.Name,
                Parties = includeparties ? votelistsParties : null

            };
        }

    }
}
