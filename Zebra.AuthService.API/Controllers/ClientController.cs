using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Zebra.AuthService.API.Models;
using Zebra.AuthService.API.Resources;

namespace Zebra.AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<ClientLocalizer> _localizer;

        public ClientController(
            UserManager<IdentityUser> userManager, 
            IStringLocalizer<ClientLocalizer> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterLoginClientApiModel user)
        {
            if (user is null)
            {
                return BadRequest(_localizer["IncorrectPasswordOrEmailFormat"].Value);
            }

            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(_localizer["IncorrectPasswordOrEmailFormat"].Value);
            }

            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return BadRequest(_localizer["EmailAlreadyExists"].Value);
            }

            var identityUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            var registerResult = await _userManager.CreateAsync(identityUser, user.Password);
               
            if (!registerResult.Succeeded)
            {
                return BadRequest(_localizer["UnableToRegister"].Value);
            }

            var claim = new Claim("_userType", "_customerClient");

            var registeredUser = await _userManager.FindByEmailAsync(user.Email);

            await _userManager.AddClaimAsync(registeredUser, claim);

            return Ok();
        }
    }
}
