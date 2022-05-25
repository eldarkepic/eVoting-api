using eVoting.Repositories;
using eVoting.Server.Infrastructure;
using eVoting.Server.Models.Mappers;
using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Server.Services
{
    public interface IVotesService
    {
        Task<OperationResponse<VoteDetail>> CreateAsync(VoteDetail model);
        Task<OperationResponse<VoteDetail>> UpdateAsync(VoteDetail model);
        Task<OperationResponse<VoteDetail>> RemoveAsync(string voteId);
        IEnumerable<VoteDetail> GetCandidateVotes(string candidateId);
    }

    public class VotesService : IVotesService
    {
        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;

        public VotesService(IUnitOfWork unitOfWork, IdentityOptions identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }


        public async Task<OperationResponse<VoteDetail>> CreateAsync(VoteDetail model)
        {
            if (string.IsNullOrWhiteSpace(model.CandidateId))
                return new OperationResponse<VoteDetail> { IsSuccess = false, Message = "Candidate is required" };

            var candidate = await _unitOfWork.Candidates.GetByIdAsync(model.CandidateId);
            if (candidate == null)
            {
                return new OperationResponse<VoteDetail>
                {
                    IsSuccess = false,
                    Message = "Candidate cannot be found"
                };
            }


            var vote = new Vote
            {
                Name = model.Name,
                PublishingDate = model.PublishingDate,
                Candidate = candidate
            };

            await _unitOfWork.Votes.CreateAsync(vote);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            model.Id = vote.Id;

            return new OperationResponse<VoteDetail>
            {
                IsSuccess = true,
                Message = "Vote has been created successfully",
                Data = model
            };
        }

        public CollectionResponse<VoteDetail> GetAllVotes(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VoteDetail> GetCandidateVotes(string candidateId)
        {
            var votes = _unitOfWork.Votes.GetAllForCandidate(candidateId).ToArray();

            return votes.Select(c => c.ToVoteDetail());
        }

        public async Task<OperationResponse<VoteDetail>> RemoveAsync(string id)
        {
            var vote = await _unitOfWork.Votes.GetByIdAsync(id);
            if (vote == null)
                return new OperationResponse<VoteDetail>
                {
                    IsSuccess = false,
                    Message = "Vote not found"
                };

            _unitOfWork.Votes.Remove(vote);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<VoteDetail>
            {
                Data = vote.ToVoteDetail(),
                IsSuccess = true,
                Message = "Vote deleted successfully"
            };
        }

        public Task<OperationResponse<VoteDetail>> UpdateAsync(VoteDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
