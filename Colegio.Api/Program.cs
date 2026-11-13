using Microsoft.EntityFrameworkCore;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Contexto;
using Colegio.Infraestructura.Interfaces;
using Colegio.Infraestructura.Repositorios;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de gestión del colegio",
        Version = "v1",
        Description = "API para gestionar cursos, departamentos, estudiantes y docentes."
    });
});
var connectionString = builder.Configuration.GetConnectionString("Colegio") ?? "Data Source=colegio-local.db";
builder.Services.AddDbContext<ContextoColegio>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IRepositorioCursos, RepositorioCursos>();
builder.Services.AddScoped<IRepositorioDepartamentos, RepositorioDepartamentos>();
builder.Services.AddScoped<IRepositorioEstudiantes, RepositorioEstudiantes>();
builder.Services.AddScoped<IRepositorioDocentes, RepositorioDocentes>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API del colegio v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "API del colegio | Swagger";
    options.DisplayRequestDuration();
});

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ContextoColegio>();
    await context.Database.EnsureCreatedAsync();
    if (!await context.Departamentos.AnyAsync())
    {
        context.Departamentos.Add(new Departamento { Nombre = "General", Descripcion = "Departamento inicial" });
        await context.SaveChangesAsync();
    }
}


app.Run();
