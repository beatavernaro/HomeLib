using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;


namespace HomeLib
{
    internal class FindBook
    {

        #region GetAll
        public static async Task GetAll()
         {

                HttpClient httpClient = new HttpClient();
                
                try
                {
                    var response = await httpClient.GetAsync($"https://localhost:44335/api/Books");
                    var code = response.StatusCode;
                    var message = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<BookData>>(message);

                foreach (var bookData in result.OrderBy(x => x.Title))
                {
                    Console.WriteLine($"-- ID: {bookData.Id}  --");
                    Console.WriteLine($"Título: {bookData.Title}  |  Autor: {bookData.Authors}  |  Categoria: {bookData.Categorie} \r\n");
                }
                
            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

         }
        #endregion

        #region ShowBookByYear
        public static async Task ShowBookByYear(string year)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44335/api/Books/year/{year}");
            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<BookData>>(message);

            if (result.Count() != 0)
            {
                foreach (var bookData in result)
                {
                    Console.WriteLine($"-- ID: {bookData.Id}  --");
                    Console.WriteLine($"Título: {bookData.Title}  |  Autor: {bookData.Authors}  |  Categoria: {bookData.Categorie}  |  Ano: {bookData.PublishedDate}\r\n");
                }

            } else
            {
                Console.WriteLine("Livro não encontrado!");
                Thread.Sleep(2000);
                Menu.MainMenu();
            }
        }
        #endregion

        #region ShowBookByAuthor
        public static async Task ShowBookByAuthor(string name)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44335/api/Books/author/{name}");
            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<BookData>>(message);

            if (result.Count() != 0)
            {
                foreach (var bookData in result)
                {
                    Console.WriteLine($"-- ID: {bookData.Id}  --");
                    Console.WriteLine($"Título: {bookData.Title}  |  Autor: {bookData.Authors}  |  Categoria: {bookData.Categorie}  |  Ano: {bookData.PublishedDate}\r\n");
                }

            }
            else
            {
                Console.WriteLine("Autor não encontrado!");
                Thread.Sleep(2000);
                Menu.MainMenu();
            }
        }
        #endregion

        #region ShowBookByTitle
        public static async Task ShowBookByTitle(string title)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44335/api/Books/title/{title}");
            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<BookData>>(message);

            if (result.Count() != 0)
            {
                foreach (var bookData in result)
                {
                    Console.WriteLine($"-- ID: {bookData.Id}  --");
                    Console.WriteLine($"Título: {bookData.Title}  |  Autor: {bookData.Authors}  |  Categoria: {bookData.Categorie}  |  Ano: {bookData.PublishedDate}\r\n");
                }
                
            }
            else
            {
                Console.WriteLine("Titulo não encontrado!");
                Thread.Sleep(2000);
                Menu.MainMenu();
            }
        }
        #endregion

        #region ShowBookById
        public static async Task<BookData> ShowBookById(int id)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44335/api/Books/id/{id}");
            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<BookData>>(message);

            return result[0];
        }
        #endregion

        #region DeleteOneBook
        public static async Task DeleteOneBook()
        {
            Console.Write("Digite o título para encontrar o livro: ");
            var title = Console.ReadLine()!;
            ShowBookByTitle(title).GetAwaiter().GetResult();

            Console.Write("Digite o ID do livro que gostaria de deletar: ");
            var idDelete = int.Parse(Console.ReadLine()!);
            
            Console.Clear();
            await ShowBookById(idDelete);
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
            ShowBookByTitle(title).GetAwaiter().GetResult();

            Console.Write("Digite o ID do livro que gostaria de atualizar: ");
            var idUpdate = int.Parse(Console.ReadLine()!);

            Console.Clear();
            
            var book = ShowBookById(idUpdate).GetAwaiter().GetResult();

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

    }
}

