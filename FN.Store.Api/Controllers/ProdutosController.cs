using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.ViewModels.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepositoryADO _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutosController(ProdutoRepositoryADO produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dados = await _produtoRepository.GetAllWithCategoryAsync();
                //.Select(produto => produto.ParaProdutosGet());


            return Ok(dados);
        }
        [HttpGet("{id}", Name = "GetProdutoById")]
        public IActionResult GetById(int id)
        {
            var produto =  _produtoRepository.GetByIdWithCategoryAsync(id);

            if (produto == null) return NotFound();

            return Ok(produto);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProdutoAddEdit model)
        {
            var categoria = await _categoriaRepository.GetAsync(model.CategoriaId);

            if (categoria == null) ModelState.AddModelError("CategoriaId", "Categoria não existe!");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = model.ParaProduto();
            _produtoRepository.Add(data);

            var produto = data.ParaProdutosGet();
            produto.CategoriaNome = categoria.Nome;

            return CreatedAtRoute("GetProdutoById", new { produto.Id }, produto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]ProdutoAddEdit model)
        {
            var categoria = await _categoriaRepository.GetAsync(model.CategoriaId);
            if (categoria == null) ModelState.AddModelError("CategoriaId", "Categoria não existe!");
            
            var produto = await _produtoRepository.GetAsync(id);

            if (produto == null) ModelState.AddModelError("Id","Produto não Cadastrado!");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            produto.UpdateProduto(model.Nome, model.Preco, model.CategoriaId);
            _produtoRepository.Update(produto);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> GetByIdelete(int id)
        {
            var produto = await _produtoRepository.GetAsync(id);

            if (produto == null) return BadRequest(new { Produto = new string[] { "Produto não encontrado!" } });
            _produtoRepository.Delete(produto);

            return Ok();
        }
    }
}
