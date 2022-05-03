using EverywhereNotes.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace EverywhereNotes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository
            ?? new UserRepository(_dataContext);

        public IDbContextTransaction BeginTransaction()
        {
            return _dataContext.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dataContext.Dispose();
        }
    }
}
