using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;

namespace EverywhereNotes.Repositories
{
    public interface INotesRepository
    {
        public Task<NoteResponse> AddAsync(NoteRequest note);

        public Task DeleteAsync(long id);

        public Task<List<NoteResponse>> GetByUserIdAsync(long userId);
        
        public Task<List<NoteResponse>> GetBinByUserIdAsync(long userId);

        public Task<NoteResponse?> GetByIdAsync(long id);

        public Task UpdateAsync(NoteResponse note);
    }
}
