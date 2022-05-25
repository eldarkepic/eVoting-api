using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class Party : UserRecord
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public virtual Votelist Votelist { get; set; }
        public string VotelistId { get; set; }
        public virtual List<PartyCandidate> PartyCandidates { get; set; }
    }
}
