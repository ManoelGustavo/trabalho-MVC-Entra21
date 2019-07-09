using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.DataBase;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Atualizar(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE categorias SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO categorias (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Categoria categoria = new Categoria();
            categoria.Nome = row["nome"].ToString();
            categoria.Id = Convert.ToInt32(row["id"]);
            return categoria;
        }

        public List<Categoria> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM categorias";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Categoria> categorias = new List<Categoria>();

            foreach (DataRow row in table.Rows)
            {
                Categoria categoria = new Categoria()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["nome"].ToString()
                };

                categorias.Add(categoria);
            }
            return categorias;
        }
    }
}
