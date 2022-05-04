namespace EverywhereNotes.Models.Entities
{
    public class User : IEntity
    {
        public long Id { get; init; }

        public string Email { get; init; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
