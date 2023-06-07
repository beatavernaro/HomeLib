namespace HomeLib.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody]Book book)
    {
        try
        {
            await BookService.SaveBookAsync(book);
            return Ok("Livro cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Não foi possível cadastrar o livro. Motivo: {ExMessage}", ex.Message);
            //Estou mantendo o UnprocessableEntity para todas as situações por enquanto.
            //Futuramente, iremos tratar dos StatusCodes corretos.
            return UnprocessableEntity(ex.Message);
        }
    }
}
