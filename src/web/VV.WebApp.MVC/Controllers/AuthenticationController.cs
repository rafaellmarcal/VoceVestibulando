using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models;
using VV.WebApp.MVC.Services;

namespace VV.WebApp.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
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

            UserResponseLogin response = await _authenticationService.Register(user);

            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (!ModelState.IsValid) return View(user);

            UserResponseLogin response = await _authenticationService.Login(user);

            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            return View();
        }

        private async Task ProcessLogin(UserResponseLogin respose)
        {
            JwtSecurityToken token = FormatedToken(respose.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", respose.AccessToken));
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
