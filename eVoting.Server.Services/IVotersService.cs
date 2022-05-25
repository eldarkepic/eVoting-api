using eVoting.Repositories;
using eVoting.Server.Infrastructure;
using eVoting.Server.Models.Models;
using eVoting.SharedFiles;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eVoting.Server.Services
{
    public interface IVotersService
    {
        Task<OperationResponse<string>> RegisterVoterAsync(RegisterRequest model);
        Task<LoginResponse> GenerateTokenAsync(LoginRequest model);

    }

    public class VoterService : IVotersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthOptions _authOptions;

        public VoterService(IUnitOfWork unitOfWork, AuthOptions authOptions)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions;
        }
        public async Task<LoginResponse> GenerateTokenAsync(LoginRequest model)
        {
            var voter = await _unitOfWork.Voters.GetVoterByEmailAsync(model.Email);
            if(voter == null)
            {
                return new LoginResponse {
                    Message = "Invalid username or password",
                    IsSuccess = false
                };
            }
            if(!(await _unitOfWork.Voters.CheckPasswordAsync(voter, model.Password)))
            {
                return null;
            }

            var userRole = await _unitOfWork.Voters.GetVoterRoleAsync(voter);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, voter.Id),
                new Claim(ClaimTypes.GivenName, voter.FirstName),
                new Claim(ClaimTypes.Surname, voter.LastName),
                new Claim(ClaimTypes.Email, voter.Email),
                new Claim(ClaimTypes.Role, userRole),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
            
            
            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Message = "Login successful",
                IsSuccess = true,
                AccessToken = tokenAsString,
                ExpiryDate = DateTime.Now.AddDays(30),
            };

        }

        public async Task<OperationResponse<string>> RegisterVoterAsync(RegisterRequest model)
        {
            var userByEmail = await _unitOfWork.Voters.GetVoterByEmailAsync(model.Email);
            
            if (userByEmail != null)
            {
                return new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "User already exists",
                };
            }

            var voter = new Voter
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                JMBG = model.JMBG,
                IdCard = model.IdCard
            };

            await _unitOfWork.Voters.CreateVoterAsync(voter, model.Password, "User");

            return new OperationResponse<string>
            {
                Message = "Welcome to eVoting",
                IsSuccess = true
            };
        }
    }
}
