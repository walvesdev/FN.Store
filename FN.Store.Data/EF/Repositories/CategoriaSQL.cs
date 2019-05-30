using System;
using System.Collections.Generic;
using System.Text;

namespace FN.Store.Data.EF.Repositories
{
    public static class CategoriaSQL
    {
        public static string GetById = string.Format(@"
            SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome 
            FROM Produto AS P 
            JOIN Categoria AS C 
            ON P.CategoriaId = C.Id 
            where p.id = @id
         ");
        public static string GetIdNomeAsync = string.Format(@"
            SELECT ID, NOME 
            FROM CATEGORIA 
            WHERE ID = @categoriaId
         ");
    }
}
