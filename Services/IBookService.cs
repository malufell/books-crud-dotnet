namespace BooksWebApi.Controllers;

public interface IBookService
{
    Book Create(Book book);

    Book FindByID(long id);

    List<Book> FindAll();

    Book Update(Book book);

    void Delete(long id);
}