namespace HomeLib.Domain.Entidades;

/// <summary>
/// Representa os dados do livro.
/// </summary>
/// <remarks>
/// <para>
/// Perceba que removi os atributos <see cref="JsonPropertyNameAttribute"/>, pois o resultado que você espera pode ser
/// feito automaticamente através de uma configuração na serialização. Você verá mais detalhes sobre isso na Application.
/// A ideia é só decorar quando for estritamente necessário, como por exemplo, ignorar uma propriedade na
/// serialização/desserialização.
/// </para>
/// <para>
/// Tomei a liberdade de remover o <see cref="System.Text.Json"/> nativo e trocar pelo pacote Newtonsoft. Embora o
/// serializador nativo seja ótimo em muitos aspectos, ele ainda é um componente em constante evolução para alcançar todos
/// os feitos do "concorrente". 
/// </para>
/// <para>
/// Você pode acompanhar esse roadmap clicando
/// <a href="https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/migrate-from-newtonsoft?pivots=dotnet-6-0#table-of-differences-between-newtonsoftjson-and-systemtextjson">
///     aqui.
/// </a>
/// </para> 
/// </remarks>
public class Book : EntidadeBase
{
    public string? Isbn { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    
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
    public string? Authors { get; set; }

    public string? PublishedDate { get; set; } = string.Empty;

    public int? PageCount { get; set; }

    public string? Categorie { get; set; } = string.Empty;

    public string? Language { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}