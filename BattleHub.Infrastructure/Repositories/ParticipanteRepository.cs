using BattleHub.Domain.Entities;
using BattleHub.Domain.Repositories;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Infrastructure.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly AppDbContext _ctx;
        public ParticipanteRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<Participante?> ObterPorIdAsync(Guid id) =>
            await _ctx.Participantes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AdicionarAsync(Participante p) =>
            await _ctx.Participantes.AddAsync(p);

        public Task AtualizarAsync(Participante p)
        { _ctx.Participantes.Update(p); return Task.CompletedTask; }

        public Task RemoverAsync(Participante p)
        { _ctx.Participantes.Remove(p); return Task.CompletedTask; }

        public IQueryable<Participante> Query() => _ctx.Participantes.AsQueryable();
    }
}
