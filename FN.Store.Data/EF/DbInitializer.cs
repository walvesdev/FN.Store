using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF
{
    public class DbInitializer
    {
        private readonly ProdutoRepositoryEF _produtoRepository;
        

        public DbInitializer(ProdutoRepositoryEF produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public void PreencherBanco()
        {
            var Alimento = new Categoria { Nome = "Alimento", Descricao = "Alimento" };
            var Papelaria = new Categoria { Nome = "Papelaria", Descricao = "Papelaria" };
            var Carro = new Categoria { Nome = "Carro", Descricao = "Carro" };

            var produtos = new List<Produto>()
            {
                new Produto { Id = 1, Nome = "Picanha", Categoria = Alimento, Preco = 34.9M },
                new Produto { Id = 2, Nome = "Lapis", Categoria = Papelaria, Preco = 3.9M },
                new Produto { Id = 3, Nome = "Caderno", Categoria = Papelaria, Preco = 4.9M },
                new Produto { Id = 4, Nome = "Kadett", Categoria = Carro, Preco = 8034.9M },
                new Produto { Id = 5, Nome = "Fusca", Categoria = Carro, Preco = 2034.9M },
                new Produto { Id = 6, Nome = "Fanta", Categoria = Alimento, Preco = 5.9M }
            };

            foreach (var prod in produtos)
            {
                if (!_produtoRepository._db.Where(p => p.Id == prod.Id).Any())
                {
                    var produto = new Produto
                    {
                        Nome = prod.Nome,
                        Categoria = prod.Categoria,
                        Preco = prod.Preco,
                        DataCriacao = DateTime.Now
                    };

                    _produtoRepository.Add(produto);
                }

            }

        }
    }
}
