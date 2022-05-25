using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eVoting.Server.Models.Models
{
    public abstract class UserRecord : Record
    {

        public UserRecord()
        {
            CreatationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }


        public DateTime CreatationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public virtual Voter CreatedByVoter { get; set; }
        // Foreign keys 
        [ForeignKey(nameof(CreatedByVoter))]
        public string CreatedByVoterId { get; set; }

        public virtual Voter ModifiedByVoter { get; set; }

        [ForeignKey(nameof(ModifiedByVoter))]
        public string ModifiedByVoterId { get; set; }
    }
}
