using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VV.WebAPI.Core.Configuration;
using VV.WebApp.MVC.Models;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Services
{
    public class CatalogService : BaseService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(
            HttpClient httpClient,
            IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("catalog/products");

            IsValidResponse(response);

            return await DeserializeHttpResponseMessage<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(string id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"catalog/products/{id}");

            IsValidResponse(response);

            return await DeserializeHttpResponseMessage<ProductViewModel>(response);
        }
    }
}
