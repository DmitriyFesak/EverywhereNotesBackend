using AutoMapper;
using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Database;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Services;
using Microsoft.EntityFrameworkCore;

namespace EverywhereNotes.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public NotesRepository(DataContext dataContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dataContext = dataContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<NoteResponse> AddAsync(NoteRequest note)
        {
            var createdNote = new Note
            {
                Title = note.Title,
                Content = note.Content,
                Color = note.Color,
                CreationDateTime = DateTime.Now,
                userId = _currentUserService.UserId
            };

            await _dataContext.Notes.AddAsync(createdNote);

            return _mapper.Map<NoteResponse>(createdNote);
        }

        public async Task DeleteAsync(long id)
        {
            var noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            
            if (noteToDelete != null)
            {
                _dataContext.Notes.Remove(noteToDelete);
            }
        }

        public async Task<List<NoteResponse>> GetBinByUserIdAsync(long userId)
        {
            var notes = await _dataContext.Notes.Where(x => x.userId == userId && x.MovedToBin).ToListAsync();

            return _mapper.Map<List<NoteResponse>>(notes);
        }

        public async Task<NoteResponse?> GetByIdAsync(long id)
        {
            var note = await _dataContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<List<NoteResponse>> GetByUserIdAsync(long userId)
        {
            var notes = await _dataContext.Notes.Where(x => x.userId == userId && !x.MovedToBin).ToListAsync();

            return _mapper.Map<List<NoteResponse>>(notes);
        }

        public async Task UpdateAsync(NoteResponse note)
        {
            var noteToUpdate = await _dataContext.Notes.FirstOrDefaultAsync(x => x.Id == note.Id);

            if (noteToUpdate == null)
            {
                return;
            }

            noteToUpdate.Title = note.Title;
            noteToUpdate.Content = note.Content;
            noteToUpdate.Color = note.Color;
            noteToUpdate.MovedToBin = note.MovedToBin;

            noteToUpdate.LastUpdateDateTime = DateTime.Now;

            _dataContext.Notes.Update(noteToUpdate);
        }
    }
}
