using EverywhereNotes.Models.Entities;

namespace EverywhereNotes.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> IsEmailTakenAsync(string email);

        public Task<User> GetByEmailAsync(string email);

        public Task AddAsync(User user);

        public Task DeleteAsync(long id);

        public Task<List<User>> GetAllAsync();

        public Task<User> GetByIdAsync(long id);

        public void Update(User user);
    }
}
