using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAnjoGestaoBackend.Models;
using ProjetoAnjoGestaoBackend.Models.DTO;
using Xunit;

namespace ProjetoAnjoGestaoBackend.Tests;

public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public EndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DEFAULT");
                });
            });
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task PostLoja_DeveRetornarCreated_QuandoDadosSaoValidos()
    {
        // Arrange
        var novaLoja = new LojaDTO("Loja Matriz");

        // Act
        var response = await _client.PostAsJsonAsync("/lojas", novaLoja);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<LojaDTO>();
        Assert.Equal("Loja Matriz", resultado?.Nome);
    }

    [Fact]
    public async Task PostLoja_DeveRetornarConflict_QuandoLojaJaExiste()
    {
        // Arrange
        var dto = new LojaDTO("Loja Unica");
        
        // Força a criação da primeira
        var resp1 = await _client.PostAsJsonAsync("/lojas", dto);
        Assert.Equal(HttpStatusCode.Created, resp1.StatusCode);

        // Act - Tenta criar a mesma
        var resp2 = await _client.PostAsJsonAsync("/lojas", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, resp2.StatusCode);
    }

    [Fact]
    public async Task PostServico_DeveCriarServicoERetornarStatusCreated()
    {
        // Arrange
        var servicoDto = new ServicoDTO(
            "Fusca",          // ModeloCarro
            "Cliente Teste",  // NomeCliente
            "Loja Centro",    // NomeLoja
            "ABC-1234",       // PlacaVeiculo
            150.00m,          // ValorTotal
            new List<TipoDeServicoDTO> { new("Troca de Óleo", 150.00m) }
        );

        // Act
        var response = await _client.PostAsJsonAsync("/servicos", servicoDto);
        
        // Assert 1: O POST precisa funcionar
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Act 2: Buscar a lista para conferir
        var getResponse = await _client.GetAsync("/servicos");
        var lista = await getResponse.Content.ReadFromJsonAsync<List<ServicoResponseDTO>>();

        // Assert 2: Verificações detalhadas
        Assert.NotNull(lista);
        Assert.NotEmpty(lista);
        var servico = lista.First();
        Assert.Equal("Cliente Teste", servico.NomeCliente);
        Assert.Equal("Loja Centro", servico.NomeLoja);
        Assert.Equal("ABC-1234", servico.PlacaVeiculo);
    }
}