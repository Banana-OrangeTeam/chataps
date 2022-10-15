using Chat.Common.DTOs.UserDTOs;
using Chat.Core.Exceptions;
using Chat.Core.Helpers;
using Chat.Core.Interfaces.Services;
using Chat.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public AuthentificationService(
            UserManager<User> userManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }

        public async Task RegistrationAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if(! result.Succeeded)
            {
                StringBuilder errorMessage = new();
                foreach (var error in result.Errors)
                {
                    errorMessage.Append(error.Description.ToString() + " ");
                }
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, errorMessage.ToString());
            }
        }

        public async Task<UserAutorizationDTO> LoginAsync(LoginDTO logDTOs)
        {
            var user = await _userManager.FindByEmailAsync(logDTOs.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, logDTOs.Password))
            {
                throw new HttpException(System.Net.HttpStatusCode.Unauthorized, "Incorect login or password");
            }

            return new UserAutorizationDTO()
            {
                Token = CreateToken(user)
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.ValidIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.Value.LifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
