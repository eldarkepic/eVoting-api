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
    public class VotesController : ControllerBase
    {

        private readonly IVotesService _votesService;

        public VotesController(IVotesService votesService)
        {
            _votesService = votesService;
        }



        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteDetail>))]
        [Authorize(Roles = "User, Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(VoteDetail model)
        {
            var result = await _votesService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Edit(VoteDetail model)
        {
            var result = await _votesService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<VoteDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<VoteDetail>))]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _votesService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
