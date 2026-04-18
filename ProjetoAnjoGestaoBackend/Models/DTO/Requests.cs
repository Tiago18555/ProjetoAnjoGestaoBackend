using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAnjoGestaoBackend.Models.DTO;

public record ServicoDTO(
    [property: JsonPropertyName("modelo_carro")] [property: Required] string ModeloCarro,
    [property: JsonPropertyName("nome_cliente")] [property: Required] string NomeCliente,
    [property: JsonPropertyName("nome_loja")] [property: Required] string NomeLoja,
    [property: JsonPropertyName("placa_veiculo")] [property: Required] string PlacaVeiculo,
    [property: JsonPropertyName("valor_total")]  [property: Required] Decimal ValorTotal,
    [property: JsonPropertyName("lista_servicos")] [property: Required] List<TipoDeServicoDTO> ListaServicos
);

public record LojaDTO(
    [property: JsonPropertyName("nome")] [property: Required] string Nome
);

public record TipoDeServicoDTO(
    [property: JsonPropertyName("nome")] [property: Required] string Nome,
    [property: JsonPropertyName("preco")] [property: Required] Decimal Preco
);