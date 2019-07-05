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
    public class CartaoCreditoRepository : ICartaoCreditoRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Atualizar(CartaoCredito cartaoCredito)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE cartoes_credito SET
numero = @NUMERO,
data_vencimento = @DATA_VENCIMENTO,
cvv = @CVV,
id_cliente = @ID_CLIENTE
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NUMERO", cartaoCredito.Numero);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", cartaoCredito.DataVencimento);
            comando.Parameters.AddWithValue("@CVV", cartaoCredito.Cvv);
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);
            comando.Parameters.AddWithValue("@ID", cartaoCredito.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(CartaoCredito cartaoCredito)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO cartoes_credito
(numero, data_vencimento, cvv, id_cliente)
OUTPUT INSERTED.ID
VALUES (@NUMERO, @DATA_VENCIMENTO, @CVV, @ID_CLIENTE)";
            comando.Parameters.AddWithValue("@NUMERO", cartaoCredito.Numero);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", cartaoCredito.DataVencimento);
            comando.Parameters.AddWithValue("@CVV", cartaoCredito.Cvv);
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public CartaoCredito ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if(table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.Numero = row["numero"].ToString();
            cartaoCredito.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
            cartaoCredito.Cvv = row["cvv"].ToString();
            cartaoCredito.IdCliente = Convert.ToInt32(row["id_cliente"]);
            cartaoCredito.Id = Convert.ToInt32(row["id"]);

            return cartaoCredito;                       
        }

        public List<CartaoCredito> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
cartoes_credito.id AS 'Id',
cartoes_credito.numero AS 'Numero',
cartoes_credito.data_vencimento AS 'DataVencimento',
cartoes_credito.cvv AS 'Cvv'
FROM cartoes_credito
INNER JOIN clientes ON (cartoes_credito.id_cliente = clientes.id)";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<CartaoCredito> cartoesCredito = new List<CartaoCredito>();

            foreach(DataRow row in table.Rows)
            {
                CartaoCredito cartaoCredito = new CartaoCredito();
                cartaoCredito.Id = Convert.ToInt32(row["Id"]);
                cartaoCredito.Numero = row["Numero"].ToString();
                cartaoCredito.DataVencimento = Convert.ToDateTime(row["DataVencimento"]);
                cartaoCredito.Cvv = row["Cvv"].ToString();
                cartaoCredito.IdCliente = Convert.ToInt32(row["ClienteId"]);

                cartaoCredito.Cliente = new Cliente();
                cartaoCredito.Cliente.Id = Convert.ToInt32(row["ClienteId"]);
                cartaoCredito.Cliente.Nome = row["ClienteNome"].ToString();

                cartoesCredito.Add(cartaoCredito);
            }
            return cartoesCredito;
        }
    }
}
