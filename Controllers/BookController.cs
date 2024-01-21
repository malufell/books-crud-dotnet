using BooksWebApi.Data.VO;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookService) : Controller
{
  private IBookService _bookService = bookService;

  [HttpGet("")]
  public IActionResult Get()
  {
    return Ok(_bookService.FindAll());
  }

  [HttpGet("{id}")]
  public IActionResult Get(long id)
  {
    var book = _bookService.FindByID(id);

    if (book == null) return NotFound();

    return Ok(book);
  }

  [HttpGet("findBookByName")]
  public IActionResult Get([FromQuery] string firstName, [FromQuery] string? lastName)
  {
    var book = _bookService.FindByName(firstName, lastName);

    if (book == null) return NotFound();

    return Ok(book);
  }

  [HttpPost]
  public IActionResult Create([FromBody] BookVO book)
  {

    if (book == null) return BadRequest();

    return Ok(_bookService.Create(book));
  }

  [HttpPut]
  public IActionResult Update([FromBody] BookVO book)
  {

    if (book == null) return BadRequest();

    return Ok(_bookService.Update(book));
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(long id)
  {
    var book = _bookService.FindByID(id);

    if (book == null) return NotFound();

    _bookService.Delete(id);

    return NoContent();
  }
}