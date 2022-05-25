

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eVoting.Server.Models.Models
{
    public class Voter : IdentityUser
    {
        public Voter()
        {
            CreatedVotelists = new List<Votelist>();
            ModifiedVotelists = new List<Votelist>();
        }

        [Required]
        public int JMBG { get; set; }
        [Required]
        public string IdCard { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public int BirthDate { get; set; }
        public virtual List<Votelist> CreatedVotelists { get; set; }
        public virtual List<Votelist> ModifiedVotelists { get; set; }

        public virtual List<Vote> CreatedVotes { get; set; }
        public virtual List<Vote> ModifiedVotes { get; set; }

        public virtual List<Candidate> CreatedCandidates { get; set; }
        public virtual List<Candidate> ModifiedCandidates { get; set; }

    }
}
