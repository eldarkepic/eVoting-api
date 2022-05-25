using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eVoting.SharedFiles
{
    public class VoteDetail
    {
        public string Id { get; set; }

        [StringLength(5000)]
        public string Name { get; set; }
        public DateTime PublishingDate { get; set; }
        public string CandidateId { get; set; }
        //public string VoterId { get; set; }
    }
}
