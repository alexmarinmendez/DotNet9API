using DotNet9API.Data;
using DotNet9API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BooksContext>(options => options.UseInMemoryDatabase("BooksDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();

app.MapPost("/api/books", async (CreateBookRequest request, BooksContext context) =>
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

app.MapGet("/api/books/{id:guid}", async (Guid id, BooksContext context) =>
{
    var book = await context.Books.FindAsync(id);
    return TypedResults.Ok(book);
}).WithName("GetBookById");

app.Run();

public record CreateBookRequest(string Title, string Isbn);