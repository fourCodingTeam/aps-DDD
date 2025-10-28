using BattleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleHub.Infrastructure.Persistence.Configs
{
    public class InscricaoConfig : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> b)
        {
            b.ToTable("Inscricoes");
            b.HasKey(x => x.Id);

            b.Property(x => x.TorneioId).IsRequired();
            b.Property(x => x.ParticipanteId).IsRequired();
            b.Property(x => x.CriadoEm).IsRequired();

            // Restringe dupes no mesmo torneio
            b.HasIndex(x => new { x.TorneioId, x.ParticipanteId }).IsUnique();
        }
    }
}
