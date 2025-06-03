using BookApi.Models;
using BookApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure settings from appsettings.json
builder.Services.Configure<BookstoreDatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<BooksService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
