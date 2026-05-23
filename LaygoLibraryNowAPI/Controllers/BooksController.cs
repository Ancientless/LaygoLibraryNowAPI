
using LaygoLibraryNowAPI.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private static List<Books> books = new List<Books>
    {
         new Books { Id = 1, Title = "The Hunger Games", Author = "Suzanne Collins", Genre = "NONFICTION", Available = true, PublishedYear = 2016 },
         new Books { Id = 2, Title = "Harry Potter and the Sorcerer's Stone", Author = "J. K. Rowling", Genre = "FICTION", Available = true, PublishedYear = 2017 }
    };

    [HttpGet] // --- ADD THIS ---
    public IActionResult getAll()
    {
        return Ok(new { status = "success", data = books, message = "Books Retrieve" });
    }

    [HttpGet("{id}")] // --- ADD THIS WITH THE ID PARAMETER ---
    public IActionResult getById(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
        return Ok(new { status = "success", data = book, message = "Book retrieved" });
    }

    [HttpPost] // --- ADD THIS ---
    public IActionResult Create([FromBody] Books newbook)
    {
        newbook.Id = books.Count + 1;
        books.Add(newbook);
        return CreatedAtAction(nameof(getById), new { id = newbook.Id },
            new { status = "success", data = newbook, message = "Book Created" });
    }

    [HttpPut("{id}")] // --- ADD THIS ---
    public IActionResult Update(int id, [FromBody] Books updateBook)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

        book.Title = updateBook.Title;
        book.Author = updateBook.Author;
        book.Genre = updateBook.Genre;
        book.Available = updateBook.Available;
        book.PublishedYear = updateBook.PublishedYear;

        return Ok(new { status = "success", data = book, message = "Book Update" });
    }

    [HttpDelete("{id}")] // --- ADD THIS ---
    public IActionResult Delete(int id)
    {
        var book = books.FirstOrDefault(book => book.Id == id);
        if (book == null)
            return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

        books.Remove(book);
        return Ok(new { status = "success", data = (object?)null, message = "Books Deleted" });
    }
}