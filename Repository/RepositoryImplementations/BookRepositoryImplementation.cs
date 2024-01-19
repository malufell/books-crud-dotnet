using BooksWebApi.Models.Context;
using BooksWebApi.Controllers;

namespace BooksWebApi.Repository.Implementations;

public class BookRepositoryImplementation(MySQLContext context) : IBookRepository
{

  private MySQLContext _context = context;

  public List<Book> FindAll()
  {
    return _context.Books.ToList();
  }

  public Book FindByID(long id)
  {
    return _context.Books.SingleOrDefault(param => param.Id.Equals(id));
  }

  public Book Create(Book book)
  {
    try
    {
      _context.Add(book);
      _context.SaveChanges();
    }
    catch (Exception)
    {
      throw;
    }

    return book;
  }

  public Book Update(Book book)
  {
    if (!Exists(book.Id)) return new Book();

    var result = _context.Books.SingleOrDefault(param => param.Id.Equals(book.Id));

    if (result != null)
    {
      try
      {
        _context.Entry(result).CurrentValues.SetValues(book);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        throw;
      }

    }

    return book;
  }

  public void Delete(long id)
  {
    var result = _context.Books.SingleOrDefault(param => param.Id.Equals(id));

    if (result != null)
    {
      try
      {
        _context.Books.Remove(result);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        throw;
      }

    }
  }

  public bool Exists(long id)
  {
    return _context.Books.Any(param => param.Id.Equals(id));
  }
}