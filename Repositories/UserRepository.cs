using otpsystemback.Interfaces;
using otpsystemback.Data.Entities;
using otpsystemback.Data;

namespace otpsystemback.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected ApplicationDbContext DbContext { get; set; }

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IQueryable<User> Get()
        {
            return this.DbContext.Set<User>();
        }

        public void Create(User entity)
        {
            this.DbContext.Set<User>().Add(entity);
            this.DbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            this.DbContext.Set<User>().Update(entity);
            this.DbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            this.DbContext.Set<User>().Remove(entity);
            this.DbContext.SaveChanges();
        }
    }
}
