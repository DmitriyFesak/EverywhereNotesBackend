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

        public async Task DeleteAsync(long id)
        {
            var userToDelete = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToDelete != null)
            {
                _dataContext.Users.Remove(userToDelete);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dataContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user; 
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _dataContext.Users.AnyAsync(user => user.Email == email);
        }

        public void Update(User user)
        {
            _dataContext.Users.Update(user);
        }
    }
}
