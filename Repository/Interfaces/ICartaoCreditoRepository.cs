using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface ICartaoCreditoRepository
    {
        int Inserir(CartaoCredito cartaoCredito);

        bool Atualizar(CartaoCredito cartaoCredito);

        bool Apagar(int id);

        List<CartaoCredito> ObterTodos(string pesquisa);

        CartaoCredito ObterPeloId(int id);
    }
}
