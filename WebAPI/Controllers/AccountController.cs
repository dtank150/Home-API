using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto ld)
        {
            var user = await uow.UserRepository.Authenticate(ld.Username, ld.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            var loginRes = new LoginResDto();
            loginRes.Username = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginDto ld)
        {
            if (await uow.UserRepository.UserAlredyExists(ld.Username))
                return BadRequest("User Aleready Exists, Please Try Somthing Else");
            uow.UserRepository.Register(ld.Username, ld.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        private string CreateJWT(User user)
        {
            var secrectKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var Credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = Credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
