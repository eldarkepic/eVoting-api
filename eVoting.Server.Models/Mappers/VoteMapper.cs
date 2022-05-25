using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Models.Mappers
{
    public static class VoteMapper
    {
        public static VoteDetail ToVoteDetail(this Vote vote)
        {
            return new VoteDetail
            {
                Id = vote.Id,

                Name = vote.Name,
                PublishingDate = vote.PublishingDate,
                CandidateId = vote.CandidateId,
            };
        }
    }
}
