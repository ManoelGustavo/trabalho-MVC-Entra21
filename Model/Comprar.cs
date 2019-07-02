using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Comprar
    {
        public int Id;
        public int IdCartaoCredito;
        public CartaoCredito CartaoCredito;
        public decimal Valor;
        public DateTime DataCompra;
    }
}
