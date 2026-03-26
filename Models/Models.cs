namespace ProjetoAnjoGestaoBackend.Models;

public class Loja {
    public int Id { get; set; }
    public required string Nome { get; set; }
}

public class TipoDeServico {
    public int Id { get; set; }
    public required string Nome { get; set; }
    public Decimal Preco { get; set; }
}

public class Servico {
    public int Id { get; set; }
    public required string NomeCliente { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public Decimal ValorTotal { get; set; }

    public required string ModeloCarro { get; set; }
    public required string PlacaVeiculo { get; set; }

    public int LojaId { get; set; }
    public Loja? Loja { get; set; }
    
    public List<TipoDeServico> ListaServicos { get; set; } = [];
}