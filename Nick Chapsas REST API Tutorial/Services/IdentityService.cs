using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Nick_Chapsas_REST_API_Tutorial.Domain;
using Nick_Chapsas_REST_API_Tutorial.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not Exist" }
                };
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/Password combination is wrong" }
                };
            }
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser !=null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This Email Already Exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return GenerateAuthenticationResulForUSer(newUser);




        }
        private AuthenticationResult GenerateAuthenticationResulForUSer(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //Is Unique ID For JWT  Used For Token Validation
                    new Claim(JwtRegisteredClaimNames.Email,newUser.Email),
                    new Claim("id",newUser.Id),


                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
