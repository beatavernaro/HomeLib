namespace HomeLib.Domain.Dto;

/// <summary>
/// Representa os dados do livro proveniente da Api do Google
/// </summary>
public class GoogleBookResponse
{
    public long TotalItems { get; set; }

    public List<Item>? Items { get; set; }
    
    /// <summary>
    /// Recupera sempre os dados do primeiro volume encontrado.
    /// </summary>
    /// <remarks>
    /// Criei esse item para ilustrar a possibilidade de criar facilitadores no seu código, a fim de evitar
    /// redundâncias. Se em todos os locais, o que me importa é sempre [0].Propriedade, então já deixo isso dentro
    /// da classe. O ponto aqui é demonstrar apenas uma das várias forma de se atingir o mesmo feito.
    /// </remarks>
    [JsonIgnore]
    public VolumeInfo? FirstVolumeInfo => Items?[0].VolumeInfo;
}

/// <summary>
/// Representa um volume.
/// </summary>
/// <remarks>
/// <para>
/// Removi o modificador "partial", pois não há necessidade nesse contexto. Na real, é preferível evitá-lo, pois permite
/// segregar uma mesma classe em arquivos separados, causando uma má organização e excesso de responsabilidade.
/// </para>
/// <para>
/// É sempre importante manter as classes limpas e devidamente em seu domínio, por isso, dado a sua natureza (receber
/// dados do Google). 
/// </para>
/// <para>
///         Referência: <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract">
///                         Microsoft.Learn
///                     </a>
///     </para>
/// </remarks>
public class Item
{
    public string? Id { get; set; }
    public VolumeInfo? VolumeInfo { get; set; }
}

public class VolumeInfo
{
    public string? Isbn { get; set; }
    
    public string? Title { get; set; }

    public List<string?>? Authors { get; set; }

    public string? PublishedDate { get; set; }

    public int? PageCount { get; set; }

    public List<string>? Categories { get; set; }

    public string? Language { get; set; }

    public string? Description { get; set; }

    
    /// <summary>
    /// Recupera sempre os dados do primeiro autor encontrado.
    /// </summary>
    [JsonIgnore]
    public string? FirstAuthor => Authors?[0];
    
    /// <summary>
    /// Recupera sempre os dados da primeira categoria.
    /// </summary>
    [JsonIgnore]
    public string? FirstCategory => Categories?[0];
}