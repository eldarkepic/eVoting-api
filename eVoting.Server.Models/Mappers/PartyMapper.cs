using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Models.Mappers
{
    public static class PartyMapper
    {

        public static PartyDetail ToPartyDetail(this Party party, IEnumerable<CandidateDetail> partyCandidates = null,
            bool includeCandidates = false)
        {
            return new PartyDetail
            {
                Id = party.Id,

                Name = party.Name,

                VotelistId = party.VotelistId,

                Candidates = includeCandidates ? partyCandidates : null,

                

            };
        }

    }
}
