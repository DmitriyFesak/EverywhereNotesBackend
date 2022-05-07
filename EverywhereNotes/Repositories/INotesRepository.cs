using EverywhereNotes.Models.Entities;

namespace EverywhereNotes.Repositories
{
    public interface INotesRepository
    {
        public Task AddAsync(Note note);

        public Task DeleteAsync(long id);

        public Task<List<Note>> GetByUserIdAsync(long userId);
        
        public Task<List<Note>> GetBinByUserIdAsync(long userId);

        public Task<Note?> GetByIdAsync(long id);

        public void Update(Note note);
    }
}
