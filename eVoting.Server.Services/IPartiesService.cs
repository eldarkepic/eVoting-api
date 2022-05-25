using AutoMapper;
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
    public interface IPartiesService
    {
        Task<OperationResponse<PartyDetail>> CreateAsync(PartyDetail model);
        Task<OperationResponse<PartyDetail>> UpdateAsync(PartyDetail model);
        Task<OperationResponse<PartyDetail>> RemoveAsync(string id);
        CollectionResponse<PartyDetail> GetAllParties(int pageNumber = 1, int pageSize = 10);
        Task<OperationResponse<CandidateDetail>> AssignOrRemoveCandidateFromPartyAsync(PartyCandidateRequest request);
        Task<OperationResponse<PartyDetail>> GetSinglePartysync(string id);

    }

    public class PartiesService : IPartiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityOptions _identity;
        

        public PartiesService(IUnitOfWork unitOfWork,
                                IdentityOptions identity)
                                
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
            
        }

        public async Task<OperationResponse<PartyDetail>> CreateAsync(PartyDetail model)
        {
            var party = new Party
            {
                Name = model.Name,
                VotelistId = model.VotelistId
            };

            await _unitOfWork.Parties.CreateAsync(party);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            model.Id = party.Id;

            return new OperationResponse<PartyDetail>
            {
                IsSuccess = true,
                Message = "Party created successfully!",
                Data = model
            };
        }

        public CollectionResponse<PartyDetail> GetAllParties(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 5)
                pageSize = 5;

            if (pageSize > 50)
                pageSize = 50;

            var parties = _unitOfWork.Parties.GetAll();
            int partiesCount = parties.Count();

            var partiesInPage = parties
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => p.ToPartyDetail());

            int pagesCount = partiesCount / pageSize;
            if ((partiesCount % pageSize) != 0)
                pagesCount++;

            return new CollectionResponse<PartyDetail>
            {
                IsSuccess = true,
                Message = "Parties retrieved successfully!",
                Records = partiesInPage.ToArray(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = pagesCount
            };
        }


        public async Task<OperationResponse<PartyDetail>> RemoveAsync(string id)
        {
            var party = await _unitOfWork.Parties.GetByIdAsync(id);

            if (party == null)
                return new OperationResponse<PartyDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Party not found"
                };

            _unitOfWork.Parties.Remove(party);

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<PartyDetail>
            {
                IsSuccess = true,
                Message = "Party has been deleted successfully!",
                Data = party.ToPartyDetail(),
            };
        }

        public async Task<OperationResponse<PartyDetail>> UpdateAsync(PartyDetail model)
        {
            var party = await _unitOfWork.Parties.GetByIdAsync(model.Id);

            if (party == null)
                return new OperationResponse<PartyDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Party not found"
                };

            party.Name = model.Name;
            

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<PartyDetail>
            {
                IsSuccess = true,
                Message = "Party has been updated successfully!",
                Data = model
            };
        }

        public async Task<OperationResponse<CandidateDetail>> AssignOrRemoveCandidateFromPartyAsync(PartyCandidateRequest request)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                return new OperationResponse<CandidateDetail> { IsSuccess = false, Message = "Candidate cannot be found" };
            }
            var party = await _unitOfWork.Parties.GetByIdAsync(request.PartyId);
            if (party == null)
            {
                return new OperationResponse<CandidateDetail> { IsSuccess = false, Message = "Party cannot be found" };
            }


            // var partyCandidates = _unitOfWork.Parties.GetAllCandidatesInParty(request.PartyId);
            var partyCandidate = party.PartyCandidates.SingleOrDefault(pc => pc.CandidateId == request.CandidateId);

            string message = string.Empty;

            if (partyCandidate != null)
            {
                _unitOfWork.Parties.RemoveCandidateFromParty(partyCandidate);

                message = "Candidate has been removed from party";
            }
            else
            {
                await _unitOfWork.Parties.AddCandidateToPartyAsync(new PartyCandidate
                {
                    Party = party,
                    Candidate = candidate,
                });

                message = "Candidate has been added to party";
            }

            await _unitOfWork.CommitChangesAsync(_identity.UserId);
            return new OperationResponse<CandidateDetail>
            {
                IsSuccess = true,
                Message = message

            };

        }

        public async Task<OperationResponse<PartyDetail>> GetSinglePartysync(string id)
        {
            var party = await _unitOfWork.Parties.GetByIdAsync(id);
            if (party == null)
            {
                return new OperationResponse<PartyDetail> { IsSuccess = false, Message = "Party cannot be found" };
            }

            var candidates = party.PartyCandidates?.Select(pc => pc.Candidate.ToCandidateDetail()).ToArray();

            return new OperationResponse<PartyDetail>
            {
                Data = party.ToPartyDetail(candidates, true),
                Message = "Party has been retreived successfully",
                IsSuccess = true
            };
        }
    }

}
