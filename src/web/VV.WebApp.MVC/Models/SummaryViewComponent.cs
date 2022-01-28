using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VV.WebApp.MVC.Models
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
