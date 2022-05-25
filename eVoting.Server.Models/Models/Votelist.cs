using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class Votelist : UserRecord
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public virtual List<VotelistParty> VotelistParties { get; set; }
    }
}
