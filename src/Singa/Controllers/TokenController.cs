using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;

using System.Text;
using System.Security.Principal;


using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Singa.Models;
using Microsoft.AspNetCore.Identity;
using Singa.Data;
using Microsoft.EntityFrameworkCore;

namespace Singa.ApiControllers
{
    using TokenOptions = Models.TokenOptions;

    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private SignInManager<ApplicationUser> SignInManager { get; }
        private ApplicationDbContext Db { get; }

        public TokenController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            Db = db;
            SignInManager = signInManager;
        }

        [HttpPost]
        public Task<IActionResult> Post(UserApi user)
        {
            return GenerateToken(user.username, user.password);
        }


        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = Db.Users.Include(x => x.Roles).FirstOrDefault(x => x.UserName == username);
               // var roleClaims = user.Roles;//Claims.Where(x => x.ClaimType == ClaimsIdentity.DefaultRoleClaimType);
                var roleClaims = new List<Claim>();
                foreach (var userRole in user.Roles)
                {
                    var role = Db.Roles.Single(x => x.Id == userRole.RoleId);
                    roleClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
                }
                var claims = new ClaimsIdentity(new GenericIdentity(username, "Token"), roleClaims.ToArray());
                return claims;

            }
            // Don't do this in production, obviously!
            if (username == "TEST" && password == "TEST123")
            {
                var claims = new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin") });
                return claims;
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }


        private async Task<IActionResult> GenerateToken(string username, string password)
        {
            //var username = context.Request.Form["username"];
            //var password = context.Request.Form["password"];

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
                //return new JsonResult(new { status = "fail", message = "Ошибка при регистрации", errors = result.Errors });
                //context.Response.StatusCode = 400;
                //await context.Response.WriteAsync("Invalid username or password.");
                //return;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
                //,new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
            };

            claims.AddRange(identity.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType));

            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TokenOptions.Expires),
                signingCredentials:  new SigningCredentials(TokenOptions.SigningKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TokenOptions.Expires.TotalSeconds
            };

            //context.Response.ContentType = "application/json";
            /*var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };*/
            return new JsonResult(response);
        }



        public static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

       public class UserApi
    {        
        public string username { get; set; }
        public string password { get; set; }
    }
}