using DotNet9API.Data;
using DotNet9API.Models;
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
    return TypedResults.Ok();
}).WithName("CreateBook");

app.Run();

public record CreateBookRequest(string Title, string Isbn);