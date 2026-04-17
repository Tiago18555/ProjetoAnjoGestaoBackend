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



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseNpgsql(connectionString));

var app = builder.Build();

// --- CONFIGURAÇÃO SWAGGER (Middleware) ---
// No Docker, você pode querer habilitar o Swagger mesmo fora de "Development" 
// para facilitar os testes iniciais. Se quiser apenas em Dev, use: if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Anjo Gestão v1");
    c.RoutePrefix = string.Empty;
});

//app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
    DbSeeder.Seed(db);
}

app.MapServicoEndpoints();
app.Run();