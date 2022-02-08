using System.Collections.Generic;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ProductViewModel> GetById(string id);
        Task<IEnumerable<ProductViewModel>> GetAll();
    }
}
