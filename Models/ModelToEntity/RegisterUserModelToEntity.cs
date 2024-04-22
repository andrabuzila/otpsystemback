using otpsystemback.Interfaces;
using otpsystemback.Data.Entities;

namespace otpsystemback.Models.ModelToEntity
{
    public class RegisterUserModelToEntity : IRegisterUserModelToEntity
    {
        public RegisterUserModelToEntity()
        {

        }

        public User RegisterModelToUser(UserRegisterModel registeredUser)
        {
            return new User
            {
                Id = registeredUser.Id,
                Email = registeredUser.Email,
                PasswordHash = registeredUser.PasswordHash,
                Salt = registeredUser.Salt
            };
        }
    }
}
