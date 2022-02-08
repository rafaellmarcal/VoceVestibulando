using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Controllers
{
    public class CatalogController : BaseWebAppController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAll());
        }

        [HttpGet]
        [Route("catalog/product/{id}")]
        public async Task<IActionResult> ProductDetail(string id)
        {
            return View(await _catalogService.GetById(id));
        }
    }
}
