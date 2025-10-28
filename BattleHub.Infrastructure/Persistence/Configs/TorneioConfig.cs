using BattleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleHub.Infrastructure.Persistence.Configs
{
    public class TorneioConfig : IEntityTypeConfiguration<Torneio>
    {
        public void Configure(EntityTypeBuilder<Torneio> b)
        {
            b.ToTable("Torneios");
            b.HasKey(x => x.Id);

            // Nome (VO -> coluna única)
            b.OwnsOne(x => x.Nome, vo =>
            {
                vo.Property(p => p.Valor).HasColumnName("Nome").HasMaxLength(120).IsRequired();
            });

            // TamanhoChave (VO -> int)
            b.OwnsOne(x => x.TamanhoChave, vo =>
            {
                vo.Property(p => p.Valor).HasColumnName("TamanhoChave").IsRequired();
            });

            // PeriodoDatas (VO -> duas colunas)
            b.OwnsOne(x => x.Periodo, vo =>
            {
                vo.Property(p => p.Inicio).HasColumnName("DataInicio").IsRequired();
                vo.Property(p => p.Fim).HasColumnName("DataFim").IsRequired();
            });

            // Enum como int
            b.Property(x => x.Estado).HasConversion<int>().IsRequired();

            // Relacionamentos (1->N) - agregado controla consistência
            b.HasMany(x => x.Inscricoes)
                .WithOne()
                .HasForeignKey(i => i.TorneioId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Partidas)
                .WithOne()
                .HasForeignKey(p => p.TorneioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice útil para busca
            b.HasIndex("Nome");
        }
    }
}
