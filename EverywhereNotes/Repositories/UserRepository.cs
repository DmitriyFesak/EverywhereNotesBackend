using EverywhereNotes.Database;
using EverywhereNotes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EverywhereNotes.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(User user)
        {
            await _dataContext.Users.AddAsync(user);
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetQueryableNoTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _dataContext.Users.AnyAsync(user => user.Email == email);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
