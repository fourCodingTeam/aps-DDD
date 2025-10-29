using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using BattleHub.Domain.Repositories;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Application.Services
{
    public class PartidaAppService: IPartidaAppService
    {
        private readonly AppDbContext _ctx;
        private readonly IPartidaRepository _partidas;
        private readonly IParticipanteRepository _participantes;

        public PartidaAppService(AppDbContext ctx, IPartidaRepository partidas, IParticipanteRepository participantes)
        {
            _ctx = ctx;
            _partidas = partidas;
            _participantes = participantes;
        }

        public async Task<IEnumerable<PartidaViewModel>> ListarPorTorneioAsync(Guid torneioId)
        {
            var partidas = await _partidas.Query()
                .Where(p => p.TorneioId == torneioId)
                .OrderBy(p => p.Rodada)
                .ToListAsync();

            var participantes = await _participantes.Query().ToListAsync();

            return partidas.Select(p => new PartidaViewModel
            {
                Id = p.Id,
                Rodada = p.Rodada,
                ParticipanteA = participantes.FirstOrDefault(x => x.Id == p.ParticipanteAId)?.Nome.Valor ?? "-",
                ParticipanteB = participantes.FirstOrDefault(x => x.Id == p.ParticipanteBId)?.Nome.Valor ?? "-",
                Estado = p.Estado.ToString(),
                Vencedor = p.Resultado?.VencedorId is Guid v
                    ? participantes.FirstOrDefault(x => x.Id == v)?.Nome.Valor
                    : null,
                Placar = p.Resultado?.Placar
            });
        }

        public async Task ReportarVencedorAsync(Guid partidaId, Guid vencedorId, string placar)
        {
            var partida = await _partidas.ObterPorIdAsync(partidaId)
                ?? throw new Exception("Partida não encontrada.");

            partida.ReportarResultado(vencedorId, placar);
            await _ctx.SaveChangesAsync();
        }
    }
}
