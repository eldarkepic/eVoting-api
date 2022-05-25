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
    public interface ICandidatesService
    {
        Task<OperationResponse<CandidateDetail>> CreateAsync(CandidateDetail model);
        Task<OperationResponse<CandidateDetail>> UpdateAsync(CandidateDetail model);
        Task<OperationResponse<CandidateDetail>> RemoveAsync(string id);
        CollectionResponse<CandidateDetail> GetAllCandidates(int pageNumber = 1, int pageSize = 10);
        
    }


    public class CandidatesService : ICandidatesService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityOptions _identity;
        //private readonly IMapper _mapper;

        public CandidatesService(IUnitOfWork unitOfWork,
                                IdentityOptions identity)
        //IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
            //_mapper = mapper;
        }

        

        public async Task<OperationResponse<CandidateDetail>> CreateAsync(CandidateDetail model)
        {
            var candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                VoteNumber = 0,
                PartyId = model.PartyId
            };

            await _unitOfWork.Candidates.CreateAsync(candidate);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            model.Id = candidate.Id;
            

            return new OperationResponse<CandidateDetail>
            {
                IsSuccess = true,
                Message = "Candidate has been created successfully!",
                Data = model
            };
        }

        public CollectionResponse<CandidateDetail> GetAllCandidates(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 5)
                pageSize = 5;

            if (pageSize > 50)
                pageSize = 50;

            var candidates = _unitOfWork.Candidates.GetAll();
            int candidatesCount = candidates.Count();

            var candidatesInPage = candidates
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => p.ToCandidateDetail());

            int pagesCount = candidatesCount / pageSize;
            if ((candidatesCount % pageSize) != 0)
                pagesCount++;

            return new CollectionResponse<CandidateDetail>
            {
                IsSuccess = true,
                Message = "Candidates retrieved successfully!",
                Records = candidatesInPage.ToArray(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = pagesCount
            };
        }

        public async Task<OperationResponse<CandidateDetail>> RemoveAsync(string id)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

            if (candidate == null)
                return new OperationResponse<CandidateDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Candidate not found"
                };

            //_unitOfWork.Candidates.RemoveVotes(candidate);
            // TODO: Remove the comments and the playsist assingments 
            _unitOfWork.Candidates.Remove(candidate);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<CandidateDetail>
            {
                IsSuccess = true,
                Message = "Candidate has been deleted successfully!",
                Data = candidate.ToCandidateDetail(),
            };
        }

        public async Task<OperationResponse<CandidateDetail>> UpdateAsync(CandidateDetail model)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(model.Id);

            if (candidate == null)
                return new OperationResponse<CandidateDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Candidate not found"
                };

            candidate.FirstName = model.FirstName;
            candidate.LastName = model.LastName;
            candidate.VoteNumber = 0;
            candidate.PartyId = model.PartyId;


            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<CandidateDetail>
            {
                IsSuccess = true,
                Message = "Candidate has been updated successfully!",
                Data = model
            };
        }
    }
}
