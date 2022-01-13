using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VV.Autenticacao.API.Models;

namespace VV.Autenticacao.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser()
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var loginAttempt = await _userManager.CreateAsync(user, usuario.Senha);

            if (loginAttempt.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLogin usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            var loginAttempt = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);

            if (loginAttempt.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
