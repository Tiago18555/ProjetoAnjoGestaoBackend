using Microsoft.EntityFrameworkCore;
using ProjetoAnjoGestaoBackend.EndPoints;
using ProjetoAnjoGestaoBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => 
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Projeto Anjo Gestão API", 
        Version = "v1",
        Description = "API para gestão de serviços de oficina"
    });
});

if (builder.Environment.EnvironmentName != "Testing")
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseNpgsql(connectionString));
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Anjo Gestão v1");
    c.RoutePrefix = string.Empty;
});

//app.UseCors("AllowAll");

if (builder.Environment.EnvironmentName != "Testing")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();    
    db.Database.Migrate();

    DbSeeder.Seed(db);    
}

app.MapServicoEndpoints();
app.Run();

public partial class Program { }