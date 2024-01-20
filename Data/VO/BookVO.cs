using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebApi.Data.VO;

[Table("books")]
public class BookVO
{
  public long Id { get; set; }

  public string Author { get; set; }

  public DateTime LaunchDate { get; set; }

  public decimal Price { get; set; }

  public string Title { get; set; }
}

/*
exemplo de serialização dos dados

using System.Text.Json.Serialization;
public class BookVO
{
  [JsonPropertyName("code")]
  public long Id { get; set; }

  [JsonPropertyName("name")]
  public string Author { get; set; }

  [JsonPropertyName("date")]
  public DateTime LaunchDate { get; set; }

  [JsonIgnore] > não vai retornar esse campo
  public decimal Price { get; set; }

  [JsonPropertyName("book_name")]
  public string Title { get; set; }
}
*/