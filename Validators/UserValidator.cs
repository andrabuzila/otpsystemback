using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using otpsystemback.Data.Helpers;
using otpsystemback.Interfaces;
using otpsystemback.Repositories;
using System.Net.Mail;

namespace otpsystemback.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository userRepository;
        public UserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult ValidateOTP(string password)
        {
            if (password.Length < 8 || password.Length > 20)
            {
                return new BadRequestObjectResult("Password length not ok");
            }
            else if (!IsAnyCharacterInOTP(password, Characters.LowerCaseLetters))
            {
                return new BadRequestObjectResult("Password does not contain Lower Cases");
            }
            else if (!IsAnyCharacterInOTP(password, Characters.UpperCaseLetters))
            {
                return new BadRequestObjectResult("Password does not contain Upper Cases");
            }
            else if (!IsAnyCharacterInOTP(password, Characters.Numbers))
            {
                return new BadRequestObjectResult("Password does not contain Numbers");
            }
            else if (!IsAnyCharacterInOTP(password, Characters.Punctuations))
            {
                return new BadRequestObjectResult("Password does not contain Punctuations");
            }
            else return new OkResult();
        }

        public string ValidateUserEmail(string email)
        {
            try
            {
                MailAddress check = new MailAddress(email);
                return "";
            }
            catch (FormatException)
            {
                return "Email is wrong";
            }
        }
        public bool CheckIfEmailExist(string email)
        {
            var user = this.userRepository.Get().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsAnyCharacterInOTP(string password, char[] charactersType)
        {
            foreach(char c in charactersType)
            {
                if (password.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
