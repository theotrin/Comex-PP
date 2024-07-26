using Comex;
using System.Text.Json;
using Comex.Models;

namespace Comex.Service;

internal class ListarProdutos : Menu
{
    public override void Executar(List<Produto> produtos)
    {
        base.Executar(produtos);

        Console.WriteLine("Exibindo todos os produtos registradoss na nossa aplicação");

        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"Produto: {produtos[i].Nome}, Preço: {produtos[i].PrecoUnitario:F2}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
