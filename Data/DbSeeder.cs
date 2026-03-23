using ProjetoAnjoGestaoBackend.Models;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        db.Database.EnsureCreated();

        try
        {
            if (!db.Lojas.Any())
            {
                var lojas = new List<Loja> 
                { 
                    new() { Nome = "Universound" },
                    new() { Nome = "Anjo - IE Auto" },
                    new() { Nome = "Rota do Som" }
                    
                };

                db.Lojas.AddRange(lojas);

                db.SaveChanges();
                System.Console.WriteLine("Lojas populadas");
            }

            if (!db.TiposDeServico.Any())
            {
                var servicos = new List<TipoDeServico>
                {
                    new() { Nome = "Carro sem para-brisa", Preco = 100 },
                    new() { Nome = "Carro completo", Preco = 150 },
                    new() { Nome = "Apenas Para-brisa", Preco = 50 },
                    new() { Nome = "Apenas Vidro Traseiro", Preco = 50 },
                    new() { Nome = "Serviço por Porta", Preco = 30 },
                    new() { Nome = "Envelopamento", Preco = 0 },
                    new() { Nome = "Extraordinário (Residencial/Arq)", Preco = 0 }
                };

                db.TiposDeServico.AddRange(servicos);

                db.SaveChanges();
                System.Console.WriteLine("Serviços Populados");
            }            
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
}