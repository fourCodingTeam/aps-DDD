using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface ITorneioRepository
    {
        Task<Torneio?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Torneio t);
        Task AtualizarAsync(Torneio t);
        Task RemoverAsync(Torneio t);
        IQueryable<Torneio> Query();
    }
}
