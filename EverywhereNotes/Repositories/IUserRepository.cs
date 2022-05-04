using EverywhereNotes.Models.Entities;

namespace EverywhereNotes.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> IsEmailTakenAsync(string email);

        public abstract Task AddAsync(User user);

        public abstract Task DeleteAsync(long id);

        public abstract Task<List<User>> GetAllAsync();

        public abstract Task<User> GetByIdAsync(long id);

        public abstract Task UpdateAsync(User user);

        public abstract IQueryable<User> GetQueryableNoTracking();
    }
}
