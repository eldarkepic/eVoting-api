using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.SharedFiles
{
    public class VotelistPartyRequest
    {
        [Required]
        public string VotelistId { get; set; }
        [Required]
        public string PartyId { get; set; }
    }
}
