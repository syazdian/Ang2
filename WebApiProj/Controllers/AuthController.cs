using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
       

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
          
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity =  CheckValidity(credentials.UserName, credentials.Password);
            if (identity.Result == false)
            {
                return BadRequest(Helpers.Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            //var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            JwtSecurityToken token = GenerateJwtToken(credentials.UserName);
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

      
        public JwtSecurityToken GenerateJwtToken( string username)
        {
            var claims = new[]
             {
                new Claim(ClaimTypes.Name, username)
            };


            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("hcxDUVp_JTu_yFzn856ZI_sj5VGCgwaleYkzvKgsQYbIO0stHyjoOyh4esE"));
          //  var key2 = Encoding.ASCII.GetBytes("hcxDUVp_JTu_yFzn856ZI_sj5VGCgwaleYkzvKgsQYbIO0stHyjoOyh4esE");
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:43416/",
                audience: "http://localhost:43416/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            return token;
        }

    }
}