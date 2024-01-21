using BooksWebApi.Controllers;

namespace BooksWebApi.Repository;

public interface IBookRepository : IRepository<Book>
{
  List<Book> FindByName(string firstName, string? lastName);
}
