using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAnjoGestaoBackend.Models.DTO;

public record LojaDTO(
    [property: JsonPropertyName("nome")] [property: Required] string Nome
);

public record TipoDeServicoDTO(
    [property: JsonPropertyName("nome")] [property: Required] string Nome,
    [property: JsonPropertyName("preco")] int Preco
);
