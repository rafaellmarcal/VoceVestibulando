using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VV.Catalogo.API.Data;
using VV.Catalogo.API.Models;

namespace VV.Catalogo.API.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await _produtoRepository.GetAll();
        }

        [HttpGet("products/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.GetById(id);
        }
    }
}
