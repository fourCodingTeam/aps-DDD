using BattleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Infraestrutura.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    public DbSet<Torneio> Torneios => Set<Torneio>();
    public DbSet<Participante> Participantes => Set<Participante>();
    public DbSet<Inscricao> Inscricoes => Set<Inscricao>();
    public DbSet<Partida> Partidas => Set<Partida>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
