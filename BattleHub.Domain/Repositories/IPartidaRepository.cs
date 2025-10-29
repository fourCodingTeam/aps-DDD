using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IPartidaRepository
    {
        Task<Partida?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Partida p);
        Task AtualizarAsync(Partida p);
        Task RemoverAsync(Partida p);
        IQueryable<Partida> Query();
    }
}
