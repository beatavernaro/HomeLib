namespace HomeLib.Application.Services;

/// <summary>
/// Serviço dedicado a consumo da Api do GoogleBooks.
/// </summary>
/// <remarks>
/// Por hora, manteremos-a estática por questões de didática, já que futuramente, iremos abordar injeção de dependência.
/// </remarks>
public static class GoogleBooksService
{
    private const string GoogleBooksUrl = "https://www.googleapis.com/books/v1/";

    /// <summary>
    /// Obtém detalhe de um livro.
    /// </summary>
    /// <param name="isbn">Código ISBN.</param>
    /// <returns>Retorna um <see cref="GoogleBookResponse"/>.</returns>
    /// <exception cref="Exception">Interrompe a aplicação caso as validações básicas não sejam atendidas.</exception>
    /// <remarks>
    /// <para>
    /// Por hora, estou mantendo uma <see cref="Exception"/> como interrupção de fluxo, apenas por didática. A ideia final
    /// será adotarmos algo mais funcional (circuito), usando a biblioteca OneOf. Mas irei deixar isso para um commit
    /// futuro. 
    /// </para>
    /// </remarks>
    public static async Task<GoogleBookResponse?> ObterLivroPorNumeroIsbnAsync(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            throw new Exception("Código ISBN vazio ou inválido.");
        }
        
        using var httpClient = new HttpClient();
        
        var httpResponseMessage =
            await httpClient.GetAsync($"{GoogleBooksUrl}volumes?q=isbn:{isbn}").ConfigureAwait(false);

        if (httpResponseMessage.IsSuccessStatusCode && httpResponseMessage.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Serviço indisponível. Tente novamente mais tarde.");
        }

        var bookResponseSerializado = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        var bookResponse = JsonConvert.DeserializeObject<GoogleBookResponse>(bookResponseSerializado);

        if (bookResponse is null || bookResponse.TotalItems <= 0)
        {
            throw new Exception("Livro não encontrado.");
        }

        if (bookResponse.FirstVolumeInfo != null)
        {
            bookResponse.FirstVolumeInfo.Isbn = isbn;
        }

        return bookResponse;
    }
}