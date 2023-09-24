using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzaria.models;

namespace Pizzaria.manager
{
    public class PizzaManager
    {
        private List<Pizza> pizzas = new List<Pizza>();

        public void AdicionarPizza(Pizza pizza)
        {
            pizzas.Add(pizza);
        }

        public List<Pizza> ListarPizzas()
        {
            return pizzas;
        }

         public Pizza ObterPizzaPorNome(string nome)
        {
            return pizzas.FirstOrDefault(pizza => pizza.Nome.ToUpper() == nome.ToUpper());
        }
    }
}