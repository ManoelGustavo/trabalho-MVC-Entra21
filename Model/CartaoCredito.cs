using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartaoCredito
    {
        public int Id;

        public string Numero;

        public DateTime DataVencimento;

        public string Cvv;

        public int IdCliente;

        public Cliente Cliente;
    }
}
