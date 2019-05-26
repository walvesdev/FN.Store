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

        public ProdutosController(ProdutoRepositoryEF produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dados = (await _produtoRepository.GetAllWithCategoryAsync())
                .Select(produto => produto.ParaProdutosGet());

            
            return  Ok(dados);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoRepository.GetByIdWithCategoryAsync(id);

            if(produto == null)  return NotFound();

            return Ok(produto?.ParaProdutosGet());
        }
    }
}
