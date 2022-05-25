using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class VotelistParty : Record
    {
        public virtual Party Party { get; set; }
        public string PartyId { get; set; }

        public virtual Votelist Votelist { get; set; }
        public string VotelistId { get; set; }

    }
    }
