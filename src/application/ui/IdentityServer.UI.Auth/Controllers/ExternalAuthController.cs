using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Infrastructure.Models;
using IdentityServer.UI.Auth.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityServer.UI.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.UI.Auth.Controllers
{
    public class ExternalAuthController : Controller
    {
        private readonly ILogger<ExternalAuthController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public ExternalAuthController(ILogger<ExternalAuthController> logger, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "ExternalAuth", new {RedirectUrl = returnUrl});
            var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(props, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string redirectUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info is null) return RedirectToAction("Login", "Auth");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded) return Redirect(redirectUrl);

            var username = info.Principal.FindFirstValue(ClaimTypes.Name);
            return View("ExternalRegister", new ExternalRegisterDto(){Username = username, RedirectUrl = redirectUrl});
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterDto dto)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info is null) return RedirectToAction("Login", "Auth");
            
            var user = new User(){UserName = dto.Username};
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded) return View(dto);
            result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded) return View(dto);

            await _signInManager.SignInAsync(user, false);
            return Redirect(dto.RedirectUrl);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
