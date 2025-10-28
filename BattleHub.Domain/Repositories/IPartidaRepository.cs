using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IPartidaRepository
    {
        Task<Partida?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task AdicionarAsync(Partida p, CancellationToken ct = default);
        Task AtualizarAsync(Partida p, CancellationToken ct = default);
        Task RemoverAsync(Partida p, CancellationToken ct = default);
        IQueryable<Partida> Query();
    }
}
