using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        public bool Alterar(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contabilidades SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            comando.Parameters.AddWithValue("@ID", contabilidade.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO contabilidades (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Contabilidade ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Nome = row["nome"].ToString();
            contabilidade.Id = Convert.ToInt32(row["id"]);
            return contabilidade;
        }

        public List<Contabilidade> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contabilidades";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Contabilidade> contabilidades = new List<Contabilidade>();

            foreach(DataRow row in table.Rows)
            {
                Contabilidade contabilidade = new Contabilidade()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["nome"].ToString()
                };

                contabilidades.Add(contabilidade);
            }
            return contabilidades;
        }
    }
}
