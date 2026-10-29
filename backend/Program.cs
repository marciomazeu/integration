var builder = WebApplication.CreateBuilder(args);
// Adicione a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5012/") 
});
var app = builder.Build();
// Habilite o CORS
app.UseCors("AllowAll");
// Endpoint simples retornando uma lista de produtos
app.MapGet("/api/products", () =>
{
    var products = new[]
    {
        new { Id = 1, Name = "Teclado Mecânico", Price = 250.00 },
        new { Id = 2, Name = "Mouse Gamer", Price = 120.00 },
        new { Id = 3, Name = "Monitor 24", Price = 800.00 }
    };

    return Results.Ok(products);
});

app.Run();