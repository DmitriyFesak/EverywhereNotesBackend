using AutoMapper;
using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Models.ResultModel;
using EverywhereNotes.Repositories;

namespace EverywhereNotes.Services
{
    public class NotesService : INotesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public NotesService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<NoteResponse>> AddAsync(CreateNoteRequest note)
        {
            try
            {
                if (note == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.ValidationError, "Note is null!");
                }

                var createdNote = new Note
                {
                    Title = note.Title,
                    Content = note.Content,
                    CreationDateTime = DateTime.Now,
                    userId = _currentUserService.UserId
                };

                await _unitOfWork.NotesRepository.AddAsync(createdNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(createdNote));
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> DeleteAsync(long id)
        {
            try
            {
                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
                }

                await _unitOfWork.NotesRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(foundNote));
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> GetByIdAsync(long id)
        {
            var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

            if (foundNote == null)
            {
                return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
            }

            return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(foundNote));
        }

        public async Task<Result<List<NoteResponse>>> GetByUserIdAsync()
        {
            var userId = _currentUserService.UserId;

            var foundNotes = await _unitOfWork.NotesRepository.GetByUserIdAsync(userId);

            if (foundNotes == null || foundNotes.Count == 0)
            {
                return Result<List<NoteResponse>>.GetError(ErrorCode.NotFound, "Notes with this user id were not found!");
            }

            return Result<List<NoteResponse>>.GetSuccess(_mapper.Map<List<NoteResponse>>(foundNotes));
        }

        public async Task<Result<NoteResponse>> MoveToTrashAsync(long id)
        {
            try
            {
                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
                }

                if (foundNote.IsInTrash)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Conflict, "Note with this id is already in trash!");
                }

                foundNote.IsInTrash = true;

                _unitOfWork.NotesRepository.Update(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(foundNote));
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> RestoreFromTrashAsync(long id)
        {
            try
            {
                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
                }

                if (!foundNote.IsInTrash)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Conflict, "Note with this id is not in trash!");
                }

                foundNote.IsInTrash = false;

                _unitOfWork.NotesRepository.Update(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(foundNote));
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> UpdateAsync(UpdateNoteRequest note)
        {
            try
            {
                if (note == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note request is null!");
                }

                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(note.Id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note was not found!");
                }

                foundNote.Title = note.Title;
                foundNote.Content = note.Content;
                foundNote.LastUpdateDateTime = DateTime.Now;

                _unitOfWork.NotesRepository.Update(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(_mapper.Map<NoteResponse>(foundNote));
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }
    }
}
