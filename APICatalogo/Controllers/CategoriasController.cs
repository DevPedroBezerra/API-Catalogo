using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger _logger;

        public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _repository = repository;  
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        { 
           var categorias = _repository.GetCategorias();
           return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult Get(int id)
        {

            var categoria = _repository.GetCategorias(id);
                if (categoria is null)
                 { return NotFound(); }
                 return Ok(categoria);
           

        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null) 
            {
             _logger.LogWarning($"Dados Invalidos...");
            return BadRequest(); 
            }
           var categoriaCriada = _repository.Create(categoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId) 
            {
                _logger.LogWarning($"Dados Invalidos...");
                return BadRequest();
            }
            _repository.Update(categoria);
            return Ok(categoria);

        }

        [HttpDelete ("{id}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetCategorias (id);
            if (categoria is null)
            {
                return NotFound();
            }
            var categoriaExcluida = _repository.Delete(id);
            return Ok(categoriaExcluida);

        }
    }
}
