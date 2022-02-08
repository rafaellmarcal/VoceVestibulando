using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Controllers
{
    public class BaseWebAppController : Controller
    {
        protected bool IsValid(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Notifications.Any())
            {
                foreach (var mensagem in resposta.Errors.Notifications)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}
