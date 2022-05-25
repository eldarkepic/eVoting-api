using eVoting.Server.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eVoting.Server.Models
{
    public class MyContext : IdentityDbContext<Voter>
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {

        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Votelist> Votelists { get; set; }
        public DbSet<VotelistParty> VotelistParties { get; set; }
        public DbSet<PartyCandidate> PartyCandidates { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Voter>()
                .HasMany(p => p.CreatedVotes)
                .WithOne(p => p.CreatedByVoter)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Voter>()
                .HasMany(p => p.ModifiedVotes)
                .WithOne(p => p.ModifiedByVoter)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Voter>()
               .HasMany(p => p.CreatedVotelists)
               .WithOne(p => p.CreatedByVoter)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Voter>()
                .HasMany(p => p.ModifiedVotelists)
                .WithOne(p => p.ModifiedByVoter)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Voter>()
             .HasMany(p => p.CreatedCandidates)
             .WithOne(p => p.CreatedByVoter)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Voter>()
                .HasMany(p => p.ModifiedCandidates)
                .WithOne(p => p.ModifiedByVoter)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ApplicationUser>().ToTable("MyUsers");

            base.OnModelCreating(builder);
        }




        private string _userId = null;

        public async Task SaveChangesAsync(string userId)
        {
            _userId = userId;
            await SaveChangesAsync();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is UserRecord)
                {
                    var userRecord = (UserRecord)item.Entity;

                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            userRecord.ModificationDate = DateTime.UtcNow;
                            userRecord.ModifiedByVoterId = _userId;
                            break;
                        case EntityState.Added:
                            userRecord.ModificationDate = DateTime.UtcNow;
                            userRecord.ModifiedByVoterId = _userId;
                            userRecord.CreatationDate = DateTime.UtcNow;
                            userRecord.CreatedByVoterId = _userId;
                            break;
                        default:
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
