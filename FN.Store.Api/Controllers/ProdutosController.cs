using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using FN.Store.Domain.ViewModels.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepositoryADO _produtoRepository;
        private readonly CategoriaRepositoryADO _categoriaRepository;

        public ProdutosController(ProdutoRepositoryADO produtoRepository, CategoriaRepositoryADO categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {

            var dados = (await _produtoRepository.GetAllWithCategoryAsync()).Select(produto => produto.ParaProdutosModel());
            

            return Ok(dados);
        }
        [HttpGet("{id}", Name = "GetProdutoById")]
        public async Task<IActionResult> GetById(int id)
        {
            var produtoModel = (await _produtoRepository.GetByIdWithCategoryAsync(id))
                .Select(produto => produto.ParaProdutosModel());

            if (produtoModel == null) return NotFound();

            return Ok(produtoModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProdutoAddEdit model)
        {
            var categoria = await _categoriaRepository.GetIdNomeAsync(model.CategoriaId);

            if (categoria == null) ModelState.AddModelError("CategoriaId", "Categoria não existe!");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = model.ParaProduto();
            _produtoRepository.Add(data);

            var produto = data.ParaProdutosModel();
            produto.CategoriaNome = categoria.Nome;

            return CreatedAtRoute("GetProdutoById", new { produto.Id }, produto);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Produto model)
        {
            var produto = _produtoRepository.GetByIdAsync(id);

            if (produto == null) ModelState.AddModelError("Id", "Produto não Cadastrado!");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            model.Id = id;

            _produtoRepository.Update(model);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> GetByIdelete(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null) return BadRequest(new { Produto = new string[] { "Produto não encontrado!" } });
            _produtoRepository.Delete(produto);

            return Ok();
        }
    }
}
