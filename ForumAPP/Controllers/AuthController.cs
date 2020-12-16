using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ForumAPP.Data;
using ForumAPP.Data.Models;
using ForumAPP.Dtos;
using ForumAPP.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace ForumAPP.Controllers
{
    public class AuthController : Controller
    {
        private readonly ForumContext _forumContext;
        private readonly AppSettings _options;

        public AuthController(IOptions<AppSettings> options, ForumContext forumContext)
        {
            _forumContext = forumContext;
            _options = options.Value;
        }

        [HttpPut("[controller]/{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserDto user)
        {
            var existingUser = await _forumContext.Users.FindAsync(id);
            
            CreatePasswordHash(user.Password, existingUser.PasswordSalt, out var passwordHash);
            existingUser.PasswordHash = passwordHash;
            _forumContext.Users.Update(existingUser);
            try
            {
                if (await _forumContext.SaveChangesAsync() == 0)
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return Ok(new ValidationDto() {Message = exception.InnerException?.Message});
            }

            return Ok();
        }
        
        [HttpPost("/authorize")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var (securityToken, userFromDb) = await LoginUser(user);

            if (securityToken == null)
                return BadRequest();
            
            var jwtToken = securityToken as JwtSecurityToken;
            HttpContext.Response.Cookies.Append("Token", jwtToken?.RawData, new CookieOptions()
            {
                Expires = securityToken.ValidTo,
                HttpOnly = true
            });
            user.Token = jwtToken?.RawData;
            user.Id = userFromDb.Id;

            if (ModelState.IsValid)
            {
                return Ok(user);
            }

            return BadRequest();
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        private void CreatePasswordHash(string password, byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        private async Task<(SecurityToken, User)> LoginUser(UserDto user)
        {
            var userFromRepo =
                await _forumContext.Users.FirstOrDefaultAsync(user1 => user1.Login.ToLower() == user.Login.ToLower());

            if (userFromRepo == null)
                userFromRepo = await RegisterUser(user);
            
            if (!VerifyPasswordHash(user.Password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return default;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_options.Token));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (token, userFromRepo);
        }

        private async Task<User> RegisterUser(UserDto user)
        {
            user.Login = user.Login.ToLower();
            
            var userToCreate = new User()
            {
                Login = user.Login
            };

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            userToCreate.PasswordHash = passwordHash;
            userToCreate.PasswordSalt = passwordSalt;

            await _forumContext.Users.AddAsync(userToCreate);
            await _forumContext.SaveChangesAsync();

            return userToCreate;
        }
    }
}