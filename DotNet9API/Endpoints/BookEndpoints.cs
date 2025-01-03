using DotNet9API.Contracts;
using DotNet9API.Data;
using DotNet9API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet9API.Endpoints
{
    public static class BookEndpoints
    {
        public static IEndpointRouteBuilder MapBookRoutes(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/books").WithTags("Books");

            group.MapPost("/", async (CreateBookRequest request, BooksContext context) =>
            {
                var newBook = new Book()
                {
                    Id = Guid.CreateVersion7(),
                    Title = request.Title,
                    Isbn = request.Isbn,
                };
                context.Books.Add(newBook);
                await context.SaveChangesAsync();
                return Results.CreatedAtRoute("GetBookById", new { newBook.Id }, newBook);
            }).WithName("CreateBook");

            group.MapGet("/{id:guid}", async (Guid id, BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
                return book is not null ? Results.Ok(book) : Results.NotFound();
            }).WithName("GetBookById");

            group.MapGet("/", async (BooksContext context) =>
            {
                var books = await context.Books.ToListAsync();
                return Results.Ok(books);
            }).WithName("GetBooks");

            group.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest request, BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
                if (book is null) return Results.NotFound();
                book.Title = request.Title;
                await context.SaveChangesAsync();
                return Results.Ok(book);
            }).WithName("UpdateBook");

            group.MapDelete("/{id:guid}", async (Guid id, BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
                if (book is null) return Results.NotFound();
                context.Books.Remove(book);
                await context.SaveChangesAsync();
                return Results.NoContent();
            }).WithName("DeleteBook");

            return app;
        }
    }
}
