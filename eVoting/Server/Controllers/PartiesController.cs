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
    
    public class PartiesController : ControllerBase
    {
        private readonly IPartiesService _partiesService;

        public PartiesController(IPartiesService partiesService)
        {
            _partiesService = partiesService;
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<PartyDetail>))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _partiesService.GetSinglePartysync(id);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound();
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<PartyDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PartyDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PartyDetail model)
        {
            var result = await _partiesService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(CollectionResponse<PartyDetail>))]
        [Authorize(Roles = "User, Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _partiesService.GetAllParties(pageNumber, pageSize);
            return Ok(result);
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<PartyDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PartyDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PartyDetail model)
        {
            var result = await _partiesService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<PartyDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PartyDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _partiesService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<string>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<string>))]
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignOrRemoveCandidate")]
        public async Task<IActionResult> AssignOrRemoveCandidate([FromBody] PartyCandidateRequest model)
        {
            var result = await _partiesService.AssignOrRemoveCandidateFromPartyAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
