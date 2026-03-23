namespace ProjetoAnjoGestaoBackend.Models;

public class Loja {
    public int Id { get; set; }
    public required string Nome { get; set; }
}

public class TipoDeServico {
    public int Id { get; set; }
    public required string Nome { get; set; }
    public int Preco { get; set; }
}

public class Carro {
    public int Id { get; set; }
    public required string Modelo { get; set; }
    public required string Placa { get; set; }
}

public class Servico {
    public int Id { get; set; }
    public required string NomeCliente { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public int ValorTotal { get; set; }

    public int LojaId { get; set; }
    public Loja? Loja { get; set; }
    
    public int CarroId { get; set; }
    public Carro? Carro { get; set; }
    
    public List<TipoDeServico> Itens { get; set; } = [];
}