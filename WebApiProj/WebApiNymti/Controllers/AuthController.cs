using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
//using WebApiNymti.Auth;
using WebApiNymti.Helpers;
using WebApiNymti.Models;
using WebApiNymti.Models.Entities;
using WebApiNymti.ViewModels;

namespace WebApiNymti.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
       

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
          
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Post([FromBody]Credentials credentials)
        //{
        //    JwtSecurityToken token = GenerateJwtToken(credentials.Email);

        //    return Ok(new
        //    {
        //        token = new JwtSecurityTokenHandler().WriteToken(token)
        //    });
        //}



        // POST api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromBody]Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Credentials credentials = new Credentials();
            //credentials.Email = email; credentials.Password = password;

            var identity =  CheckValidity(credentials.Email, credentials.Password);
            if (identity.Result == false)
            {
                return BadRequest(Helpers.Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            //var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            SecurityToken token = GenerateJwtToken(credentials.Email);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

            // return new OkObjectResult(jwt);
        }

        private Task<bool>  CheckValidity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return Task.FromResult(false);

            // get the user to verifty
            var userToVerify =  _userManager.FindByNameAsync(userName);


            if (userToVerify == null) return Task.FromResult(false);




            // check the credentials
            if (( _userManager.CheckPasswordAsync( userToVerify.Result, password)).Result)
            {
               return Task.FromResult(true) ; //await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult(false);
        }

      
        public SecurityToken GenerateJwtToken( string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("iNivDmHLpUA223sqsfhqGbMRdRj1PVkH");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

    }
}