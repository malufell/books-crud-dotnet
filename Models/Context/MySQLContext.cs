using BooksWebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BooksWebApi.Models.Context;

public class MySQLContext : DbContext {
    public MySQLContext() {}

    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

    public DbSet<Book> Books { get; set; }

}