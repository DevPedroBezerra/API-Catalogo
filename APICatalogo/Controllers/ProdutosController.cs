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
        private readonly IProdutosRepository _repository;
        public ProdutosController(IProdutosRepository repository)
        { _repository = repository; }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetProdutos();   
            if (produtos is null) { return NotFound("Produtos Não Encontrados..."); }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
           { return NotFound("Produto não Encontrado"); }
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

            bool atualizado = _repository.Update(produto);
            if (atualizado)
            {
                return Ok(produto);
            }
            else
            {
                return StatusCode(500, $"produto não encontrado");
            }

            
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
          bool deletado = _repository.Delete(id);
            if (deletado)
            {
                return Ok("Produto deletado");
            }
            else
            {
                return StatusCode(500, $"produto não encontrado");
            }
        }



    }
}
