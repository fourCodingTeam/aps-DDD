using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface ITorneioRepository
    {
        Task<Torneio?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task AdicionarAsync(Torneio t, CancellationToken ct = default);
        Task AtualizarAsync(Torneio t, CancellationToken ct = default);
        Task RemoverAsync(Torneio t, CancellationToken ct = default);
        IQueryable<Torneio> Query();
    }
}
