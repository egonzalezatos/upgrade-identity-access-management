using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DTO.Login;
using IdentityServer.I18N.ErrorMessages;
using IdentityServer.Infrastructure.Models;
using IdentityServer.Options.Login;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityServer.API.Controllers
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    //[SecurityHeaders]
    [Route("[controller]")]
    public class AuthEndpointController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly SignInManager<User> _signInManager;

        public AuthEndpointController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            SignInManager<User> signInManager)
        {

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            dto.TryValidate();
            var user = await _signInManager.UserManager.FindByNameAsync(dto.Username);
            if (user is null)
                return Unauthorized(LoginErrorMessages.USER_NOT_FOUND());

            var userLogin = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            if (userLogin != SignInResult.Success)
                return Unauthorized(LoginErrorMessages.INVALID_CREDENTIALS());

            AuthenticationProperties props = null;
            if (LoginOptions.AllowRememberLogin && dto.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(LoginOptions.RememberMeLoginDuration)
                };
            }

            // issue authentication cookie with subject ID and username
            var isuser = new IdentityServerUser(user.Id.ToString())
            {
                DisplayName = user.UserName
            };
            await HttpContext.SignInAsync(isuser, props);
            return Ok();
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost("logout")]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // build a model so the logged out page knows what to display
            if (User?.Identity?.IsAuthenticated is true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
                await _signInManager.SignOutAsync();
                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return Ok("LoggedOut");
        }

        [HttpGet("auth")]
        public async Task<IActionResult> Authenticate()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some.id"),
                new Claim("granny", "cookie")
            };

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my long secret key haha"));
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(0.5),
                new SigningCredentials(secret, SecurityAlgorithms.HmacSha256));
            var handler = new JwtSecurityTokenHandler();
            return Ok(handler.WriteToken(token));
        }
        
        [HttpGet("check")]
        [Authorize]
        public Task<IActionResult> Check()
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}