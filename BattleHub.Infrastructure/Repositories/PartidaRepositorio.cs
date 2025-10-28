using BattleHub.Domain.Entities;
using BattleHub.Domain.Repositories;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Infrastructure.Repositories
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly AppDbContext _ctx;
        public PartidaRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<Partida?> ObterPorIdAsync(Guid id, CancellationToken ct = default) =>
            await _ctx.Partidas.FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task AdicionarAsync(Partida p, CancellationToken ct = default) =>
            await _ctx.Partidas.AddAsync(p, ct);

        public Task AtualizarAsync(Partida p, CancellationToken ct = default)
        { _ctx.Partidas.Update(p); return Task.CompletedTask; }

        public Task RemoverAsync(Partida p, CancellationToken ct = default)
        { _ctx.Partidas.Remove(p); return Task.CompletedTask; }

        public IQueryable<Partida> Query() => _ctx.Partidas.AsQueryable();
    }
}
