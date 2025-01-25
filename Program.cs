using System.ComponentModel.DataAnnotations;
using BookInventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var books = new List<Book> { };

app.MapGet("/books", () =>
{
    return books;
});

app.MapGet("/books/{id}", ([FromRoute] int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Book does not exist");

    return Results.Ok(book);
});

app.MapPost("/books", ([FromBody] Book book) =>
{
    var validationResults = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(book, new ValidationContext(book), validationResults, false);

    if (!isValid)
        return Results.BadRequest(validationResults[0].ErrorMessage);

    var newBook = new Book
    {
        Title = book.Title,
        Author = book.Author,
        Price = book.Price,
        Genre = book.Genre
    };

    books.Add(newBook);
    newBook.Id = books.IndexOf(newBook) + 1;

    return Results.Ok(newBook);
});

app.MapPut("books/{id}", ([FromRoute] int id, [FromBody] Book bookUpdate) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Book does not exist");

    book.Title = bookUpdate.Title;
    book.Author = bookUpdate.Author;
    book.Price = bookUpdate.Price;
    book.Genre = bookUpdate.Genre;

    var validationResults = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(book, new ValidationContext(book), validationResults, false);

    if (!isValid)
        return Results.BadRequest(validationResults[0].ErrorMessage);

    return Results.Ok(book);
});

app.MapDelete("books/{id}", ([FromRoute] int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Book does not exist");

    books.Remove(book);

    return Results.Ok("Book deleted");
});

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.Run();