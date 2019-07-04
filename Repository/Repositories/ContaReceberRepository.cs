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
    public class ContaReceberRepository : IContaReceberRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contas_receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Atualizar(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_receber SET id_cliente = @ID_CLIENTE,id_categoria = @ID_CARTEGORIA,nome = @NOME,data_pagamento = @DATA_PAGAMENTO,valor = @VALOR WHERE id=@ID";
            comando.Parameters.AddWithValue("@ID", contaReceber.Id);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;

        }

        public int Inserir(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_receber(id_cliente, id_categoria, nome, data_pagamento, valor) OUTPUT INSERTED.ID VALUES(@ID_CLIENTE, @ID_CATEGORIA, @NOME, @DATA_PAGAMENTO, @VALOR)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contas_receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(linha["id"]);
            contaReceber.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            contaReceber.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            contaReceber.Nome = linha["nome"].ToString();
            contaReceber.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
            return contaReceber;
        }

        public List<ContaReceber> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT clientes.id AS 'ClienteId', 
clientes.nome AS 'ClienteNome', 
clientes.cpf AS 'ClienteCpf', 
categorias.id AS 'CategoriaId',
categorias.nome AS 'CategoriaNome', 
contas_receber.id AS 'Id', 
contas_receber.nome AS 'Nome', 
contas_receber.data_pagamento AS 'DataPagamento',
contas_receber.valor AS 'Valor' 
FROM contas_receber 
INNER JOIN clientes ON(contas_receber.id_cliente = clientes.id) 
INNER JOIN categorias ON(contas_receber.id_categoria = categorias.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaReceber> contasReceber = new List<ContaReceber>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaReceber contaReceber = new ContaReceber();
                contaReceber.Id = Convert.ToInt32(linha["id"]);
                contaReceber.Nome = linha["nome"].ToString();
                contaReceber.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
                contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
                contaReceber.IdCliente = Convert.ToInt32(linha["ClienteId"]);
                contaReceber.Cliente = new Cliente();
                contaReceber.Cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                contaReceber.Cliente.Nome = linha["ClienteNome"].ToString();
                contaReceber.Cliente.Cpf = linha["ClienteCpf"].ToString();
                contaReceber.IdCategoria = Convert.ToInt32(linha["CategoriaId"]);
                contaReceber.Categoria = new Categoria();
                contaReceber.Categoria.Id = Convert.ToInt32(linha["CategoriaId"]);
                contaReceber.Categoria.Nome = linha["CategoriaNome"].ToString();
                contasReceber.Add(contaReceber);
            }
            return contasReceber;
        }
    }
}
