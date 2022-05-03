namespace EverywhereNotes.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> IsEmailTakenAsync(string email);
    }
}
