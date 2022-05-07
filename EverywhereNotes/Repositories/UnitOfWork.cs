using EverywhereNotes.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace EverywhereNotes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private INotesRepository _notesRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository
            ?? new UserRepository(_dataContext);

        public INotesRepository NotesRepository => _notesRepository = _notesRepository
            ?? new NotesRepository(_dataContext);

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
