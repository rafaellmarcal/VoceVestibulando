using System.Threading.Tasks;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> Login(UserLogin user);
        Task<AuthenticationResponse> Register(UserRegister user);
    }
}
