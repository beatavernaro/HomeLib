namespace HomeLib.Application.Services;

/// <summary>
/// Serviço dedicado as rotinas relacionadas ao Livro.
/// </summary>
/// <remarks>Iremos tratar de repositórios mais pra frente. Por hora, será simples assim.</remarks>
public static class BookService
{
    private const string NomeBancoDados = "bancoDados.txt";

    public static Book ToBook(this GoogleBookResponse googleBookResponse)
    {
        var volumeInfo = googleBookResponse.FirstVolumeInfo;
        
        return new Book
        {
            Id = new Random().Next(1, int.MaxValue),
            Isbn = volumeInfo?.Isbn,
            Title = volumeInfo?.Title,
            Authors = volumeInfo?.FirstAuthor,
            PublishedDate = volumeInfo?.PublishedDate,
            PageCount = volumeInfo?.PageCount,
            Categorie = volumeInfo?.FirstCategory,
            Language = volumeInfo?.Language,
            Description = volumeInfo?.Description
        };
    }
    
    public static async Task<int> SaveBookAsync(Book book)
    {
        if (!File.Exists(NomeBancoDados))
        {
            await File.WriteAllTextAsync(NomeBancoDados, string.Empty).ConfigureAwait(false);
        }
        
        var bancoDadosJson = await File.ReadAllTextAsync(NomeBancoDados).ConfigureAwait(false);
        var bancoDados = JsonConvert.DeserializeObject<List<Book>>(bancoDadosJson) ?? new List<Book>();
        
        var isbnJaCadastrado = bancoDados.Any(a =>
            a.Isbn is not null && a.Isbn.Equals(book.Isbn, StringComparison.OrdinalIgnoreCase)); 

        if (isbnJaCadastrado)
        {
            throw new Exception("Código ISBN já registrado.");
        }

        bancoDados.Add(book);
        
        //Como não temos banco de dados, estou mantendo aqui algo rudimentar, porém, funcional.
        //Não suporta gravações concorrentes. É apenas didático.
        //Quando movermos para um banco de dados real, isso será removido.
        await File.WriteAllTextAsync(NomeBancoDados, JsonConvert.SerializeObject(bancoDados)).ConfigureAwait(false);

        return book.Id;
    }
}