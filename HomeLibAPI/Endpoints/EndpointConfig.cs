using HomeLibAPI.Data;
using HomeLibAPI.Model;

namespace HomeLibAPI.Endpoints;

public static class EndpointConfig
{

    public static void AddEndpoint(WebApplication app)
    {
        #region GET
        app.MapGet("/api/books", (AppDbContext context) =>
        {
            var orderBooks = (from book in context.BookModels
                              orderby book.Title
                              select book).ToList();

            if (orderBooks.Count == 0) return Results.NoContent();

            return Results.Ok(orderBooks);
        });

        #endregion

        #region POST
        app.MapPost("/api", (AppDbContext context, BookModel book) =>
        {
            context.BookModels.Add(book);
            context.SaveChanges();
            return Results.Created($"Created at Id: {book.Id}", book);
        });

        #endregion

        #region PUT
        app.MapPut("/api/{id}", (AppDbContext context, BookModel book, Guid id) =>
        {
            var bookToUpdate = context.BookModels.Find(id);
            if (bookToUpdate is null) return Results.NotFound();

            bookToUpdate.Title = book.Title;

            context.SaveChanges();
            return Results.Ok(bookToUpdate);
        });
        #endregion

        #region DELETE
        app.MapDelete("/api/{id}", (Guid id, AppDbContext context) =>
        {
            var bookToDelete = context.BookModels.Find(id);

            if (bookToDelete is null) return Results.NotFound();

            _ = context.BookModels.Remove(bookToDelete);
            context.SaveChanges();

            return Results.Ok();
        });
        #endregion
    }

}
