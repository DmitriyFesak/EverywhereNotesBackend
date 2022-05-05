using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Models.ResultModel;

namespace EverywhereNotes.Services
{
    public interface IUserService
    {
        public Task<Result<AuthSuccessResponse>> RegisterAsync(UserRegistrationRequest request);
        public Task<Result<AuthSuccessResponse>> AuthorizeAsync(UserAuthorizationRequest request);
    }
}
