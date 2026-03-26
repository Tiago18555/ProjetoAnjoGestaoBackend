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
                    ts.Id,
                    ts.Nome,
                    ts.Preco
                ))
                .AsNoTracking()
                .ToListAsync());

        group.MapGet("/servicos", async (AppDbContext db) => 
            await db.Servicos
                .Include(s => s.Loja)
                .Include(s => s.ListaServicos)
                .Select(s => new ServicoResponseDTO
                (
                    s.Loja != null ? s.Loja.Nome : "Loja não informada",
                    s.ModeloCarro,                                     
                    s.PlacaVeiculo,                                    
                    s.NomeCliente,                                     
                    s.Data,                                            
                    s.ListaServicos.Select(t => new TipoDeServicoDTO(  
                        t.Id, 
                        t.Nome, 
                        t.Preco
                    )).ToList(),
                    s.ValorTotal
                ))
                .AsNoTracking()
                .ToListAsync()); 

     
        group.MapPost("/", async (ServicoDTO dto, AppDbContext db) =>
        {
            var loja = await db.Lojas.FirstOrDefaultAsync(l => l.Nome == dto.NomeLoja) 
                    ?? new Loja { Nome = dto.NomeLoja };

            var novosItens = dto.ListaServicos.Select(itemDto => new TipoDeServico
            {
                Nome = itemDto.Nome,
                Preco = itemDto.Preco
            }).ToList();

            var novoServico = new Servico
            {
                NomeCliente = dto.NomeCliente,
                ModeloCarro = dto.ModeloCarro,
                PlacaVeiculo = dto.PlacaVeiculo,
                ValorTotal = dto.ValorTotal,
                Data = DateTime.UtcNow,
                Loja = loja,
                ListaServicos = novosItens
            };

            db.Servicos.Add(novoServico);
            await db.SaveChangesAsync();

            return Results.Created($"/servicos/{novoServico.Id}", novoServico);
        });
    }    
}