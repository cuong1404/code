using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens;

namespace DatingApp.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthController (IAuthRepository authRepository, IConfiguration config) {
            _authRepository = authRepository;
            _config = config;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserForRegisterDto user) {
            if (await _authRepository.ExistName (user.Username.ToLower ())) {
                return BadRequest ("UserName already exists!");
            }
            var userToCreate = new User ();
            userToCreate.Username = user.Username;
            var createdUser = await _authRepository.Register (userToCreate, user.Password);
            return StatusCode (201);
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login (UserForLoginDto user) {
            var userLogin = await _authRepository.Login (user.Username.ToLower (), user.Password);
            if (userLogin == null) {
                return Unauthorized ();
            }

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, userLogin.Id.ToString ()),
                new Claim (ClaimTypes.Name, userLogin.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);
            return Ok (new { token = tokenHandler.WriteToken(token)});
        }
    }
}