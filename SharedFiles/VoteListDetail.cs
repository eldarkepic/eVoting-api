using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.SharedFiles
{
    public class VoteListDetail
    {
        public string Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<PartyDetail> Parties { get; set; }
    }
}
