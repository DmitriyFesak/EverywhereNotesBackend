using EverywhereNotes.Database;
using EverywhereNotes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EverywhereNotes.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private DataContext _dataContext;

        public NotesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(Note note)
        {
            await _dataContext.Notes.AddAsync(note);
        }

        public async Task DeleteAsync(long id)
        {
            var noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            
            if (noteToDelete != null)
            {
                _dataContext.Notes.Remove(noteToDelete);
            }
        }

        public async Task<Note?> GetByIdAsync(long id)
        {
            var note = await _dataContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            return note;
        }

        public async Task<List<Note>> GetByUserIdAsync(long userId)
        {
            var notes = await _dataContext.Notes.Where(x => x.userId == userId).ToListAsync();

            return notes;
        }

        public void Update(Note note)
        {
            _dataContext.Notes.Update(note);
        }
    }
}
