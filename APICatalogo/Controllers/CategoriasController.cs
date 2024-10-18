using APICatalogo.Context;
using APICatalogo.DTO;
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
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;

        public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _uof = uof;
       
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        { 
           var categorias = _uof.CategoriaRepository.GetAll();
           return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<CategoriaDTO> Get(int id)
        {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
                if (categoria is null)
                 { return NotFound(); }
            var categoriaDto = new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };
                 return Ok(categoriaDto);
           

        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null) 
            {
             _logger.LogWarning($"Dados Invalidos...");
            return BadRequest(); 
            }
            var categoria = new Categoria()
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };
           var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            var novaCategoriaDto = new CategoriaDTO()
            {
                CategoriaId = categoriaCriada.CategoriaId,
                Nome = categoriaCriada.Nome,
                ImagemUrl = categoriaCriada.ImagemUrl
            };
            _uof.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);

        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId) 
            {
                _logger.LogWarning($"Dados Invalidos...");
                return BadRequest();
            }
            var categoria = new Categoria()
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };
            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            var categoriaAtualizadaDto = new CategoriaDTO()
            {
                CategoriaId = categoriaAtualizada.CategoriaId,
                Nome = categoriaAtualizada.Nome,
                ImagemUrl = categoriaAtualizada.ImagemUrl
            };
            
            return Ok(categoriaAtualizadaDto);

        }

        [HttpDelete ("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get (c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound();
            }
            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            var categoriaExcluidaDto = new CategoriaDTO()
            {
                CategoriaId = categoriaExcluida.CategoriaId,
                Nome = categoriaExcluida.Nome,
                ImagemUrl = categoriaExcluida.ImagemUrl
            };
            return Ok(categoriaExcluida);

        }
    }
}
