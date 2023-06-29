using System.Text.Json;


namespace HomeLib
{
    internal class FindBook
    {

        #region GetAll
        public static async Task GetAll()
        {

            HttpClient httpClient = new HttpClient();

            try
            {//refit
                var response = await httpClient.GetAsync($"https://localhost:5247/api/books");
                var code = response.StatusCode;
                var message = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<BookData>>(message);

                foreach (var bookData in result.OrderBy(x => x.Title))
                {
                    Console.WriteLine($"-- ID: {bookData.Id}  --");
                    Console.WriteLine($"Título: {bookData.Title}  |  Autor: {bookData.Authors}  |  Categoria: {bookData.Categorie} \r\n");
                }
                Menu.GoBack();
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
                Menu.GoBack();

            }
            else
            {
                Console.WriteLine("Livro não encontrado!");
                Console.WriteLine("Voltando ao menu inicial");
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
                Menu.GoBack();
            }
            else
            {
                Console.WriteLine("Autor não encontrado!");
                Console.WriteLine("Voltando ao menu inicial");
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
                Menu.GoBack();
            }
            else
            {
                Console.WriteLine("Titulo não encontrado!");
                Console.WriteLine("Voltando ao menu inicial");
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

    }
}

