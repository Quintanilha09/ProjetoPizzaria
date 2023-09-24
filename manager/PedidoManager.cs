using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzaria.models;

namespace Pizzaria.manager
{
    public class PedidoManager
    {
        private List<Pedido> pedidos = new List<Pedido>();
        private int proximoNumeroPedido = 1;

        public void AdicionarPedido(Pedido pedido)
        {
            pedidos.Add(pedido);
        }

        public List<Pedido> ListarPedidos()
        {
            return pedidos;
        }
        
        public Pedido ObterPedidoPorNumero(int numeroPedido)
        {
            if (numeroPedido >= 1 && numeroPedido <= pedidos.Count)
            {
                return pedidos[numeroPedido - 1];
            }
            return null;
        }

        public int ObterProximoNumeroPedido()
    {
        return proximoNumeroPedido++;
    }
    }
}