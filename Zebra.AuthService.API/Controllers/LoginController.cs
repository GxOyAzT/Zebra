using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Zebra.AuthService.API.Models;
using Zebra.AuthService.API.Resources;
using Zebra.AuthService.API.Services.Token;

namespace Zebra.AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICreateToken _createToken;
        private readonly IStringLocalizer<ClientLocalizer> _localizer;

        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ICreateToken createToken,
            IStringLocalizer<ClientLocalizer> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _createToken = createToken;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterLoginClientApiModel user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            if (identityUser is null)
            {
                return BadRequest(_localizer["IncorrectPasswordOrEmail"].Value);
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(identityUser, user.Password, false);

            if (!loginResult.Succeeded)
            {
                return BadRequest(_localizer["IncorrectPasswordOrEmail"].Value);
            }

            var claims = await _userManager.GetClaimsAsync(identityUser);

            var token = _createToken.Create(claims);

            return Ok(new TokenApiModel(user.Email, token));
        }
    }
}
