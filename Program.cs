using System;
using System.Collections.Generic;
using System.Linq;
using Pizzaria.manager;
using Pizzaria.models;

namespace Pizzaria
{
    class Program
    {
    static void Main(string[] args)
        {
            PizzaManager pizzaManager = new PizzaManager();
            PedidoManager pedidoManager = new PedidoManager();

            while (true)
            {
                Console.WriteLine("ESCOLHA UMA OPÇÃO: ");
                Console.WriteLine("1 - Adicionar Pizza");
                Console.WriteLine("2 - Listar Pizzas");
                Console.WriteLine("3 - Criar Novo Pedido");
                Console.WriteLine("4 - Listar Pedidos");
                Console.WriteLine("5 - Pagar Pedido");
                Console.WriteLine("6 - Sair");
                Console.Write("Digite sua opção: ");

                int opcao;
                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            AdicionarPizza(pizzaManager);
                            break;
                        case 2:
                            ListarPizzas(pizzaManager.ListarPizzas());
                            break;
                        case 3:
                            CriarNovoPedido(pedidoManager, pizzaManager);
                            break;
                        case 4:
                            ListarPedidos(pedidoManager.ListarPedidos());
                            break;
                        case 5:
                            PagarPedido(pedidoManager);
                            break;
                        case 6:
                            Console.WriteLine("Saindo do programa.");
                            return;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida. Digite um número.");
                }
            }
        }

        static void AdicionarPizza(PizzaManager pizzaManager)
        {
            Console.WriteLine("Adicionar uma Pizza!\n");

            Console.WriteLine("Digite o nome da Pizza: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Adicione os ingredientes da pizza separados por vírgula: ");
            string ingredientesInput = Console.ReadLine();
            List<string> ingredientes = ingredientesInput.Split(',').Select(i => i.Trim()).ToList();

            Console.WriteLine("Digite o preço da Pizza (formato 00,00): ");
            float preco;
            if (float.TryParse(Console.ReadLine(), out preco))
            {
                Pizza novaPizza = new Pizza { Nome = nome, Ingredientes = ingredientes, Preco = preco };
                pizzaManager.AdicionarPizza(novaPizza);
                Console.WriteLine("\nPIZZA CRIADA COM SUCESSO!\n");
            }
            else
            {
                Console.WriteLine("Preço inválido. A pizza não será criada.");
            }
        }

        static void ListarPizzas(List<Pizza> pizzas)
        {
            Console.WriteLine("\nLista de Pizzas:\n");

            foreach (var pizza in pizzas)
            {
                Console.WriteLine($"Nome: {pizza.Nome}");
                Console.WriteLine($"Ingredientes: {string.Join(", ", pizza.Ingredientes)}");
                Console.WriteLine($"Preço: {pizza.Preco:C2}\n");
            }
        }

        static void CriarNovoPedido(PedidoManager pedidoManager, PizzaManager pizzaManager)
        {
            Console.WriteLine("Criar um Novo Pedido!\n");

            Console.WriteLine("Quem é o cliente: ");
            string clienteNome = Console.ReadLine();

            Console.WriteLine("Telefone do cliente: ");
            string clienteTelefone = Console.ReadLine();

            Pedido pedido = new Pedido
            {
                Numero = pedidoManager.ObterProximoNumeroPedido(), // Obtenha o próximo número de pedido
                ClienteNome = clienteNome,
                TelefoneCliente = clienteTelefone
            };
            float totalPedido = 0.0f;

            string opcao;
            do
            {
                Console.WriteLine("\nEscolha uma Pizza para adicionar:");
                ListarPizzas(pizzaManager.ListarPizzas());
                Console.Write("Digite o nome exato da pizza: ");
                string nomePizza = Console.ReadLine().ToUpper();

                Pizza pizzaSelecionada = pizzaManager.ObterPizzaPorNome(nomePizza);

                if (pizzaSelecionada != null)
                {
                    pedido.Pizzas.Add(pizzaSelecionada);
                    totalPedido += pizzaSelecionada.Preco;

                    Console.WriteLine($"\nPizza '{pizzaSelecionada.Nome}' adicionada ao pedido.");
                }
                else
                {
                    Console.WriteLine("Pizza não encontrada.");
                }

                Console.Write("Deseja acrescentar outra pizza? (S/N): ");
                opcao = Console.ReadLine();
            } while (opcao.ToUpper() == "S");

            Console.WriteLine("\nPEDIDO CRIADO");
            Console.WriteLine($"Pedido #{pedido.Numero}:"); // Correção aqui
            Console.WriteLine($"Cliente: {clienteNome} - {clienteTelefone}");
            Console.WriteLine("Pizzas no Pedido:");

            foreach (var pizza in pedido.Pizzas)
            {
                Console.WriteLine($"- {pizza.Nome} - {pizza.Preco:C2}");
            }

            Console.WriteLine($"Total: {totalPedido:C2}\n");

            pedidoManager.AdicionarPedido(pedido);
        }

        static void ListarPedidos(List<Pedido> pedidos)
        {
            Console.WriteLine("\nLista de Pedidos:\n");

            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"PEDIDO #{pedido.Numero}:");
                Console.WriteLine($"Cliente: {pedido.ClienteNome} - {pedido.TelefoneCliente}");
                Console.WriteLine("Pizzas do Pedido:");

                foreach (var pizza in pedido.Pizzas)
                {
                    Console.WriteLine($"- {pizza.Nome} - R$ {pizza.Preco:C2}");
                }

                float totalPedido = pedido.Pizzas.Sum(pizza => pizza.Preco);
                Console.WriteLine($"Total do Pedido: R$ {totalPedido:C2}");

                float faltaPagar = totalPedido - pedido.ValorPago; // Calcula o valor que falta pagar
                Console.WriteLine($"Quanto Falta para Pagar: R$ {faltaPagar:C2}");

                string statusPagamento = pedido.Pago ? "SIM" : "NÃO";
                Console.WriteLine($"Pago: {statusPagamento}");
                Console.WriteLine(); // Pula uma linha entre os pedidos
            }
        }

        static void PagarPedido(PedidoManager pedidoManager)
        {
            Console.WriteLine("Digite o número do pedido que deseja pagar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroPedido))
            {
                Pedido pedido = pedidoManager.ObterPedidoPorNumero(numeroPedido);
                if (pedido != null && !pedido.Pago)
                {
                    Console.WriteLine($"Pedido #{numeroPedido} encontrado.");
                    Console.WriteLine("Escolha a forma de pagamento:");
                    Console.WriteLine("1 - Dinheiro");
                    Console.WriteLine("2 - Cartão de Débito");
                    Console.WriteLine("3 - Vale-Refeição");
                    Console.Write("Escolha uma forma de pagamento: ");

                    if (int.TryParse(Console.ReadLine(), out int formaPagamento) && formaPagamento >= 1 && formaPagamento <= 3)
                    {
                        List<string> formasPagamento = new List<string>();

                         if (formaPagamento == 1 || formaPagamento == 2)
                        {
                            Console.Write("Qual o valor pago: ");
                            if (float.TryParse(Console.ReadLine(), out float valorPago))
                            {
                                if (valorPago >= pedido.Total)
                                {
                                    if (valorPago > pedido.Total)
                                    {
                                        Console.WriteLine($"Troco: R${valorPago - pedido.Total:C2}");
                                    }
                                    formasPagamento.Add(formaPagamento == 1 ? "Dinheiro" : "Cartão de Débito");
                                    pedido.Pago = true;
                                    pedido.ValorPago = valorPago;
                                }
                                else
                                {
                                    Console.WriteLine("Valor insuficiente para pagamento.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Valor inválido.");
                            }
                        }
                        else
                        {
                            formasPagamento.Add("Vale-Refeição");
                            pedido.Pago = true;
                        }

                        pedido.FormasDePagamento.AddRange(formasPagamento);
                        Console.WriteLine("Pagamento registrado com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Forma de pagamento inválida.");
                    }
                }
                else if (pedido != null && pedido.Pago)
                {
                    Console.WriteLine($"Pedido #{numeroPedido} já foi pago.");
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado ou já foi pago.");
                }
            }
            else
            {
                Console.WriteLine("Número de pedido inválido. Digite um número.");
            }
        }
  
    
    }
                
}
