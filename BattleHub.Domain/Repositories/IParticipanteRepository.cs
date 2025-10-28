using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IParticipanteRepository
    {
        Task<Participante?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task AdicionarAsync(Participante p, CancellationToken ct = default);
        Task AtualizarAsync(Participante p, CancellationToken ct = default);
        Task RemoverAsync(Participante p, CancellationToken ct = default);
        IQueryable<Participante> Query();
    }
}
