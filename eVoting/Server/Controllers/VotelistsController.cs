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
    
    public class VotelistsController : ControllerBase
    {
        private readonly IVotelistsService _votelistsService;

        public VotelistsController(IVotelistsService votelistsService)
        {
            _votelistsService = votelistsService;
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteListDetail>))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _votelistsService.GetSingleVotelistAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound();
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteListDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteListDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(VoteListDetail model)
        {
            var result = await _votelistsService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }



        [ProducesResponseType(200, Type = typeof(CollectionResponse<VoteListDetail>))]
        [Authorize(Roles = "User, Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _votelistsService.GetAllVotelists(pageNumber, pageSize);
            return Ok(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteListDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteListDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(VoteListDetail model)
        {
            var result = await _votelistsService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteListDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteListDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _votelistsService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<string>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<string>))]
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignOrRemoveParty")]
        public async Task<IActionResult> AssignOrRemoveParty([FromBody] VotelistPartyRequest model)
        {
            var result = await _votelistsService.AssignOrRemovePartyFromVotelistAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
