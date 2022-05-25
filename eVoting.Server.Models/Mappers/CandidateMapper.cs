using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Models.Mappers
{
    public static class CandidateMapper
    {
        public static CandidateDetail ToCandidateDetail(this Candidate candidate)
        {
            return new CandidateDetail
            {
                Id = candidate.Id,

                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                VoteNumber = 0,
                PartyId = candidate.PartyId

            };
        }
    }
}
