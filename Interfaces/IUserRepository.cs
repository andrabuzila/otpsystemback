using otpsystemback.Data.Entities;

namespace otpsystemback.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Get();

        void Create(User entity);

        void Update(User entity);

        void Delete(User entity);
    }
}
