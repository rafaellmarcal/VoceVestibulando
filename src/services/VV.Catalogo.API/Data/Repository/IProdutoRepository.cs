using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VV.Catalogo.API.Models;
using VV.Core.Data;

namespace VV.Catalogo.API.Data
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);

        void Add(Produto produto);
        void Update(Produto produto);
    }
}
