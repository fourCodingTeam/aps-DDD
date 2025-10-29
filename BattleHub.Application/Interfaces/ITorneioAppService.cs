using BattleHub.Application.ViewModels;

namespace BattleHub.Application.Interfaces
{
    public interface ITorneioAppService
    {
        Task<IEnumerable<TorneioViewModel>> ListarAsync();
        Task<TorneioViewModel?> ObterPorIdAsync(Guid id);
        Task CriarAsync(TorneioViewModel model);
        Task AtualizarAsync(TorneioViewModel model);
        Task RemoverAsync(Guid id);
        Task InscreverParticipanteAsync(Guid torneioId, Guid participanteId);
        Task PublicarAsync(Guid torneioId);
    }
}
