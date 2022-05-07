using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Models.ResultModel;

namespace EverywhereNotes.Services
{
    public interface INotesService
    {
        public Task<Result<NoteResponse>> AddAsync(CreateNoteRequest note);

        public Task<Result<NoteResponse>> DeleteAsync(long id);

        public Task<Result<List<NoteResponse>>> GetByUserIdAsync();

        public Task<Result<NoteResponse>> GetByIdAsync(long id);

        public Task<Result<NoteResponse>> UpdateAsync(long id, CreateNoteRequest note);

        public Task<Result<NoteResponse>> MoveToTrashAsync(long id);
        
        public Task<Result<NoteResponse>> RestoreFromTrashAsync(long id);
    }
}
