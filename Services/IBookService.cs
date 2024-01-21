using BooksWebApi.Data.VO;

namespace BooksWebApi.Controllers;

public interface IBookService
{
	BookVO Create(BookVO book);

	BookVO FindByID(long id);

	List<BookVO> FindByName(string firstName, string? lastName);

	List<BookVO> FindAll();

	BookVO Update(BookVO book);

	void Delete(long id);
}