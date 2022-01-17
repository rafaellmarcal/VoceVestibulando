using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VV.Autenticacao.API.Models;

namespace VV.Autenticacao.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(
            IOptions<AppSettings> appSettings,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var loginAttempt = await _userManager.CreateAsync(user, usuario.Senha);

            if (loginAttempt.Succeeded)
                return CustomResponse(await CreateJwt(usuario.Email));

            foreach (IdentityError error in loginAttempt.Errors)
                AddErrors(error.Description);

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLogin usuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var loginAttempt = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);

            if (loginAttempt.Succeeded) return CustomResponse(await CreateJwt(usuario.Email));

            if (loginAttempt.IsLockedOut)
                AddErrors("Usuário bloqueado por tentativas inválidas!");
            else
                AddErrors("Usuário/Senha inválido(s)!");


            return CustomResponse();
        }

        private async Task<UsuarioRespostaLogin> CreateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            var identityClaim = new ClaimsIdentity();
            identityClaim.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaim,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UsuarioRespostaLogin()
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
