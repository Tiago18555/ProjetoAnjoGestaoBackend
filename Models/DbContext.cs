using Microsoft.EntityFrameworkCore;
namespace ProjetoAnjoGestaoBackend.Models;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Servico> Servicos => Set<Servico>();
    public DbSet<Loja> Lojas => Set<Loja>();
    public DbSet<TipoDeServico> TiposDeServico => Set<TipoDeServico>();
}