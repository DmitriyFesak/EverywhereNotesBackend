﻿using AutoMapper;
using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Extensions;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Models.Enums;
using EverywhereNotes.Models.ResultModel;
using EverywhereNotes.Repositories;

namespace EverywhereNotes.Services
{
    public class NotesService : INotesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public NotesService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<NoteResponse>> AddAsync(NoteRequest note)
        {
            try
            {
                if (note == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.ValidationError, "Note is null!");
                }

                var noteResponse = await _unitOfWork.NotesRepository.AddAsync(note);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(noteResponse);
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

                if (foundNote.userId != _currentUserService.UserId)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Only owner can reach the note!");
                }

                                if (!foundNote.MovedToBin)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Note can be deleted only from bin!");
                }

                await _unitOfWork.NotesRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(foundNote);
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<List<NoteResponse>>> GetBinByUserIdAsync()
        {
            var userId = _currentUserService.UserId;

            var foundNotes = await _unitOfWork.NotesRepository.GetBinByUserIdAsync(userId);

            if (foundNotes == null || foundNotes.Count == 0)
            {
                return Result<List<NoteResponse>>.GetError(ErrorCode.NotFound, "Bin is empty!");
            }

            return Result<List<NoteResponse>>.GetSuccess(foundNotes);
        }

        public async Task<Result<NoteResponse>> GetByIdAsync(long id)
        {
            var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

            if (foundNote == null)
            {
                return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
            }

            if (foundNote.userId != _currentUserService.UserId)
            {
                return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Only owner can reach the note!");
            }

            return Result<NoteResponse>.GetSuccess(foundNote);
        }

        public async Task<Result<List<NoteResponse>>> GetByUserIdAsync()
        {
            var userId = _currentUserService.UserId;

            var foundNotes = await _unitOfWork.NotesRepository.GetByUserIdAsync(userId);

            if (foundNotes == null || foundNotes.Count == 0)
            {
                return Result<List<NoteResponse>>.GetError(ErrorCode.NotFound, "Notes with this user id were not found!");
            }

            return Result<List<NoteResponse>>.GetSuccess(foundNotes);
        }

        public async Task<Result<NoteResponse>> MoveToBinAsync(long id)
        {
            try
            {
                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
                }

                if (foundNote.userId != _currentUserService.UserId)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Only owner can reach the note!");
                }

                if (foundNote.MovedToBin)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Conflict, "Note with this id is already in bin!");
                }

                foundNote.MovedToBin = true;

                await _unitOfWork.NotesRepository.UpdateAsync(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(foundNote);
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> RestoreFromBinAsync(long id)
        {
            try
            {
                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note with this id was not found!");
                }

                if (foundNote.userId != _currentUserService.UserId)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Only owner can reach the note!");
                }

                if (!foundNote.MovedToBin)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Conflict, "Note with this id is not in bin!");
                }

                foundNote.MovedToBin = false;

                await _unitOfWork.NotesRepository.UpdateAsync(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(foundNote);
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }

        public async Task<Result<NoteResponse>> UpdateAsync(long id, NoteRequest note)
        {
            try
            {
                if (note == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note request is null!");
                }

                var foundNote = await _unitOfWork.NotesRepository.GetByIdAsync(id);

                if (foundNote == null)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.NotFound, "Note was not found!");
                }

                if (foundNote.userId != _currentUserService.UserId)
                {
                    return Result<NoteResponse>.GetError(ErrorCode.Forbidden, "Only owner can reach the note!");
                }

                foundNote.Title = note.Title;
                foundNote.Content = note.Content;
                foundNote.Color = note.Color;

                await _unitOfWork.NotesRepository.UpdateAsync(foundNote);
                await _unitOfWork.CommitAsync();

                return Result<NoteResponse>.GetSuccess(foundNote);
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<NoteResponse>.GetError(ErrorCode.InternalServerError, "Internal error");
            }
        }
    }
}
