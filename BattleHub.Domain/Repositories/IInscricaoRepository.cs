using BattleHub.Domain.Entities;

namespace BattleHub.Domain.Repositories
{
    public interface IInscricaoRepository
    {
        Task<Inscricao?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Inscricao i);
        Task RemoverAsync(Inscricao i);
        IQueryable<Inscricao> Query();
    }
}
