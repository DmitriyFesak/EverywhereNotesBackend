using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Helpers;
using FluentValidation;

namespace EverywhereNotes.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationValidator()
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Password).NotEmpty().Matches(PasswordHelper.PasswordPattern).WithMessage("Password does not meet requirements.");
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords do not match.");
        }
    }
}
