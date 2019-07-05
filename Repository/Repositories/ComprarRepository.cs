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
    public class ComprarRepository : IComprarRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM compras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Atualizar(Comprar comprar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE compras SET id_cartao_credito = @ID_CARTAO_CREDITO, valor = @VALOR, data_compra = @DATA_COMPRA WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", comprar.Id);
            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", comprar.IdCartaoCredito);
            comando.Parameters.AddWithValue("@VALOR", comprar.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", comprar.DataCompra);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Comprar comprar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO compras(id_cartao_credito, valor, data_compra) OUTPUT INSERTED.ID VALUES(@ID_CARTAO_CREDITO, @VALOR, @DATA_COMPRA)";
            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", comprar.IdCartaoCredito);
            comando.Parameters.AddWithValue("@VALOR", comprar.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", comprar.DataCompra);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Comprar ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM compras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Comprar comprar = new Comprar();
            comprar.Id = Convert.ToInt32(linha["id"]);
            comprar.IdCartaoCredito = Convert.ToInt32(linha["id_cartao_credito"]);
            comprar.Valor = Convert.ToDecimal(linha["valor"]);
            comprar.DataCompra = Convert.ToDateTime(linha["data_compra"]);
            return comprar;
        }

        public List<Comprar> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT 
cartoes_credito.id AS 'CartaoCreditoId', 
cartoes_credito.numero AS 'CartaoCreditoNumero', 
cartoes_credito.data_vencimento AS 'CartaoCreditoDataVencimento',
cartoes_credito.cvv AS 'CartaoCreditoCvv', 
compras.id AS 'Id', 
compras.valor AS 'Valor', 
compras.data_compra AS 'DataCompra',
clientes.id AS 'ClienteId',
clientes.Nome AS 'ClienteNome',
clientes.Cpf AS 'ClienteCpf'
FROM compras 
INNER JOIN cartoes_credito ON(compras.id_cartao_credito = cartoes_credito.id)
INNER JOIN clientes ON(cartoes_credito.id_cliente = clientes.id)
";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Comprar> compras = new List<Comprar>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Comprar comprar = new Comprar();
                comprar.Id = Convert.ToInt32(linha["Id"]);
                comprar.Valor = Convert.ToDecimal(linha["Valor"]);
                comprar.DataCompra = Convert.ToDateTime(linha["DataCompra"]);

                comprar.CartaoCredito = new CartaoCredito();
                comprar.IdCartaoCredito = Convert.ToInt32(linha["CartaoCreditoId"]);
                comprar.CartaoCredito.Id = Convert.ToInt32(linha["CartaoCreditoId"]);
                comprar.CartaoCredito.Numero = linha["CartaoCreditoNumero"].ToString();
                comprar.CartaoCredito.DataVencimento = Convert.ToDateTime(linha["CartaoCreditoDataVencimento"]);
                comprar.CartaoCredito.Cvv = linha["CartaoCreditoCvv"].ToString();

                comprar.CartaoCredito.Cliente = new Cliente();
                comprar.CartaoCredito.Cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                comprar.CartaoCredito.Cliente.Nome = linha["ClienteNome"].ToString();
                comprar.CartaoCredito.Cliente.Cpf = linha["ClienteCpf"].ToString();
                compras.Add(comprar);
            }
            return compras;
        }
    }
}
