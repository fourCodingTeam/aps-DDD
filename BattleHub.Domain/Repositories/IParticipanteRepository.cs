using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IParticipanteRepository
    {
        Task<Participante?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Participante p);
        Task AtualizarAsync(Participante p);
        Task RemoverAsync(Participante p);
        IQueryable<Participante> Query();
    }
}
