namespace EverywhereNotes.Services
{
    public interface ICurrentUserService
    {
        public long UserId { get; }

        public string Email { get; }
    }
}
