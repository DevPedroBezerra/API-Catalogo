using APICatalogo.Context;
using APICatalogo.Controllers;
using APICatalogo.Repository;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutosRepository  _produtoRepo;
        private ICategoriaRepository _categoriaRepo;
        public AppDbContext _context;
        public UnitOfWork (AppDbContext context)
        {
            _context = context;
        }
        public IProdutosRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutosRepository(_context);
            }
        }
        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
            
    }
}
