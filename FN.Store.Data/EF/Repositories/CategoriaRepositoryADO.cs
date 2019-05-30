using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class CategoriaRepositoryADO 
    {
        private IConfiguration _config;
        private string stringConexao;

        public CategoriaRepositoryADO(IConfiguration config)
        {
            _config = config;
            stringConexao = this._config.GetConnectionString("StoreDataContext");

        }
        public Produto GetById(object id)
        {
            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(CategoriaSQL.GetById, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader _reader = cmd.ExecuteReader();

                Produto produto = new Produto();
                produto.Id = _reader.GetInt32(0);
                produto.Nome = _reader.GetString(1);
                produto.Preco = _reader.GetDecimal(2);
                produto.CategoriaId = _reader.GetInt32(3);
                produto.Categoria.Nome = _reader.GetString(4);

                conn.Close();

                return produto;
            }
        }

        public async Task<Categoria> GetIdNomeAsync(int categoriaId)
        {
            Categoria categoria = null;

            using (SqlConnection conn = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(CategoriaSQL.GetIdNomeAsync, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                conn.Open();

                SqlDataReader _reader = await cmd.ExecuteReaderAsync();

                while (_reader.Read())
                {
                    categoria = new Categoria
                    {
                        Id = _reader.GetInt32(0),
                        Nome = _reader.GetString(1)                        
                    };

                }
                conn.Close();
            }
            return categoria;
        }
    }
}
