using BattleHub.Application.ViewModels;

namespace BattleHub.Application.Interfaces
{
    public interface IPartidaAppService
    {
        Task<IEnumerable<PartidaViewModel>> ListarPorTorneioAsync(Guid torneioId, CancellationToken ct = default);
        Task ReportarVencedorAsync(Guid partidaId, Guid vencedorId, string placar, CancellationToken ct = default);
    }
}
