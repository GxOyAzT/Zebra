using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Zebra.AuthService.API.Models;
using Zebra.AuthService.API.Services.Token;

namespace Zebra.AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICreateToken _createToken;

        public ClientController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            ICreateToken createToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _createToken = createToken;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterLoginClientApiModel user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return BadRequest();
            }

            var identityUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            var registerResult = await _userManager.CreateAsync(identityUser, user.Password);
               
            if (!registerResult.Succeeded)
            {
                return BadRequest();
            }

            var claim = new Claim("_userType", "_customerClient");

            var registeredUser = await _userManager.FindByEmailAsync(user.Email);

            await _userManager.AddClaimAsync(registeredUser, claim);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterLoginClientApiModel user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            if (identityUser is null)
            {
                return BadRequest();
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(identityUser, user.Password, false);

            if (!loginResult.Succeeded)
            {
                return BadRequest();
            }

            var claims = await _userManager.GetClaimsAsync(identityUser);

            var token = _createToken.Create(claims);

            return Ok(token);
        }
    }
}
