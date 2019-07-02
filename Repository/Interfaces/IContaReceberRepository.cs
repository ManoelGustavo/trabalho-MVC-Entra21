using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    interface IContaReceberRepository
    {
        int Inserir(ContaReceber contaReceber);
        bool Atualizar(ContaReceber contaReceber);
        bool Apagar(int id);
        List<ContaReceber> ObterTodos(string pesquisa);
        ContaReceber ObterPeloId(int id);
    }
}
