
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebApi.Model.Base;

public class BaseEntity
{
  [Column("id")]
  public long Id { get; set; }
}