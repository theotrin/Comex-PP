// See https://aka.ms/new-console-template for more information
using Comex;
using System.Text.Json;
using Comex.Models;
using Comex.Service;

// lstpt = lista de produtos para ser utilizada durante os testes
var lstpt = new List<Produto>
{
    new Produto("Notebook")
    {
        Descricao = "Notebook Dell Inspiron",
        PrecoUnitario = 3500.00,
        Quantidade = 10
    },
    new Produto("Smartphone")
    {
        Descricao = "Smartphone Samsung Galaxy",
        PrecoUnitario = 1200.00,
        Quantidade = 25
    },
    new Produto("Monitor")
    {
        Descricao = "Monitor LG Ultrawide",
        PrecoUnitario = 800.00,
        Quantidade = 15
    },
    new Produto("Teclado")
    {
        Descricao = "Teclado Mecânico RGB",
        PrecoUnitario = 250.00,
        Quantidade = 50
    }
};

Dictionary<int, Menu> opcoes = new();

opcoes.Add(2,new ListarProdutos());

// ltspd = lista de pedidos para ser utilizada durante os testes
var ltspd = new List<Pedido>();

// ms = mensagem de boas veindas do projeto
string ms = "Boas vindas ao COMEX";

// Método que tem função de Exibir o Logo
void Elg()
{
    Console.WriteLine(@"
────────────────────────────────────────────────────────────────────────────────────────
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██████████████░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██░░██████████─██░░██████░░██─██░░░░░░░░░░░░░░░░░░██─██░░██████████─████░░██──██░░████─
─██░░██─────────██░░██──██░░██─██░░██████░░██████░░██─██░░██───────────██░░░░██░░░░██───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░░░░░░░░░██─────██░░░░░░██─────
─██░░██─────────██░░██──██░░██─██░░██──██████──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──────────██░░██─██░░██───────────██░░░░██░░░░██───
─██░░██████████─██░░██████░░██─██░░██──────────██░░██─██░░██████████─████░░██──██░░████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██──────────██░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
────────────────────────────────────────────────────────────────────────────────────────");
    Console.WriteLine(ms);
}

// Método que tem função de Exibir Opcoes Do Menu
async Task Epdm()
{
    Elg();
    Console.WriteLine("\nDigite 1 Criar Produto");
    Console.WriteLine("Digite 2 Listar Produtos");
    Console.WriteLine("Digite 3 Consultar API Externa");
    Console.WriteLine("Digite 4 Ordenar Produtos pelo Título");
    Console.WriteLine("Digite 5 Ordenar Produtos pelo Preço");
    Console.WriteLine("Digite 6 Criar Pedido");
    Console.WriteLine("Digite 7 Listar Pedidos");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica)) {

        Menu menu = opcoes[opcaoEscolhidaNumerica];
        menu.Executar(lstpt);
        await Epdm();
    }

    switch (opcaoEscolhidaNumerica)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("Registro de Produto");

            Console.Write("Digite o nome do Produto: ");
            string nomeDoProduto = Console.ReadLine();
            var produto = new Produto(nomeDoProduto);

            Console.Write("Digite a descrição do Produto: ");
            string descricaoDoProduto = Console.ReadLine();
            produto.Descricao = descricaoDoProduto;

            Console.Write("Digite o preço do Produto: ");
            string preçoDoProduto = Console.ReadLine();
            produto.PrecoUnitario = double.Parse(preçoDoProduto);

            Console.Write("Digite a quantidade do Produto: ");
            string quantidadeDoProduto = Console.ReadLine();
            produto.Quantidade = int.Parse(quantidadeDoProduto);

            lstpt.Add(produto);
            Console.WriteLine($"O Produto {produto.Nome} foi registrado com sucesso!");
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
            await Epdm();
            break;
        case 3:
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nExibindo Produtos\n");
                    string resposta = await client.GetStringAsync("http://fakestoreapi.com/products");
                    var produtos = JsonSerializer.Deserialize<List<Produto>>(resposta)!;
                    for (int i = 0; i < produtos.Count; i++)
                    {
                        Console.WriteLine($"Nome: {produtos[i].Nome}, " +
                            $"Descrição: {produtos[i].Descricao}, " +
                            $"Preço {produtos[i].PrecoUnitario} \n");
                    }
                    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
                    Console.ReadKey();
                    Console.Clear();
                    await Epdm();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Temos um problema: {ex.Message}");
                }
            }
            break;
        case 4:
            var produtosOrdenados = lstpt.OrderBy(p => p.Nome).ToList();
            Console.Clear();
            Console.WriteLine("Produtos ordenados pelo título:");
            for (int i = 0; i < produtosOrdenados.Count; i++)
            {
                Console.WriteLine($"Produto: {produtosOrdenados[i].Nome}, Preço: {produtosOrdenados[i].PrecoUnitario:F2}");
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
            await Epdm();
            break;
        case 5:
            var produtosOrdenadosPorPreco = lstpt.OrderBy(p => p.PrecoUnitario).ToList();
            Console.Clear();
            Console.WriteLine("Produtos ordenados pelo preço:");
            for (int i = 0; i < produtosOrdenadosPorPreco.Count; i++)
            {
                Console.WriteLine($"Produto: {produtosOrdenadosPorPreco[i].Nome}, Preço: {produtosOrdenadosPorPreco[i].PrecoUnitario:F2}");
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
            await Epdm();
            break;
        case 6:
            Console.Clear();
            Console.WriteLine("Criando um novo pedido\n");

            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine()!;
            var cliente = new Cliente();
            cliente.Nome = nomeCliente;

            var pedido = new Pedido(cliente);

            Console.WriteLine("\nProdutos disponíveis:");
            for (int i = 0; i < lstpt.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {lstpt[i].Nome}");
            }

            Console.Write("Digite o número do produto que deseja adicionar (ou 0 para finalizar): ");
            int numeroProduto = int.Parse(Console.ReadLine()!);

            var produtoEscolhido = lstpt[numeroProduto - 1];

            Console.Write("Digite a quantidade: ");
            int quantidade = int.Parse(Console.ReadLine()!);

            var itemDePedido = new ItemDePedido(produtoEscolhido, quantidade, produtoEscolhido.PrecoUnitario);
            pedido.AdicionarItem(itemDePedido);

            Console.WriteLine($"Item adicionado: {itemDePedido}\n");


            ltspd.Add(pedido);
            Console.WriteLine($"\nPedido criado com sucesso:\n{pedido}");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            await Epdm();
            break;
        case 7:
            Console.Clear();
            Console.WriteLine("Exibindo todos os produtos registradoss na nossa aplicação");

            var pedidosOrdenados = ltspd.OrderBy(p => p.Cliente.Nome).ToList();

            foreach (var Pedido in pedidosOrdenados)
            {
                Console.WriteLine($"Cliente: {Pedido.Cliente.Nome}, Total: {Pedido.Total:F2}");
            }

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
            await Epdm();
            break;
        case -1:
            Console.WriteLine("Tchau tchau :)");
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
}

await Epdm();






