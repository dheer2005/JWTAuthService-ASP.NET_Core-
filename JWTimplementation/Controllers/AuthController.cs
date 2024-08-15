using JWTimplementation.Interfaces;
using JWTimplementation.Models;
using JWTimplementation.Request_Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTimplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authservice)
        {
            _authService = authservice;
        }

       

        // POST api/<AuthController>
        [HttpPost]
        public string Login([FromBody] LoginRequest loginModel)
        {
            var result = _authService.Login(loginModel);
            return result;
        }

        // POST api/<AuthController>/5
        [HttpPost("addUser")]
        public User AddUSer([FromBody] User value)
        {
            var user = _authService.AddUser(value);
            return user;
        }

    }
}
