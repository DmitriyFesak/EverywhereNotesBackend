using EverywhereNotes.Contracts.Requests;
using EverywhereNotes.Extensions;
using EverywhereNotes.Services;
using Microsoft.AspNetCore.Mvc;

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

        //[SwaggerResponse(200, type: typeof(AccountDto))]
        [Route("reg")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequest request)
        {
            var response = await _userService.RegisterUserAsync(request);

            return response.ToActionResult();
        }
    }
}
