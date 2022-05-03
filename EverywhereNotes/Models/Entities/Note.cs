namespace EverywhereNotes.Models.Entities
{
    public class Note : IEntity
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }

        public DateTime CreationDateTime { get; init; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
