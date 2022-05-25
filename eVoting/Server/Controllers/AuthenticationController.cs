using eVoting.Server.Services;
using eVoting.SharedFiles;
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
    
    public class AuthenticationController : ControllerBase
    {

        private IVotersService _votersService;

        public AuthenticationController(IVotersService votersService)
        {
            _votersService = votersService;
        }


        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(400, Type = typeof(LoginResponse))]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var loginResponse = await _votersService.GenerateTokenAsync(model);
            if (loginResponse == null)
                return BadRequest("Invalid username or password");

            return Ok(loginResponse);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<string>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<string>))]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var loginResponse = await _votersService.RegisterVoterAsync(model);
            if (loginResponse.IsSuccess)
                return Ok(loginResponse);

            return BadRequest(loginResponse);

            
        }

    }
}
