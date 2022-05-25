using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.SharedFiles
{
    public class CandidateDetail
    {
        public string Id { get; set; }

        [Required]
        [StringLength(5000)]
        public string FirstName { get; set; }

        public int LastName { get; set; }

        public string PartyId { get; set; }

        public int VoteNumber { get; set; }

       // public virtual List<VotelistParty> UserVotes { get; set; }
        //public virtual List<Vote> Votes { get; set; }
       // public virtual List<PartyCandidate> PartyCandidates { get; set; }
    }
}
