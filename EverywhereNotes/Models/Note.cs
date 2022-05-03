namespace EverywhereNotes.Models
{
    public class Note
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }

        public DateTime CreationDateTime { get; init; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
