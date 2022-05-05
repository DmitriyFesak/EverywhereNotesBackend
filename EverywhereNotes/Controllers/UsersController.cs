using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Contracts.Responses;
using EverywhereNotes.Extensions;
using EverywhereNotes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EverywhereNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [SwaggerResponse(200, type: typeof(AuthSuccessResponse))]
        [Route("authorize")]
        [HttpPost]
        public async Task<IActionResult> AuthorizeUser(UserAuthorizationRequest request)
        {
            var response = await _userService.AuthorizeAsync(request);

            return response.ToActionResult();
        }

        [SwaggerResponse(200, type: typeof(AuthSuccessResponse))]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequest request)
        {
            var response = await _userService.RegisterAsync(request);

            return response.ToActionResult();
        }

        [Authorize]
        [Route("password")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var response = await _userService.ChangePasswordAsync(request);
            
            return response.ToActionResult();
        }
    }
}
