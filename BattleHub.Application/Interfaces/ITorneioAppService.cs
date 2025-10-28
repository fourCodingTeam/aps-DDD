using BattleHub.Application.ViewModels;

namespace BattleHub.Application.Interfaces
{
    public interface ITorneioAppService
    {
        Task<IEnumerable<TorneioViewModel>> ListarAsync(CancellationToken ct = default);
        Task<TorneioViewModel?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task CriarAsync(TorneioViewModel model, CancellationToken ct = default);
        Task AtualizarAsync(TorneioViewModel model, CancellationToken ct = default);
        Task RemoverAsync(Guid id, CancellationToken ct = default);
    }
}
