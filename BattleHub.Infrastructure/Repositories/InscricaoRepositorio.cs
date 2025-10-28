using BattleHub.Domain.Entities;
using BattleHub.Domain.Repositories;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Infrastructure.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly AppDbContext _ctx;
        public InscricaoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<Inscricao?> ObterPorIdAsync(Guid id, CancellationToken ct = default) =>
            await _ctx.Inscricoes.FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task AdicionarAsync(Inscricao i, CancellationToken ct = default) =>
            await _ctx.Inscricoes.AddAsync(i, ct);

        public Task RemoverAsync(Inscricao i, CancellationToken ct = default)
        { _ctx.Inscricoes.Remove(i); return Task.CompletedTask; }

        public IQueryable<Inscricao> Query() => _ctx.Inscricoes.AsQueryable();
    }
}
