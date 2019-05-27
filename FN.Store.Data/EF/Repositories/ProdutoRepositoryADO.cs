using FN.Store.Domain.Entities;
using FN.Store.Domain.ViewModels.Produtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class ProdutoRepositoryADO
    {
        protected IConfiguration _config;
        private string stringConexao;

        public ProdutoRepositoryADO(IConfiguration config)
        {
            _config = config;
            stringConexao = this._config.GetConnectionString("StoreDataContext");
        }

        public void Add(Produto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Produto entity)
        {
            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM PRODUTO WHERE ID = @Id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();                
                conn.Close();
            }
        }

        public async Task<List<ProdutoModel>> GetAllAsync()
        {
            List<ProdutoModel> lstProduto = new List<ProdutoModel>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Produto", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync();

                while (_reader.Read())
                {
                    ProdutoModel produto = new ProdutoModel()
                    {
                        Id = Convert.ToInt32(_reader["Id"]),
                        Nome = _reader["Nome"].ToString(),
                        Preco = Convert.ToDecimal(_reader["Preco"]),
                        CategoriaNome = _reader["CatNome"].ToString(),
                        CategoriaId = Convert.ToInt32(_reader["CategoriaId"])
                    };

                    lstProduto.Add(produto);
                }
                con.Close();
            }

            return lstProduto;
        }

        public  List<ProdutoModel> GetByIdWithCategoryAsync(int id)
        {
            List<ProdutoModel> listaProdutos = new List<ProdutoModel>();

            string sql = @"SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome as CatNome FROM Produto AS P JOIN Categoria AS C ON P.CategoriaId = C.Id where p.id = @id";

            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader _reader =  cmd.ExecuteReader();
                while (_reader.Read())
                {
                    ProdutoModel produto = new ProdutoModel()
                    {
                        Id = Convert.ToInt32(_reader["Id"]),
                        Nome = _reader["Nome"].ToString(),
                        Preco = Convert.ToDecimal(_reader["Preco"]),
                        CategoriaNome = _reader["CatNome"].ToString(),
                        CategoriaId = Convert.ToInt32(_reader["CategoriaId"])
                    };

                    listaProdutos.Add(produto);
                }
                conn.Close();
                return listaProdutos;
            }
        }

        public async Task<IEnumerable<ProdutoModel>> GetAllWithCategoryAsync()
        {
            List<ProdutoModel> listaProduto = new List<ProdutoModel>();

            string sqlCategoria = @"SELECT p.id, p.Nome, p.Preco, p.CategoriaId, c.nome as CatNome FROM Produto AS P JOIN Categoria AS C ON P.CategoriaId = C.Id";

            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(sqlCategoria, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync();

                while (_reader.Read())
                {
                    ProdutoModel produto = new ProdutoModel()
                    {
                        Id = Convert.ToInt32(_reader["Id"]),
                        Nome = _reader["Nome"].ToString(),
                        Preco = Convert.ToDecimal(_reader["Preco"]),
                        CategoriaNome = _reader["CatNome"].ToString(),
                        CategoriaId = Convert.ToInt32(_reader["CategoriaId"])
                    };

                    listaProduto.Add(produto);
                }
                conn.Close();

            }

            return listaProduto;
        }

        public Task<Produto> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Produto>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Produto entity)
        {
            throw new NotImplementedException();
        }

    }
}
