using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.CodeDom;

namespace APICatalogo.Repositories
{
    public class ProdutosRepostiry : IProdutosRepository
    {



        private readonly AppDbContext _context;

        public ProdutosRepostiry(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Produto> GetProdutos()
            {
                return _context.Produtos;
            }

            public Produto GetProduto(int id)
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null) { throw new InvalidOperationException("produto é null"); }
                return produto;
            }
            public Produto Create(Produto produto)
            {
                if (produto is null)
                    throw new InvalidOperationException("Produto é null");
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return produto;
            }
            public bool Update(Produto produto)
            {
                if (produto is null)
                    throw new InvalidOperationException("Produto é null");
                if (_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId))
                {
                    _context.Produtos.Update(produto);
                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            public bool Delete(int id)
            {
                var produto = _context.Produtos.Find(id);
                if (produto is not null)
                {
                    _context.Remove(produto);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }

        }
    }
}