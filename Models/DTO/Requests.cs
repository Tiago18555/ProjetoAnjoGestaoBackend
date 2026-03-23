using System.Text.Json.Serialization;

namespace ProjetoAnjoGestaoBackend.Models.DTO;

public record ServicoDTO(
    [property: JsonPropertyName("nome_cliente")] string Nome,
    [property: JsonPropertyName("valor_total")] string ValorTotal,
    [property: JsonPropertyName("nome_loja")] string NomeLoja,
    [property: JsonPropertyName("carro_modelo")] string ModeloCarro,
    [property: JsonPropertyName("carro_placa")] string PlacaCarro,
    [property: JsonPropertyName("lista_servicos")] List<TipoDeServicoDTO> ListaServicos
);
