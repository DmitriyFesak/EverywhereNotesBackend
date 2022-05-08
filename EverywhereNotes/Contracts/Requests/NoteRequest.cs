using EverywhereNotes.Models.Enums;

namespace EverywhereNotes.Contracts.Requests
{
    public class NoteRequest
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NoteColor Color { get; set; }
    }
}
