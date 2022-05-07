using Microsoft.EntityFrameworkCore.Storage;

namespace EverywhereNotes.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
     
        public INotesRepository NotesRepository { get; }

        public Task CommitAsync();

        public void Rollback();

        public IDbContextTransaction BeginTransaction();
    }
}
