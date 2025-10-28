using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IInscricaoRepository
    {
        Task<Inscricao?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task AdicionarAsync(Inscricao i, CancellationToken ct = default);
        Task RemoverAsync(Inscricao i, CancellationToken ct = default);
        IQueryable<Inscricao> Query();
    }
}
