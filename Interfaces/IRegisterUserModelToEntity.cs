using otpsystemback.Data.Entities;
using otpsystemback.Models;

namespace otpsystemback.Interfaces
{
    public interface IRegisterUserModelToEntity
    {
        User RegisterModelToUser(UserRegisterModel registeredUser);
    }
}
