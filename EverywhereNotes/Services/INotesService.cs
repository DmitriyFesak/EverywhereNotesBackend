using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Models.ResultModel;

namespace EverywhereNotes.Services
{
    public interface INotesService
    {
        public Task<Result<NoteResponse>> AddAsync(NoteRequest note);

        public Task<Result<NoteResponse>> DeleteAsync(long id);

        public Task<Result<List<NoteResponse>>> GetByUserIdAsync();

        public Task<Result<NoteResponse>> GetByIdAsync(long id);

        public Task<Result<NoteResponse>> UpdateAsync(long id, NoteRequest note);

        public Task<Result<List<NoteResponse>>> GetBinByUserIdAsync();

        public Task<Result<NoteResponse>> MoveToBinAsync(long id);
        
        public Task<Result<NoteResponse>> RestoreFromBinAsync(long id);
    }
}
