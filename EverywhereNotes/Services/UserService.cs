using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Models;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Models.ResultModel;
using EverywhereNotes.Repositories;

namespace EverywhereNotes.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenticationResult>> RegisterUserAsync(UserRegistrationRequest request)
        {
            var isEmailTaken = await _unitOfWork.UserRepository.IsEmailTakenAsync(request.Email);

            if (isEmailTaken)
            {
                return Result<AuthenticationResult>.GetError(ErrorCode.Conflict, "This email is already used!");
            }

            /*try
            {
                var user = new User
                {
                    Email = request.Email,
                    Password = request.Password
                };


            }*/
            return Result<AuthenticationResult>.GetSuccess(new AuthenticationResult() 
            {
                Success = true
            });
        }
    }
}
