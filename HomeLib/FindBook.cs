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
            }
        }


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
            }
        }

        

        /*public static async Task DeleteOneBook(int id)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:44335/api/Books/del/{id}");
            var code = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BookData>(message);

            Console.WriteLine($"-- {result.Id} --");
            Console.WriteLine($"Title: {result.Title}  |  Author: {result.Authors}  |  Category: {result.Categorie} \r\n");
        }*/

    }
}

