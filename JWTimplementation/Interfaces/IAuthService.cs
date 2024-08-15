using JWTimplementation.Models;
using JWTimplementation.Request_Models;

namespace JWTimplementation.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);
    }
}
