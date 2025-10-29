using BattleHub.Domain.Entities;
using BattleHub.Domain.Repositories;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Infrastructure.Repositories
{
    public class TorneioRepository : ITorneioRepository
    {
        private readonly AppDbContext _ctx;
        public TorneioRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Torneio?> ObterPorIdAsync(Guid id)
        {
            return await _ctx.Torneios
                .Include(t => t.Inscricoes)
                .Include(t => t.Partidas)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AdicionarAsync(Torneio t)
        {
            await _ctx.Torneios.AddAsync(t);
        }

        public Task AtualizarAsync(Torneio t)
        { 
            _ctx.Torneios.Update(t); return Task.CompletedTask; 
        }

        public Task RemoverAsync(Torneio t)
        { 
            _ctx.Torneios.Remove(t); return Task.CompletedTask;
        }

        public IQueryable<Torneio> Query() => _ctx.Torneios.AsQueryable();
    }
}
