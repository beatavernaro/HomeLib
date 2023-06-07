namespace HomeLib.Application.Services;

/// <summary>
/// Serviço dedicado as rotinas relacionadas ao Livro.
/// </summary>
/// <remarks>Iremos tratar de repositórios mais pra frente. Por hora, será simples assim.</remarks>
public class BookService
{
    private const string NomeBancoDados = "bancoDados.txt";
    public static async Task SaveBookAsync(BookResponse bookResponse)
    {
        var bancoDadosJson = await File.ReadAllTextAsync(NomeBancoDados).ConfigureAwait(false);
        var livros = JsonConvert.DeserializeObject<List<Book>>(bancoDadosJson) ?? new List<Book>();
        
        var volumeInfo = bookResponse.FirstVolumeInfo;
        var isbnJaCadastrado = livros.Any(a =>
            a.Isbn is not null && a.Isbn.Equals(volumeInfo?.Isbn, StringComparison.OrdinalIgnoreCase)); 

        if (isbnJaCadastrado)
        {
            throw new Exception("Código ISBN já registrado.");
        }
           
        var newBook = new Book
        {
            Id = livros.Count + 1,
            Isbn = volumeInfo?.Isbn,
            Title = volumeInfo?.Title,
            Authors = volumeInfo?.FirstAuthor,
            PublishedDate = volumeInfo?.PublishedDate,
            PageCount = volumeInfo?.PageCount,
            Categorie = volumeInfo?.FirstCategory,
            Language = volumeInfo?.Language,
            Description = volumeInfo?.Description
        };

        
        livros.Add(newBook);
        
        //Como não temos banco de dados, estou mantendo aqui algo rudimentar, porém, funcional.
        await File.WriteAllTextAsync(NomeBancoDados, JsonConvert.SerializeObject(livros)).ConfigureAwait(false);
    }
}