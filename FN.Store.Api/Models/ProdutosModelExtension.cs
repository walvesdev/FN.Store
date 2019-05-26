using FN.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api.Models
{
    public static class ProdutosModelExtension
    {
        public static ProdutosGet ParaProdutosGet(this Produto entity)
        {
            return new ProdutosGet
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Preco = entity.Preco,
                CategoriaId = entity.CategoriaId,
                CategoriaNome = entity.Categoria?.Nome
            };
        }

        public static Produto ParaProduto(this ProdutoAddEdit model)
        {
            return new Produto
            {
                Nome = model.Nome,
                Preco = model.Preco,
                CategoriaId = model.CategoriaId
            };
        }
    }
}
