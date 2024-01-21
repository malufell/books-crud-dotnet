using BooksWebApi.Controllers;
using BooksWebApi.Models.Context;
using BooksWebApi.Repository.Generic;

namespace BooksWebApi.Repository;

public class BookRepository(MySQLContext context) : GenericRepository<Book>(context), IBookRepository
{
  private readonly MySQLContext _context = context;

  public List<Book> FindByName(string firstName, string? lastName = null)
  {
    var query = _context.Books.AsQueryable();

    if (!string.IsNullOrWhiteSpace(firstName))
    {
      query = query.Where(param => param.Title.Contains(firstName));
    }

    if (!string.IsNullOrWhiteSpace(lastName))
    {
      query = query.Where(param => param.Title.Contains(lastName));
    }

    return query.ToList();
  }
}