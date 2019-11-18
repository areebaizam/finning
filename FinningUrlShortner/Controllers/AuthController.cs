using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinningUrlShortner.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinningUrlShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            if (!(userForLogin.Username=="finning" && userForLogin.Password=="password"))
                return Unauthorized();
            //Logic to check UserName from Repo goes here
            // Sample Code:  var userFromRepo = await _repo.Login(userForLogin.Username, userForLogin.Password);
            // Verify Username in Repo: var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.PasswordHash == password);

            //CREATE CLAIMS
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userForLogin.Username),
                new Claim(ClaimTypes.Role,"admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((_config.GetSection("AppSettings:TokenKey")).Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}