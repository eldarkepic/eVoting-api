using eVoting.Server.Services;
using eVoting.SharedFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVoting.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesService _candidatesService;

        public CandidatesController(ICandidatesService candidatesService)
        {
            _candidatesService = candidatesService;
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<CandidateDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CandidateDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CandidateDetail model)
        {
            var result = await _candidatesService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(CollectionResponse<CandidateDetail>))]
        [Authorize(Roles = "User, Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _candidatesService.GetAllCandidates(pageNumber, pageSize);
            return Ok(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<CandidateDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CandidateDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CandidateDetail model)
        {
            var result = await _candidatesService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<CandidateDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CandidateDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _candidatesService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
