namespace EverywhereNotes.Contracts.Responses
{
    public class NoteResponse
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Color { get; set; }

        public DateTime CreationDateTime { get; init; }

        public DateTime? LastUpdateDateTime { get; set; }

        public long userId { get; init; }
    }
}
