using System;
using System.Linq;
using System.Threading.Tasks;
using BattleHub.Infraestrutura.Persistence;
using BattleHub.Domain.Entities;

namespace BattleHub.Infraestrutura.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            // se já houver torneios, assume que já seedou
            if (db.Torneios.Any()) return;

            // criar participantes
            var p1 = new Participante { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Nome = "Alice", Email = "alice@example.com" };
            var p2 = new Participante { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Nome = "Bob", Email = "bob@example.com" };
            var p3 = new Participante { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Nome = "Carla", Email = "carla@example.com" };

            db.Participantes.AddRange(p1, p2, p3);

            // criar torneio
            var torneioId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var torneio = new Torneio(
                /* supondo construtor (Nome, TamanhoChave, PeriodoDatas) */ 
                // se não tiver construtor público, use propriedades conforme sua entidade
            );

            // Se sua entidade Torneio não tiver construtor que aceite parâmetros, crie o objeto "manual":
            torneio.Id = torneioId;
            torneio.Nome = "Torneio Demo";
            torneio.TamanhoChave = 8;
            torneio.DataInicio = DateTime.UtcNow.Date;
            torneio.DataFim = DateTime.UtcNow.Date.AddDays(7);
            torneio.Estado = 0; // ajuste conforme enum/valores do seu domínio

            db.Torneios.Add(torneio);

            // criar inscrições ligando participantes ao torneio
            db.Inscricoes.AddRange(
                new Inscricao { Id = Guid.NewGuid(), TorneioId = torneioId, ParticipanteId = p1.Id, CriadoEm = DateTime.UtcNow },
                new Inscricao { Id = Guid.NewGuid(), TorneioId = torneioId, ParticipanteId = p2.Id, CriadoEm = DateTime.UtcNow },
                new Inscricao { Id = Guid.NewGuid(), TorneioId = torneioId, ParticipanteId = p3.Id, CriadoEm = DateTime.UtcNow }
            );

            await db.SaveChangesAsync();
        }
    }
}
