using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.CodeDom;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class ProdutosRepository : Repository<Produto>, IProdutosRepository
    { 
        private readonly AppDbContext _context;

        public ProdutosRepository(AppDbContext context) : base(context)
        {
        }
        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaID == id);
        }
    }
}