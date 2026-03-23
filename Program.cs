using Microsoft.EntityFrameworkCore;
using ProjetoAnjoGestaoBackend.EndPoints;
using ProjetoAnjoGestaoBackend.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
    
    DbSeeder.Seed(db);
}

app.MapServicoEndpoints();
app.Run();