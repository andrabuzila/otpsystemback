using Microsoft.AspNetCore.Mvc;

namespace otpsystemback.Interfaces
{
    public interface IUserValidator
    {
        IActionResult ValidateOTP(string password);

        string ValidateUserEmail(string email);

        bool CheckIfEmailExist(string email);
    }
}
