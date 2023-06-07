using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLib.Api.Controllers;

[ApiController]
[Route("[controller]")]
//Todas as apis, por padrão, vão apenas comunicar através de Json (sugestão)
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
//Todas as apis, por padrão, vão tratar esses dois tipos de retornos. Os específicos ficam em cada verbo.
[ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [SwaggerOperation("Post")]
    [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(int), description: "Cria o recurso Livro.")]
    public async Task<IActionResult> Post([FromBody]Book book)
    {
        try
        {
            var id = await BookService.SaveBookAsync(book);
            return CreatedAtAction(nameof(GetById), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Não foi possível cadastrar o livro. Motivo: {ExMessage}", ex.Message);
            //Estou mantendo o UnprocessableEntity para todas as situações por enquanto.
            //Futuramente, iremos tratar dos StatusCodes corretos.
            return UnprocessableEntity(ex.Message);
        }
    }
    
    [HttpGet(nameof(GetById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
    public async Task<IActionResult> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
