using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Helpers;
using FluentValidation;

namespace EverywhereNotes.Validators
{
    public class UserAuthorizationValidator : AbstractValidator<UserAuthorizationRequest>
    {
        public UserAuthorizationValidator()
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Password).NotEmpty();
        }
    }
}
