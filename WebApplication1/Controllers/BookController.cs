using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    static private List<Book> s_books = new List<Book>
    {
        new Book
        {
            Id = 1,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            YearPublished = 1925
        },
        new Book
        {
            Id = 2,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            YearPublished = 1960
        },
        new Book
        {
            Id = 3,
            Title = "1984",
            Author = "George Orwell",
            YearPublished = 1949
        },
        new Book         {
            Id = 4,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            YearPublished = 1813
        },
        new Book
        {
            Id = 5,
            Title = "The Catcher in the Rye",
            Author = "J.D. Salinger",
            YearPublished = 1951
        }
    };

    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        return Ok(s_books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = s_books.FirstOrDefault(b => b.Id == id);
        
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> CreateBook(Book book)
    {
        if (book is null)
        {
            return BadRequest();
        }

        s_books.Add(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        var existingBook = s_books.FirstOrDefault(b => b.Id == id);

        if (existingBook is null)
        {
            return NotFound();
        }

        existingBook.Title          = updatedBook.Title;
        existingBook.Author         = updatedBook.Author;
        existingBook.YearPublished  = updatedBook.YearPublished;

        return NoContent();
    }
}
