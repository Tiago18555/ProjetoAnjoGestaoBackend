using Microsoft.EntityFrameworkCore;
using ProjetoAnjoGestaoBackend.Models;
using ProjetoAnjoGestaoBackend.Models.DTO;

namespace ProjetoAnjoGestaoBackend.EndPoints;

public static class Endpoints {

    public static void MapServicoEndpoints(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("/");

        group.MapGet("/lojas", async (AppDbContext db) => 
            await db.Lojas
                .Select(l => new LojaDTO
                (
                    l.Nome
                ))
                .AsNoTracking()
                .ToListAsync());   

        group.MapGet("/tipodeservico", async (AppDbContext db) => 
            await db.TiposDeServico
                .Select(ts => new TipoDeServicoDTO
                (
                    ts.Nome,
                    ts.Preco
                ))
                .AsNoTracking()
                .ToListAsync());     

        group.MapPost("/", async (ServicoDTO servico, AppDbContext db) =>
        {
            //ADICIONAR CARRO (UPSERT)
            //ADICIONAR LOJA

            //db.TiposDeServico.Add(servico);
            //await db.SaveChangesAsync();

            //return Results.Created($"/servicos/{servico.Id}", servico);
        });
    }
}