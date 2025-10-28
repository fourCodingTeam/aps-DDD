using BattleHub.Application.ViewModels;

namespace BattleHub.Application.Interfaces
{
    public interface IParticipanteAppService
    {
        Task<IEnumerable<ParticipanteViewModel>> ListarAsync(CancellationToken ct = default);
        Task<ParticipanteViewModel?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task CriarAsync(ParticipanteViewModel model, CancellationToken ct = default);
        Task AtualizarAsync(ParticipanteViewModel model, CancellationToken ct = default);
        Task RemoverAsync(Guid id, CancellationToken ct = default);
    }
}
