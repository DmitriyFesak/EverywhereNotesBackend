using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Helpers;
using FluentValidation;

namespace EverywhereNotes.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(user => user.OldPassword).NotEmpty();
            RuleFor(user => user.NewPassword).NotEmpty().Matches(PasswordHelper.PasswordPattern).WithMessage("New password does not meet requirements.");
            RuleFor(user => user.ConfirmNewPassword).Equal(user => user.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
