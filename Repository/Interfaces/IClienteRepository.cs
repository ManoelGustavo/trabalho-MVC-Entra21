using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IClienteRepository
    {
        int Inserir(Cliente cliente);

        bool Apagar(int id);

        bool Atualizar(Cliente cliente);

        List<Cliente> ObterTodos(string pesquisa);

        Cliente ObterPeloId(int id);
    }
}
