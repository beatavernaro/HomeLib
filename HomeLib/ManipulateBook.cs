using System.Text;
using System.Text.Json;


namespace HomeLib
{
    internal class ManipulateBook
    {

        #region DeleteOneBook
        public static async Task DeleteOneBook()
        {
            Console.Write("Digite o título para encontrar o livro: ");
            var title = Console.ReadLine()!;
            FindBook.ShowBookByTitle(title).GetAwaiter().GetResult();

            Console.Write("Digite o ID do livro que gostaria de deletar: ");
            var idDelete = int.Parse(Console.ReadLine()!);

            Console.Clear();
            await FindBook.ShowBookById(idDelete);
            Console.WriteLine("\r\nDeseja deletar esse livro? s/n \r\n");
            var input = Console.ReadLine();
            if (input == "s")
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync($"https://localhost:44335/api/Books/del/{idDelete}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Livro deletado!");
                    Thread.Sleep(2000);
                    Menu.MainMenu();
                }
                else
                {
                    Console.WriteLine("Opa! Algo de errado não está certo. Voltando ao menu principal");
                    Thread.Sleep(2000);
                    Menu.MainMenu();
                }
            }

        }
        #endregion

        #region UpdateOneBook
        public static async Task UpdateOneBook()
        {
            Console.Write("Digite o título para encontrar o livro: ");
            var title = Console.ReadLine()!;
            FindBook.ShowBookByTitle(title).GetAwaiter().GetResult();

            Console.Write("Digite o ID do livro que gostaria de atualizar: ");
            var idUpdate = int.Parse(Console.ReadLine()!);

            Console.Clear();

            var book = FindBook.ShowBookById(idUpdate).GetAwaiter().GetResult();

            var bookUpdate = new BookData();

            bookUpdate = book;

            Console.WriteLine("\r\nDeseja atualizar esse livro? s/n \r\n");
            var input = Console.ReadLine();
            if (input == "s")
            {

                var update = "s";
                while (update == "s")
                {

                    Console.WriteLine("Qual campo? ");
                    Console.WriteLine("1 - Título");
                    Console.WriteLine("2 - Autor");
                    Console.WriteLine("3 - Categoria");
                    Console.WriteLine("4 - Ano");
                    var option = int.Parse(Console.ReadLine()!);
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Título: ");
                            bookUpdate.Title = Console.ReadLine()!;
                            break;
                        case 2:
                            Console.WriteLine("Autor: ");
                            bookUpdate.Authors = Console.ReadLine()!;
                            break;
                        case 3:
                            Console.WriteLine("Categoria: ");
                            bookUpdate.Categorie = Console.ReadLine()!;
                            break;
                        case 4:
                            Console.WriteLine("Ano: ");
                            bookUpdate.PublishedDate = Console.ReadLine()!;
                            break;
                    }
                    Console.WriteLine("\r\nAtualizar mais algum campo? s/n");
                    update = Console.ReadLine();
                }


                HttpClient httpClient = new HttpClient();

                var json = JsonSerializer.Serialize(bookUpdate);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync("https://localhost:44335/api/Books", httpContent);
                var code = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Livro atualizado!");
                    Thread.Sleep(2000);
                    Menu.MainMenu();
                }
                else
                {
                    Console.WriteLine("Opa! Algo de errado não está certo. Voltando ao menu principal");
                    Thread.Sleep(2000);
                    Menu.MainMenu();
                }
            }

        }
        #endregion

        #region AddBook
        public static async Task AddBook(BookData newBook)
        {
            HttpClient httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(newBook);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:5247/api/Books", httpContent);
        }
        #endregion

    }
}

