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

        public async Task<Partida?> ObterPorIdAsync(Guid id) =>
            await _ctx.Partidas.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AdicionarAsync(Partida p) =>
            await _ctx.Partidas.AddAsync(p);

        public Task AtualizarAsync(Partida p)
        { _ctx.Partidas.Update(p); return Task.CompletedTask; }

        public Task RemoverAsync(Partida p)
        { _ctx.Partidas.Remove(p); return Task.CompletedTask; }

        public IQueryable<Partida> Query() => _ctx.Partidas.AsQueryable();
    }
}
