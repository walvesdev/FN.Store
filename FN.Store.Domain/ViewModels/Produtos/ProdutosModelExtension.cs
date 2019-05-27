
using FN.Store.Domain.Entities;

namespace FN.Store.Domain.ViewModels.Produtos
{
    public static class ProdutosModelExtension
    {
        public static ProdutoModel ParaProdutosGet(this Produto entity)
        {
            return new ProdutoModel
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
