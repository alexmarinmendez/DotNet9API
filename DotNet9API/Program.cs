using DotNet9API.Data;
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

app.MapPost("/api/books", () =>
{

}).WithName("CreateBook");

app.Run();
