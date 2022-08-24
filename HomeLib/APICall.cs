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
         public static async Task Call()
         {
            
            // https://www.googleapis.com/books/v1/volumes?q=isbn:8533613377
            HttpClient httpClient = new HttpClient();
                
                try
                {
                var newSearch = "s";
                while (newSearch == "s")
                {
                    Console.WriteLine("Digite o ISBN do livro: ");
                    var isbn = long.Parse(Console.ReadLine());
                    //var isbn = 8533613377; //arch 8501061700

                    var response = await httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
                    var code = response.StatusCode;
                    var message = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<BookResponse>(message);

                    if (result.TotalItems != 0)
                    {
                        GetBook(result);
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

        public static void GetBook(BookResponse bookResponse)
        {
            Console.WriteLine("Livro encontrado!");

            var bookInfo = bookResponse.Items[0].VolumeInfo;

            BookData newBook = new BookData();
            newBook.Title = bookInfo.Title;
            newBook.Authors = bookInfo.Authors.ToArray()[0];
            newBook.PublishedDate = bookInfo.PublishedDate;
            newBook.PageCount = bookInfo.PageCount;
            newBook.Categorie = bookInfo.Categories.ToArray()[0].Split(",")[0];
            newBook.Language = bookInfo.Language;
            newBook.Description = bookInfo.Description;
            Console.WriteLine($" Título: {bookInfo.Title}  |  Autor: {bookInfo.Authors.ToArray()[0]}");

            Console.WriteLine("Gostaria de adicionar esse livro a sua coleção? s/n");
            var input = Console.ReadLine();
            if(input == "s")
            {
                /*Console.WriteLine("Esse livro está emprestado?");
                input = Console.ReadLine();
                if (input == "s")
                    newBook.Borrowed = "s";
                else
                    newBook.Borrowed = "n";
                //AQUI SALVA O LIVRO NO BANCO */
                AddBook(newBook);
            }

        }

        public static async Task AddBook(BookData newBook)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonSerializer.Serialize(newBook);
            
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            

            Console.WriteLine("PASSOU AQUI ANTES");
           var response = await httpClient.PostAsync("https://localhost:44335/api/Books", httpContent);
            Console.WriteLine("PASSOU AQUI DEPOIS");

            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("foi");
            }
            else
            {
                Console.WriteLine("erro");
            }

            
        }

    }
}

