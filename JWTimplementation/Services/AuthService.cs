using JWTimplementation.Context;
using JWTimplementation.Interfaces;
using JWTimplementation.Models;
using JWTimplementation.Request_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTimplementation.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtService;
        private readonly IConfiguration _configuration;
        public AuthService(JwtContext jwtcontext, IConfiguration configuration)
        {
            _jwtService = jwtcontext;
            _configuration = configuration; 
        }
        public User AddUser(User user)
        {
            var addedUser = _jwtService.Users.Add(user);
            _jwtService.SaveChanges();
            return addedUser.Entity;
        }

        public string Login(LoginRequest loginRequest)
        {
            if(loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = _jwtService.Users.SingleOrDefault(s => s.Email == loginRequest.Username && s.Password == loginRequest.Password);
                if (user != null) 
                {
                    var claims = new[]
                    {
                         new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                         new Claim("Id", user.Id.ToString()),
                         new Claim("UserName",user.Name),
                         new Claim("Email",user.Email)
                     };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;

                }
                else
                {
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }
    }
}
