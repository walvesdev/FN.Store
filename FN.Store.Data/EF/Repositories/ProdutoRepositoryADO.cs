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
        Produto produto;
        public ProdutoRepositoryADO(IConfiguration config)
        {
            _config = config;
            stringConexao = this._config.GetConnectionString("StoreDataContext");
        }

        public void Add(Produto entity)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(ProdutoSQL.Add, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", entity.Nome);
                cmd.Parameters.AddWithValue("@preco", entity.Preco);
                cmd.Parameters.AddWithValue("@dataCriacao", DateTime.Now);
                cmd.Parameters.AddWithValue("@dataAlteracao", DateTime.Now);
                cmd.Parameters.AddWithValue("@categoriaId", entity.CategoriaId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void Delete(Produto entity)
        {
            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(ProdutoSQL.Delete, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            List<Produto> listaProdutos = new List<Produto>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(ProdutoSQL.GetAllAsync, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (_reader.Read())
                {
                    Produto produto = new Produto();

                    produto.Id = _reader.GetInt32(0);
                    produto.Nome = _reader.GetString(1);
                    produto.Preco = _reader.GetDecimal(2);
                    produto.CategoriaId = _reader.GetInt32(3);
                    produto.Categoria.Nome = _reader.GetString(4);

                    listaProdutos.Add(produto);
                }
                con.Close();
            }

            return listaProdutos;
        }

        public async Task<List<Produto>> GetByIdWithCategoryAsync(int id)
        {

            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                var listaProdutos = new List<Produto>();

                SqlCommand cmd = new SqlCommand(ProdutoSQL.GetByIdWithCategoryAsync, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (_reader.Read())
                {
                    this.produto = new Produto()
                    {
                        Id = _reader.GetInt32(0),
                        Nome = _reader.GetString(1),
                        Preco = _reader.GetDecimal(2),
                        CategoriaId = _reader.GetInt32(3),
                        Categoria = new Categoria() { Nome = _reader.GetString(4) }
                    };
                    listaProdutos.Add(produto);
                }
                conn.Close();

                return listaProdutos;
            }
        }

        public async Task<IList<Produto>> GetAllWithCategoryAsync()
        {
            List<Produto> listaProduto = new List<Produto>();

            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(ProdutoSQL.GetAllWithCategoryAsync, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (_reader.Read())
                {
                    this.produto = new Produto()
                    {
                        Id = _reader.GetInt32(0),
                        Nome = _reader.GetString(1),
                        Preco = _reader.GetDecimal(2),
                        CategoriaId = _reader.GetInt32(3),
                        Categoria = new Categoria() { Nome = _reader.GetString(4) }
                    };
                    listaProduto.Add(produto);
                }
                conn.Close();

            }

            return listaProduto;
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(stringConexao))
            {

                SqlCommand cmd = new SqlCommand(ProdutoSQL.GetByIdWithCategoryAsync, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader _reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (_reader.Read())
                {
                    this.produto = new Produto()
                    {
                        Id = _reader.GetInt32(0),
                        Nome = _reader.GetString(1),
                        Preco = _reader.GetDecimal(2),
                        CategoriaId = _reader.GetInt32(3),
                        Categoria = new Categoria() { Nome = _reader.GetString(4) }
                    };
                }
                conn.Close();

                return produto;
            }
        }
        public async Task<IEnumerable<Produto>> GetByName(string nome)
        {
            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                List<Produto> listaProduto = new List<Produto>();

                SqlCommand cmd = new SqlCommand(ProdutoSQL.GetByName, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", nome);
                conn.Open();

                using (var _reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {

                    while (_reader.Read())
                    {
                        this.produto = new Produto()
                        {
                            Id = _reader.GetInt32(0),
                            Nome = _reader.GetString(1),
                            Preco = _reader.GetDecimal(2),
                            CategoriaId = _reader.GetInt32(3),
                            Categoria = new Categoria() { Nome = _reader.GetString(4) }
                        };
                        listaProduto.Add(produto);
                    }

                    conn.Close();
                }
                return listaProduto;
            }
        }

        public void Update(Produto entity)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    SqlCommand cmd = new SqlCommand(ProdutoSQL.Update, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", entity.Id);
                    cmd.Parameters.AddWithValue("@nome", entity.Nome);
                    cmd.Parameters.AddWithValue("@preco", entity.Preco);
                    cmd.Parameters.AddWithValue("@categoriaId", entity.CategoriaId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception)
            {


            }
        }

    }
}
