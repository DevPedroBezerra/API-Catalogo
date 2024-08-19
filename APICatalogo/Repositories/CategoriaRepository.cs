using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;


namespace APICatalogo.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Categoria> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();
            if (categorias is null) { throw new InvalidOperationException("Categoria é null");}

            return categorias;
        }

        public Categoria GetCategorias(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null) { throw new InvalidOperationException("Categoria é null"); }
            return categoria;
        }
        public Categoria Create([FromBody]Categoria categoria)
        {
            if(categoria is null) { throw new ArgumentNullException(nameof(categoria)); }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }
        public Categoria Update([FromBody] Categoria categoria)
        {
            
            if (categoria is null) { throw new ArgumentNullException(nameof(categoria)); }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return categoria;
        }
        public Categoria Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria is null) { throw new ArgumentNullException(nameof(id)); }
               _context.Remove(categoria);
            _context.SaveChanges();
            return categoria;
            
              
        }

    }

      
    }
}
