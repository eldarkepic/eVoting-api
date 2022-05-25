using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public class Vote : UserRecord
    {
        [StringLength(5000)]
        public string Name { get; set; }
        public DateTime PublishingDate { get; set; }
        public virtual Candidate Candidate { get; set; }
        public string CandidateId { get; set; }
    }

    }
