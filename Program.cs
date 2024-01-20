using BooksWebApi.Models.Context;
using BooksWebApi.Repository;
using BooksWebApi.Repository.Generic;
using BooksWebApi.Data.Converter.Implementations;
using BooksWebApi.Services.Implementations;
using BooksWebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using EvolveDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); 
}));

builder.Services.AddControllers();

// Database
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0, 36))
    )
);

if (builder.Environment.IsDevelopment())
{
  MigrateDatabase(connection);
}

// Dependency Injection
builder.Services.AddScoped<IBookService, BookServiceImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<BookConverter>();

builder.Services.AddMvc(options => {
  options.RespectBrowserAcceptHeader = true;
  options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
  options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
}).AddXmlSerializerFormatters();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseRouting();
app.UseCors(); // tem que colocar no lugar certo para funcionar 
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Book}/{action=Get}/{id?}");

app.Run();

void MigrateDatabase(string connection)
{
  try
  {
    var evolveConnection = new MySqlConnection(connection);
    var evolve = new Evolve(evolveConnection)

    {
      Locations = new List<string> { "db/migrations", "db/seeders" },
      IsEraseDisabled = true,
    };

    evolve.Migrate();
  }
  catch (Exception)
  {
    throw;
  }
}