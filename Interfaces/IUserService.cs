using otpsystemback.Models;
using Microsoft.AspNetCore.Mvc;

namespace otpsystemback.Interfaces
{
    public interface IUserService
    {
        string GeneratePass();
        string GenerateToken(string password);
        void AddUser(UserRegisterModel registeredUser);
        int GetUserId(string email);
        IActionResult verifyUser(string email, string password);
        IActionResult GetUserToken(string email);
    }
}
