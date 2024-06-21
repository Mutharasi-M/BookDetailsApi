using BookDetailsApi.Data;
using BookDetailsApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Set the default culture to invariant for the entire application
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

// Create a WebApplicationBuilder and configure services
var builder = WebApplication.CreateBuilder(args);

// Db Connection
builder.Services.AddDbContext<BooksApiDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("BookDetailsApiConnectionString"))
);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// repository declaration
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
