using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Helpers;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Models.ResultModel;
using EverywhereNotes.Repositories;

namespace EverywhereNotes.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenHelper _tokenHelper;

        public UserService(IUnitOfWork unitOfWork, TokenHelper tokenHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
        }

        public async Task<Result<AuthSuccessResponse>> AuthorizeAsync(UserAuthorizationRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            
            if (user == null)
            {
                return Result<AuthSuccessResponse>.GetError(ErrorCode.NotFound, "User not found!");
            }

            var passwordHash = PasswordHelper.HashPassword(request.Password, user.Salt);

            var isAuthorized = passwordHash == user.Password;

            if (!isAuthorized)
            {
                return Result<AuthSuccessResponse>.GetError(ErrorCode.Unauthorized, "Wrong password!");
            }
            else
            {
                var token = _tokenHelper.GenerateToken(user);

                return Result<AuthSuccessResponse>.GetSuccess(new AuthSuccessResponse { Token = token });
            }
        }

        public async Task<Result<AuthSuccessResponse>> RegisterAsync(UserRegistrationRequest request)
        {
            var isEmailTaken = await _unitOfWork.UserRepository.IsEmailTakenAsync(request.Email);

            if (isEmailTaken)
            {
                return Result<AuthSuccessResponse>.GetError(ErrorCode.Conflict, "This email is already used!");
            }

            //TODO: Get rid of this after adding validation
            if (request.Password != request.ConfirmPassword)
            {
                return Result<AuthSuccessResponse>.GetError(ErrorCode.Conflict, "Passwords do not match!");
            }

            try
            {
                var user = new User
                {
                    Email = request.Email,
                };

                user.Salt = PasswordHelper.GenerateSalt();
                user.Password = PasswordHelper.HashPassword(request.Password, user.Salt);

                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();

                var token = _tokenHelper.GenerateToken(user);

                return Result<AuthSuccessResponse>.GetSuccess(new AuthSuccessResponse { Token = token });
            }
            catch
            {
                _unitOfWork.Rollback();

                return Result<AuthSuccessResponse>.GetError(ErrorCode.InternalServerError, "Cannot create account.");
            }
        }
    }
}
