using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.models
{
    public class Pedido
    {
        public int Numero { get; set; }
        public string ClienteNome { get; set; }
        public string TelefoneCliente { get; set; }
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public float Total { get; set; } 
        public float ValorPago { get; set; } 
        public List<string> FormasDePagamento { get; set; } = new List<string>(); // Formas de pagamento
        public bool Pago { get; set; } 

    }
}