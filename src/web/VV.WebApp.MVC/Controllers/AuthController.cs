using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (!ModelState.IsValid) return View(user);

            AuthenticationResponse response = await _authenticationService.Register(user);

            await ExecuteLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(user);

            AuthenticationResponse response = await _authenticationService.Login(user);

            await ExecuteLogin(response);

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);

        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task ExecuteLogin(AuthenticationResponse respose)
        {
            JwtSecurityToken token = FormatedToken(respose.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("Jwt", respose.AccessToken));
            claims.AddRange(token.Claims);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken FormatedToken(string token)
            => new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
    }
}
