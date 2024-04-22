using otpsystemback.Models;
using Microsoft.AspNetCore.Mvc;

namespace otpsystemback.Interfaces
{
    public interface IUserService
    {
        string GeneratePass();
        void AddUser(UserRegisterModel registeredUser);
        IActionResult verifyUser(string email, string password);
        IActionResult GetUserToken(string email);
    }
}
