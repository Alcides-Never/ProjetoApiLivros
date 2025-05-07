using ProjetoLivros.Context;
using ProjetoLivros.Interface;
using ProjetoLivros.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Avisa que a aplicação usa Controllers
builder.Services.AddControllers();

// Adiciono o Gerador de Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LivrosContext>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

var app = builder.Build();

// Avisa o .NET que eu tenho Controladores
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapGet("/", () => "Hello World!");

app.Run();
