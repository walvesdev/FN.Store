using FN.Store.Api.Models;
using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutosController(ProdutoRepositoryEF produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dados = (await _produtoRepository.GetAllWithCategoryAsync())
                .Select(produto => produto.ParaProdutosGet());

            
            return  Ok(dados);
        }
        [HttpGet("{id}", Name = "GetProdutoById")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoRepository.GetByIdWithCategoryAsync(id);

            if(produto == null)  return NotFound();

            return Ok(produto?.ParaProdutosGet());
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProdutoAddEdit model)
        {
            var categoria = await _categoriaRepository.GetAsync(model.CategoriaId);
            if (categoria == null) ModelState.AddModelError("CategoriaId", "Categoria não existe!");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data =  model.ParaProduto();
            _produtoRepository.Add(data);

            var produto = data.ParaProdutosGet();
            produto.CategoriaNome = categoria.Nome;

            return CreatedAtRoute("GetProdutoById", new { produto.Id }, produto); 
        }
    }
}
