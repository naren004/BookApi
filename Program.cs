using BookApi.Models;
using BookApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Load MongoDB settings
builder.Services.Configure<BookstoreDatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));

// Register services
builder.Services.AddSingleton<BooksService>();

// Enable CORS for Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy
            .WithOrigins("https://localhost:7291") // Blazor WebAssembly URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👇 Use CORS before any endpoints or authorization
app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
