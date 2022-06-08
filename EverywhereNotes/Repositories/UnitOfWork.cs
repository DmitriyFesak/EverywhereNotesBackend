using AutoMapper;
using EverywhereNotes.Database;
using EverywhereNotes.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace EverywhereNotes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private INotesRepository _notesRepository;

        public UnitOfWork(DataContext dataContext, IServiceProvider serviceProvider)
        {
            _dataContext = dataContext;
            _serviceProvider = serviceProvider;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository
            ?? new UserRepository(_dataContext);

        public INotesRepository NotesRepository => _notesRepository = _notesRepository
            ?? new NotesRepository(_dataContext, _serviceProvider.GetService<ICurrentUserService>(), _serviceProvider.GetService<IMapper>());

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
