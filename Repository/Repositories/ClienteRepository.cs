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
    public class ClienteRepository : IClienteRepository
    {
        public bool Atualizar(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE clientes SET
nome = @NOME,
cpf = @CPF,
id_contabilidade = @ID_CONTABILIDADE
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);
            comando.Parameters.AddWithValue("@ID", cliente.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO clientes
(nome, cpf, id_contabilidade)
OUTPUT INSERTED.ID
VALUES (@NOME, @CPF, @ID_CONTABILIDADE)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Cliente cliente = new Cliente();
            cliente.Nome = row["nome"].ToString();
            cliente.Cpf = row["cpf"].ToString();
            cliente.IdContabilidade = Convert.ToInt32(row["id_contabilidade"]);
            cliente.Id = Convert.ToInt32(row["id"]);

            return cliente;
        }

        public List<Cliente> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT
contabilidades.id AS 'ContabilidadeId',
contabilidades.nome AS 'ContabilidadeNome',
clientes.id AS 'Id',
clientes.nome AS 'Nome',
clientes.cpf AS 'Cpf'
FROM clientes
INNER JOIN contabilidades ON (clientes.id_contabilidade = contabilidades.id)";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow row in table.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = row["nome"].ToString();
                cliente.Cpf = row["cpf"].ToString();
                cliente.Id = Convert.ToInt32(row["id"]);
                cliente.IdContabilidade = Convert.ToInt32(row["id_contabilidade"]);

                cliente.Contabilidade = new Contabilidade();
                cliente.Contabilidade.Id = Convert.ToInt32(row["ContabilidadeId"]);
                cliente.Contabilidade.Nome = row["ContabilidadeNome"].ToString();
                clientes.Add(cliente);
            }
            return clientes;
        }
    }
}
