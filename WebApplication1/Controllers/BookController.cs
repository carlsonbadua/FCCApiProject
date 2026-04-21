using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly FirstAPIContext _context;
    public BookController(FirstAPIContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooks()
    {
        return Ok(await _context.Books.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        if (book is null)
        {
            return BadRequest();
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, Book updatedBook)
    {
        var existingBook = await _context.Books.FindAsync(id);

        if (existingBook is null)
        {
            return NotFound();
        }

        existingBook.Title = updatedBook.Title;
        existingBook.Author = updatedBook.Author;
        existingBook.YearPublished = updatedBook.YearPublished;

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookById), new { id = existingBook.Id }, existingBook); ;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var bookForDeletion = await _context.Books.FindAsync(id);

        if (bookForDeletion is null)
        {
            return NotFound();
        }

        _context.Books.Remove(bookForDeletion);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
