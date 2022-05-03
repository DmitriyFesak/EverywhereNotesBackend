using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Models;
using EverywhereNotes.Models.ResultModel;

namespace EverywhereNotes.Services
{
    public interface IUserService
    {
        public Task<Result<AuthenticationResult>> RegisterUserAsync(UserRegistrationRequest request);
    }
}
