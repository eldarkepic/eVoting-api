using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class Candidate : UserRecord
    {

        [Required]
        [StringLength(5000)]
        public string FirstName { get; set; }

        public int LastName { get; set; }

        public virtual Party Party { get; set; }
        public string PartyId { get; set; }

        public int VoteNumber { get; set; }

        public virtual List<VotelistParty> UserVotes { get; set; }
        public virtual List<Vote> Votes { get; set; }
        public virtual List<PartyCandidate> PartyCandidates { get; set; }
    }
}
