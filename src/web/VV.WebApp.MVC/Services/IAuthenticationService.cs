using System.Threading.Tasks;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponseLogin> Login(UserLogin user);
        Task<UserResponseLogin> Register(UserRegister user);
    }
}
