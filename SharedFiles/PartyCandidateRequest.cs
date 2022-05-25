using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.SharedFiles
{
    public class PartyCandidateRequest
    {
        [Required]
        public string PartyId { get; set; }
        [Required]
        public string CandidateId { get; set; }
    }
}
