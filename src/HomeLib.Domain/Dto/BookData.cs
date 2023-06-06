namespace HomeLib.Domain.Dto;

/// <summary>
/// Representa os dados do livro.
/// </summary>
public class BookData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Nome do autor.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Há várias formas de garantir a inicialização de um tipo "anulável", como por exemplo definí-lo conforme ocorre
    ///com a <see cref="Title"/>, via construtor ou via operador anulável "?".
    ///     </para>
    ///     <para>
    ///         Não tem certo ou errado e sim aquele que encaixa melhor no seu contexto. Geralmente, inicializá-los no
    /// construtor é o que faz sentido inicial.
    ///     </para>
    ///     <para>
    ///         Referência: <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#nonnullable-reference-not-initialized">
    ///                         Microsoft.Learn
    ///                     </a>
    ///     </para>
    /// </remarks>
    [JsonPropertyName("authors")]
    public string? Authors { get; set; }

    [JsonPropertyName("publishedDate")]
    public string PublishedDate { get; set; } = string.Empty;

    [JsonPropertyName("pageCount")]
    public int PageCount { get; set; }

    [JsonPropertyName("categorie")]
    public string Categorie { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}