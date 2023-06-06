using HomeLib.Application.Services;
//Estou isolando o namespace novo para demarcar bem o que é novo e o que você fez, assim
//fica mais fácil compreender o objetivo das mudanças. Ao fim da refatoração, podemores 
//simplificar isso.
using DomainDto = HomeLib.Domain.Dto;

namespace HomeLib;

internal class APICall
{
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
                    Console.WriteLine($"{Environment.NewLine}Livro encontrado!{Environment.NewLine}");
                    Console.WriteLine(
                        $" Título: {bookResponse.FirstVolumeInfo.Title}  |  Autor: {bookResponse.FirstVolumeInfo.FirstAuthor}");
                    Console.WriteLine(
                        $" Categoria: {bookResponse.FirstVolumeInfo.FirstCategory}  |  Data da publicação: {bookResponse.FirstVolumeInfo.PublishedDate}");
                    Console.WriteLine($"{Environment.NewLine}Gostaria de adicionar esse livro a sua coleção? s/n");

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
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region SaveBook

    private static async Task SaveBook(DomainDto.BookResponse bookResponse)
    {
        var bookInfo = bookResponse.Items?[0].VolumeInfo;
           
        var newBook = new DomainDto.BookData
        {
            Title = bookInfo?.Title,
            Authors = bookInfo?.Authors?.ToArray()[0],
            PublishedDate = bookInfo?.PublishedDate,
            PageCount = bookInfo?.PageCount,
            Categorie = bookInfo?.Categories?.ToArray()[0],
            Language = bookInfo?.Language,
            Description = bookInfo?.Description
        };

        await ManipulateBook.AddBook(newBook);
        Console.WriteLine("\r\nSalvando...");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("Salvo!");
        Console.WriteLine("Voltando ao menu principal");
        Thread.Sleep(1500);
        Console.Clear();
        Menu.MainMenu();
          
    }
    #endregion

    #region EnterBook
    public static async Task EnterBook()
    {
        var newBook = new DomainDto.BookData();
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
            Console.WriteLine("\r\nSalvando...");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Salvo!");
            Console.WriteLine("Voltando ao meu principal");
            Thread.Sleep(1500);
            Console.Clear();
            Menu.MainMenu();
        }
        else
        {
            Console.WriteLine("Voltando ao meu principal");
            Thread.Sleep(1500);
            Console.Clear();
            Menu.MainMenu();
        }
    }
    #endregion
}