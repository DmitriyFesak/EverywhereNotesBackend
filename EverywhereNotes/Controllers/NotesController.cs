using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Extensions;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverywhereNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var notes = await _notesService.GetByUserIdAsync();
            
            return notes.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var note = await _notesService.GetByIdAsync(id);

            return note.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateNoteRequest request)
        {
            var note = await _notesService.AddAsync(request);

            return note.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] UpdateNoteRequest request)
        {
            var note = await _notesService.UpdateAsync(request);

            return note.ToActionResult();
        }

        [HttpPatch("trash/{id}")]
        public async Task<IActionResult> MoveToTrashAsync(long id)
        {
            var note = await _notesService.MoveToTrashAsync(id);

            return note.ToActionResult();
        }

        [HttpPatch("restore/{id}")]
        public async Task<IActionResult> MoveFromTrashAsync(long id)
        {
            var note = await _notesService.RestoreFromTrashAsync(id);

            return note.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var note = await _notesService.DeleteAsync(id);

            return note.ToActionResult();
        }
    }
}
