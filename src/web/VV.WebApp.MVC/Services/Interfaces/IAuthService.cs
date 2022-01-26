using System.Threading.Tasks;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin user);
        Task<UserResponseLogin> Register(UserRegister user);
    }
}
