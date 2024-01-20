using BooksWebApi.Controllers;
using BooksWebApi.Data.Converter.Implementations;
using BooksWebApi.Data.VO;
using BooksWebApi.Repository;

namespace BooksWebApi.Services.Implementations;

public class BookServiceImplementation(IRepository<Book> repository, BookConverter converter) : IBookService
{

  /*
	a ideia é que o client possa enviar um objeto que não necessariamente estará estruturado igual ao formato do objeto no banco de dados.
	por isso recebo um 'bookVO' e faço a conversão para um objeto na mesma estrutura da entidade (mesmo formato que o banco espera)
	*/
  private readonly BookConverter _converter = converter;
  private readonly IRepository<Book> _repository = repository;

  public List<BookVO> FindAll()
  {
    return _converter.Parse(_repository.FindAll());
  }

  public BookVO FindByID(long id)
  {
    return _converter.Parse(_repository.FindByID(id));
  }

  public BookVO Create(BookVO book)
  {
    /*
				recebo objeto do client em formato diferente do que eu espero (VO)
				faço a conversão do objeto para o formato que o banco espera
				salvo o objeto no banco
				faço a conversão de volta para VO para retornar ao client a mesma estrutura que ele havia me enviado
		*/
    var bookEntity = _converter.Parse(book);
    bookEntity = _repository.Create(bookEntity);
    return _converter.Parse(bookEntity);
  }

  public BookVO Update(BookVO book)
  {
    var bookEntity = _converter.Parse(book);
    bookEntity = _repository.Update(bookEntity);
    return _converter.Parse(bookEntity);
  }

  public void Delete(long id)
  {
    _repository.Delete(id);
  }
}