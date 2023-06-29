using System.Text.Json;

namespace HomeLib
{
    internal class APICall
    {

        #region Call Google Books API
        public static async Task Call()
        {

            HttpClient httpClient = new HttpClient();

            try
            {
                var newSearch = "s";
                while (newSearch == "s")
                {
                    Console.WriteLine("Digite o ISBN do livro: ");
                    var isbn = long.Parse(Console.ReadLine()!);
                    //var isbn = 8533613377; //arch 8501061700

                    var response = await httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
                    var code = response.StatusCode;
                    var message = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<BookResponse>(message);

                    if (result!.TotalItems != 0)
                    {
                        Console.WriteLine("\r\nLivro encontrado!\r\n");
                        var bookInfo = result.Items[0].VolumeInfo;
                        Console.WriteLine($" Título: {bookInfo.Title}  |  Autor: {bookInfo.Authors[0]}");
                        Console.WriteLine($" Categoria: {bookInfo.Categories[0]}  |  Data da publicação: {bookInfo.PublishedDate}");
                        Console.WriteLine("\r\nGostaria de adicionar esse livro a sua coleção? s/n");

                        var input = Console.ReadLine()!.ToLower();

                        if (input == "s")
                        {
                            await SaveBook(result);
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
        public static async Task SaveBook(BookResponse bookResponse)
        {
            var bookInfo = bookResponse.Items[0].VolumeInfo;

            BookData newBook = new BookData();

            newBook.Title = bookInfo.Title;
            newBook.Authors = bookInfo.Authors.ToArray()[0];
            newBook.PublishedDate = bookInfo.PublishedDate;
            newBook.PageCount = bookInfo.PageCount;
            if (bookInfo.Categories.ToArray()[0] == null)
                newBook.Categorie = "Não encontrado";
            else
                newBook.Categorie = bookInfo.Categories.ToArray()[0];
            newBook.Language = bookInfo.Language;
            newBook.Description = bookInfo.Description;

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
            var newBook = new BookData();
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
}

