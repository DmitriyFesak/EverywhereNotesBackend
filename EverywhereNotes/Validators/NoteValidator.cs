using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Models.Enums;
using FluentValidation;

namespace EverywhereNotes.Validators
{
    public class NoteValidator : AbstractValidator<NoteRequest>
    {
        public NoteValidator()
        {
            RuleFor(note => note.Title).MaximumLength(100);
            RuleFor(note => note.Content).NotEmpty().MaximumLength(4000);
            RuleFor(note => note.Color).IsInEnum();
        }
    }
}
