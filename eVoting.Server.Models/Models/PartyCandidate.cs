using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class PartyCandidate : Record
    {
        public virtual Candidate Candidate { get; set; }
        public string CandidateId { get; set; }

        public virtual Party Party { get; set; }
        public string PartyId { get; set; }
    }
}
