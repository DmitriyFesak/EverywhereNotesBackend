using System.IdentityModel.Tokens.Jwt;

namespace EverywhereNotes.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private long _userId;
        private string? _email;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UserId
        {
            get
            {
                if (_userId == default)
                {
                    var userIdString = GetClaimValue(claimType: "Id");

                    if (!long.TryParse(userIdString, out _userId))
                    {
                        throw new UnauthorizedAccessException("Not authorized!");
                    }
                }

                return _userId;
            }
        }

        public string Email
        {
            get
            {
                if (string.IsNullOrEmpty(_email))
                {
                    _email = GetClaimValue(claimType: "Email");

                    if (_email is null)
                    {
                        throw new UnauthorizedAccessException("Not authorized!");
                    }
                }

                return _email;
            }
        }

        /// <summary>
        /// Gets claim value by the given claim type.
        /// </summary>
        /// <returns>string representation of claim value,
        /// or null if user is not authentified</returns>
        private string? GetClaimValue(string claimType)
        {
            var v = _httpContextAccessor
                .HttpContext
                !.User
                ?.Claims
                ?.SingleOrDefault(claim => claim.Type == claimType)
                ?.Value;

            return v;
        }
    }
}
