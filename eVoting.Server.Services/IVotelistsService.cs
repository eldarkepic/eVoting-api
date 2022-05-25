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
    public interface IVotelistsService
    {
        Task<OperationResponse<VoteListDetail>> CreateAsync(VoteListDetail model);
        Task<OperationResponse<VoteListDetail>> UpdateAsync(VoteListDetail model);
        Task<OperationResponse<VoteListDetail>> RemoveAsync(string id);
        CollectionResponse<VoteListDetail> GetAllVotelists(int pageNumber = 1, int pageSize = 10);
        Task<OperationResponse<PartyDetail>> AssignOrRemovePartyFromVotelistAsync(VotelistPartyRequest request);
        Task<OperationResponse<VoteListDetail>> GetSingleVotelistAsync(string id);

    }

    public class VotelistsService : IVotelistsService
    {
        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;

        public VotelistsService(IUnitOfWork unitOfWork, IdentityOptions identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public async Task<OperationResponse<PartyDetail>> AssignOrRemovePartyFromVotelistAsync(VotelistPartyRequest request)
        {
            var party = await _unitOfWork.Parties.GetByIdAsync(request.PartyId);
            if (party == null)
            {
                return new OperationResponse<PartyDetail> { IsSuccess = false, Message = "Party cannot be found" };
            }
            var votelist = await _unitOfWork.Votelists.GetByIdAsync(request.VotelistId);
            if (votelist == null)
            {
                return new OperationResponse<PartyDetail> { IsSuccess = false, Message = "Party cannot be found" };
            }


            // var partyCandidates = _unitOfWork.Parties.GetAllCandidatesInParty(request.PartyId);
            var votelistParty = votelist.VotelistParties.SingleOrDefault(pc => pc.PartyId == request.PartyId);

            string message = string.Empty;

            if (votelistParty != null)
            {
                _unitOfWork.Votelists.RemovePartyFromVotelist(votelistParty);

                message = "Party has been removed from votelist";
            }
            else
            {
                await _unitOfWork.Votelists.AddPartyToVotelistAsync(new VotelistParty
                {
                    Party = party,
                    Votelist = votelist,
                });

                message = "Party has been added to votelist";
            }

            await _unitOfWork.CommitChangesAsync(_identity.UserId);
            return new OperationResponse<PartyDetail>
            {
                IsSuccess = true,
                Message = message

            };
        }

        public async Task<OperationResponse<VoteListDetail>> CreateAsync(VoteListDetail model)
        {

            var votelist = new Votelist
            {
                Name = model.Name,
                Description = model.Description
            };

            await _unitOfWork.Votelists.CreateAsync(votelist);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

           model.Id = votelist.Id;

            return new OperationResponse<VoteListDetail>
            {
                IsSuccess = true,
                Message = "Votelist created successfully!",
                Data = model
            };
        }

        public CollectionResponse<VoteListDetail> GetAllVotelists(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 5)
                pageSize = 5;

            if (pageSize > 50)
                pageSize = 50;

            var votelists = _unitOfWork.Votelists.GetAll();
            int votelistsCount = votelists.Count();

            var votelistsInPage = votelists
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => p.ToVotelistDetail());

            int pagesCount = votelistsCount / pageSize;
            if ((votelistsCount % pageSize) != 0)
                pagesCount++;

            return new CollectionResponse<VoteListDetail>
            {
                IsSuccess = true,
                Message = "Votelists retrieved successfully!",
                Records = votelistsInPage.ToArray(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = pagesCount
            };
        }

        public async Task<OperationResponse<VoteListDetail>> GetSingleVotelistAsync(string id)
        {
            var votelist = await _unitOfWork.Votelists.GetByIdAsync(id);
            if (votelist == null)
            {
                return new OperationResponse<VoteListDetail> { IsSuccess = false, Message = "Votelist cannot be found" };
            }

            var parties = votelist.VotelistParties?.Select(pc => pc.Party.ToPartyDetail()).ToArray();

            return new OperationResponse<VoteListDetail>
            {
                Data = votelist.ToVotelistDetail(parties, true),
                Message = "Votelist has been retreived successfully",
                IsSuccess = true
            };
        }

        public async Task<OperationResponse<VoteListDetail>> RemoveAsync(string id)
        {
            var votelist = await _unitOfWork.Votelists.GetByIdAsync(id);

            if (votelist == null)
                return new OperationResponse<VoteListDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Votelist not found"
                };

            _unitOfWork.Votelists.Remove(votelist);

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<VoteListDetail>
            {
                IsSuccess = true,
                Message = "Votelist has been deleted successfully!",
                Data = votelist.ToVotelistDetail(),
            };
        }

        public async Task<OperationResponse<VoteListDetail>> UpdateAsync(VoteListDetail model)
        {
            var votelist = await _unitOfWork.Votelists.GetByIdAsync(model.Id);

            if (votelist == null)
                return new OperationResponse<VoteListDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Votelist not found"
                };

            votelist.Name = model.Name;
            votelist.Description = model.Description;

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<VoteListDetail>
            {
                IsSuccess = true,
                Message = "Votelist has been updated successfully!",
                Data = model
            };
        }
    }
}
