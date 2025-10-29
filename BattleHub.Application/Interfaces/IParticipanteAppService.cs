using BattleHub.Application.ViewModels;

namespace BattleHub.Application.Interfaces
{
    public interface IParticipanteAppService
    {
        Task<IEnumerable<ParticipanteViewModel>> ListarAsync();
        Task<ParticipanteViewModel?> ObterPorIdAsync(Guid id);
        Task CriarAsync(ParticipanteViewModel model);
        Task AtualizarAsync(ParticipanteViewModel model);
        Task RemoverAsync(Guid id);
    }
}
