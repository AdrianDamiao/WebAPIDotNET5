using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext { // Classe padrao do .NET
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ 

    }

} 