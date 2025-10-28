using BattleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleHub.Infrastructure.Persistence.Configs
{
    public class PartidaConfig : IEntityTypeConfiguration<Partida>
    {
        public void Configure(EntityTypeBuilder<Partida> b)
        {
            b.ToTable("Partidas");
            b.HasKey(x => x.Id);

            b.Property(x => x.TorneioId).IsRequired();
            b.Property(x => x.Rodada).IsRequired();

            b.Property(x => x.ParticipanteAId);
            b.Property(x => x.ParticipanteBId);

            b.Property(x => x.AgendadaPara);
            b.Property(x => x.Estado).HasConversion<int>().IsRequired();

            // Resultado (VO simples -> 2 colunas)
            b.OwnsOne(x => x.Resultado, vo =>
            {
                vo.Property(p => p.VencedorId).HasColumnName("VencedorId");
                vo.Property(p => p.Placar).HasColumnName("Placar").HasMaxLength(50);
            });

            b.HasIndex(x => new { x.TorneioId, x.Rodada });
        }
    }
}
