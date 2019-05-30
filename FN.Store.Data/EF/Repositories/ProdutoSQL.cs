using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public static class ProdutoSQL
    {
        public static string GetById = string.Format(@"
            SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome 
            FROM Produto AS P 
            JOIN Categoria AS C 
            ON P.CategoriaId = C.Id 
            where p.id = @id
        ");
        public static string GetAllWithCategoryAsync = string.Format(@"
            SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome 
            FROM Produto AS P 
            JOIN Categoria AS C 
            ON P.CategoriaId = C.Id 
        ");
        public static string Update = string.Format(@"
            UPDATE PRODUTO 
            SET NOME = @nome, PRECO = @preco, CATEGORIAID = @categoriaId 
            where Id =  @id;
        ");

        public static string GetByName = string.Format(@"
            SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome 
            FROM Produto AS P 
            JOIN Categoria AS C 
            ON P.CategoriaId = C.Id 
            where p.nome LIKE '%@nome%'
        ");
        public static string GetByIdWithCategoryAsync = string.Format(@"
            SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome 
            FROM Produto AS P 
            JOIN Categoria AS C 
            ON P.CategoriaId = C.Id where p.id = @id
         ");
        public static string Add = string.Format(@"
            INSERT INTO PRODUTO (NOME, PRECO, DATACRIACAO, DATAALTERACAO,  CATEGORIAID) 
            VALUES (@nome, @preco, @dataCriacao, @dataAlteracao, @categoriaId);
         ");
        public static string Delete = string.Format(@"DELETE FROM PRODUTO WHERE ID = @Id");
        public static string GetAllAsync = string.Format(@"SELECT * from Produto");


    }
}
