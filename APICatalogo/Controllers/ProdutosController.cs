using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepository _produtoRepository;
        private readonly IRepository<Produto> _repository;
        public ProdutosController(IRepository<Produto> repository, IProdutosRepository produtoRepository)
        { 
            _produtoRepository = produtoRepository;
            _repository = repository;

        }
        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetAll();   
            if (produtos is null) { return NotFound("Produtos Não Encontrados..."); }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.Get(p => p.ProdutoId == id);
            if (produto is null)
           {
                return NotFound("Produto não Encontrado");
           }
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)

                return BadRequest();


            var novoProduto = _repository.Create(produto);
            return new CreatedAtRouteResult("ObterProduto", new { Id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _repository.Update(produto);
           return Ok(produtoAtualizado);

            
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
         var produto = _repository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                return Ok("Produto não encontrado...");
            }
           var produtoDeletado = _repository.Delete(produto); 
            return Ok(produtoDeletado);
        }



    }
}
