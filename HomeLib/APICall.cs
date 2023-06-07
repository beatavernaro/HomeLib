//Estou isolando o namespace novo para demarcar bem o que é novo e o que você fez, assim
//fica mais fácil compreender o objetivo das mudanças. Ao fim da refatoração, poderemos 
//simplificar isso.

using System.Net;
using System.Text;
using Newtonsoft.Json;
using DomainDto = HomeLib.Domain.Dto;
using DomainEntidade = HomeLib.Domain.Entidades;

namespace HomeLib;

internal class APICall
{
    /// <summary>
    /// Url base da execução da HomeLib.Api.
    /// </summary>
    private const string ApiUrlBase = @"https://localhost:7031/";

    #region Call Google Books API
    public static async Task Call()
    {
        try
        {
            var newSearch = "s";

            while (newSearch == "s")
            {
                Console.WriteLine("Digite o ISBN do livro: ");
                //A proposta aqui foi isolar toda a rotina específica do Google em seu próprio
                //serviço, situado na Application. A ideia, por hora, é não causar calamidade no
                //fluxo existente. Vamos atacando aos poucos.
                var bookResponse = await GoogleBooksService.ObterLivroPorNumeroIsbnAsync(Console.ReadLine());

                if (bookResponse?.FirstVolumeInfo is not null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Livro encontrado");
                    Console.WriteLine();
                    Console.WriteLine(
                        $" Título: {bookResponse.FirstVolumeInfo.Title}  |  Autor: {bookResponse.FirstVolumeInfo.FirstAuthor}");
                    Console.WriteLine(
                        $" Categoria: {bookResponse.FirstVolumeInfo.FirstCategory}  |  Data da publicação: {bookResponse.FirstVolumeInfo.PublishedDate}");
                    Console.WriteLine();
                    Console.WriteLine("Gostaria de adicionar esse livro a sua coleção? s/n");

                    var input = Console.ReadLine()!.ToLower();

                    if (input == "s")
                    {
                        await SaveBook(bookResponse);
                        newSearch = "n";
                    }
                }
                else
                {
                    Console.WriteLine("Livro não encontrado!");
                    Console.WriteLine("Gostaria de adicionar o livro manualmente? s/n");
                    if (Console.ReadLine()! == "s")
                        await EnterBook();
                    else
                    {
                        Console.WriteLine("\r\nGostaria de fazer outra busca?");
                        newSearch = Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //No momento, estamos tradando do fluxo de mensagens através de exceptions, apenas
            //por questões didáticas. Em breve, vamos mudar isso e torná-lo mais funcional.
            //Modifiquei aqui para o uso no console ser mais fluído.
            Console.WriteLine(ex.Message);
            Console.WriteLine("Pressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();
            Console.Clear();
            Menu.MainMenu();
        }
    }
    #endregion

    #region SaveBook

    private static async Task SaveBook(DomainDto.GoogleBookResponse googleBookResponse)
    {
        //Como agora vamos espetar a Api aqui, futuramente, a HomeLib não terá referência direta
        //a HomeLib.Application. Portanto, irei deixar aqui por referência.
        //await BookService.SaveBookFromGoogleBookAsync(googleBookResponse).ConfigureAwait(false);

        Console.WriteLine();
        Console.WriteLine("Salvando...");
        
        using (var httpClient = new HttpClient())
        {
            var json = JsonConvert.SerializeObject(googleBookResponse.ToBook());
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{ApiUrlBase}Book", httpContent);

            if (response is { IsSuccessStatusCode: false, StatusCode: HttpStatusCode.UnprocessableEntity })
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
        
        Console.Clear();
        Console.WriteLine("Salvo!");
        Console.WriteLine("Voltando ao menu principal");
        Console.Clear();
        Menu.MainMenu();
    }
    #endregion

    #region EnterBook
    public static async Task EnterBook()
    {
        var newBook = new DomainEntidade.Book();
        Console.WriteLine("Tenha o livro em mão e digite:");
        Console.Write("Título: ");
        newBook.Title = Console.ReadLine()!;

        Console.Write("Autor: ");
        newBook.Authors = Console.ReadLine()!;

        Console.Write("Data da publicação: ");
        newBook.PublishedDate = Console.ReadLine()!;

        Console.Write("Quantidade de páginas: ");
        newBook.PageCount = int.Parse(Console.ReadLine()!);

        Console.Write("Categoria: ");
        newBook.Categorie = Console.ReadLine()!;

        Console.Write("Idioma: ");
        newBook.Language = Console.ReadLine()!;

        newBook.Description = " ";

        Console.WriteLine("Deseja salvar esse livro? s/n");
        if (Console.ReadLine() == "s".ToLower())
        {
            await ManipulateBook.AddBook(newBook);
            Console.WriteLine();
            Console.WriteLine("Salvando...");
            Console.Clear();
            Console.WriteLine("Salvo!");
            Console.WriteLine("Voltando ao meu principal");
            Console.Clear();
            Menu.MainMenu();
        }
        else
        {
            Console.WriteLine("Voltando ao meu principal");
            Console.Clear();
            Menu.MainMenu();
        }
    }
    #endregion
}