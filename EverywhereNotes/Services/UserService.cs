using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Helpers;
using EverywhereNotes.Models.Entities;
using EverywhereNotes.Models.Enums;
using EverywhereNotes.Models.ResultModel;
using EverywhereNotes.Repositories;

namespace EverywhereNotes.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenHelper _tokenHelper;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IUnitOfWork unitOfWork, TokenHelper tokenHelper,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _currentUserService = currentUserService;
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

        public async Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return Result<string>.GetError(ErrorCode.ValidationError, "Passwords don't match!");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(_currentUserService.UserId);

            if (user.Password != PasswordHelper.HashPassword(request.OldPassword, user.Salt))
            {
                return Result<string>.GetError(ErrorCode.ValidationError, "Old password is wrong!");
            }

            user.Salt = PasswordHelper.GenerateSalt();

            var newPasswordHash = PasswordHelper.HashPassword(request.NewPassword, user.Salt);

            user.Password = newPasswordHash;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CommitAsync();

            return Result<string>.GetSuccess("Password was changed!");
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
