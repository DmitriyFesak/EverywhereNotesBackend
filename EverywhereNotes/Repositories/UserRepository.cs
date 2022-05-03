using EverywhereNotes.Database;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EverywhereNotes.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override Task AddAsync(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<List<IEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<IEntity> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<IEntity> GetQueryableNoTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _dataContext.Users.AnyAsync(user => user.Email == email);
        }

        public override Task<bool> IsEntityExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
