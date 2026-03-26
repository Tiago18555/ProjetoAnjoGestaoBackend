using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAnjoGestaoBackend.Models.DTO;

public record ServicoDTO(
    [property: JsonPropertyName("nome_loja")] [property: Required] string NomeLoja,
    [property: JsonPropertyName("carro_modelo")] [property: Required] string ModeloCarro,
    [property: JsonPropertyName("carro_placa")] [property: Required] string PlacaVeiculo,
    [property: JsonPropertyName("nome_cliente")] [property: Required] string NomeCliente,
    [property: JsonPropertyName("lista_servicos")] [property: Required] List<TipoDeServicoDTO> ListaServicos,
    [property: JsonPropertyName("valor_total")]  [property: Required] Decimal ValorTotal
);