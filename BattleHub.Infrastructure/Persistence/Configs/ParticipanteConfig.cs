using BattleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleHub.Infrastructure.Persistence.Configs
{
    public class ParticipanteConfig : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> b)
        {
            b.ToTable("Participantes");
            b.HasKey(x => x.Id);

            b.OwnsOne(x => x.Nome, vo =>
            {
                vo.Property(p => p.Valor).HasColumnName("Nome").HasMaxLength(120).IsRequired();
            });

            b.OwnsOne(x => x.Email, vo =>
            {
                vo.Property(p => p.Valor).HasColumnName("Email").HasMaxLength(180).IsRequired();
            });

            b.HasIndex("Email").IsUnique();
        }
    }
}
