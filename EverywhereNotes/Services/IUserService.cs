using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Models.ResultModel;

namespace EverywhereNotes.Services
{
    public interface IUserService
    {
        public Task<Result<AuthSuccessResponse>> RegisterUserAsync(UserRegistrationRequest request);
    }
}
