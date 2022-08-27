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
                        await SaveBook(result);
                        newSearch = "n";
                    }
                    else
                    {
                        Console.WriteLine("Livro não encontrado!");
                        Console.WriteLine("Gostaria de fazer outra busca? s/n");
                        newSearch = Console.ReadLine();
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
            Console.WriteLine("\r\nLivro encontrado!\r\n");

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

            Console.WriteLine($" Título: {newBook.Title}  |  Autor: {newBook.Authors}");
            Console.WriteLine($" Categoria: {newBook.Categorie}  |  Data da publicação: {newBook.PublishedDate}");
            Console.WriteLine();
            Console.WriteLine("Gostaria de adicionar esse livro a sua coleção? s/n");
            var input = Console.ReadLine();
            if (input == "s")
            {
                await AddBook(newBook);
                Console.WriteLine("\r\nSalvando...");
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine("Salvo!");
                Thread.Sleep(1500);
                Console.Clear();
                Menu.MainMenu();
            }
            else
                Menu.MainMenu();
        }
        #endregion

        #region AddBook
        public static async Task AddBook(BookData newBook)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonSerializer.Serialize(newBook);
            
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("https://localhost:44335/api/Books", httpContent);
        
        }
        #endregion

    }
}

