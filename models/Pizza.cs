using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.models
{
    public class Pizza
    {  
        public string Nome { get; set; }
        public List<string> Ingredientes { get; set; }
        public float Preco { get; set; }

    }
}