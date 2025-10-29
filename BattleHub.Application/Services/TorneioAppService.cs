using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using BattleHub.Domain.Entities;
using BattleHub.Domain.Interfaces;
using BattleHub.Domain.Repositories;
using BattleHub.Domain.ValueObjects;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Application.Services
{
    public class TorneioAppService : ITorneioAppService
    {
        private readonly ITorneioRepository _torneios;
        private readonly AppDbContext _ctx;
        private readonly IParticipanteRepository _participantes;
        private readonly IChaveamento _chaveador;

        public TorneioAppService(
            ITorneioRepository torneios, 
            AppDbContext ctx,
            IParticipanteRepository participantes,
            IChaveamento chaveador)
        {
            _torneios = torneios;
            _ctx = ctx;
            _participantes = participantes;
            _chaveador = chaveador;
        }

        public async Task<IEnumerable<TorneioViewModel>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await _torneios.Query().ToListAsync(ct);
            return lista.Select(t => new TorneioViewModel
            {
                Id = t.Id,
                Nome = t.Nome.Valor,
                TamanhoChave = t.TamanhoChave.Valor,
                DataInicio = t.Periodo.Inicio,
                DataFim = t.Periodo.Fim,
                Estado = t.Estado.ToString()
            });
        }

        public async Task<TorneioViewModel?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            var t = await _torneios.ObterPorIdAsync(id, ct);
            if (t == null) return null;

            return new TorneioViewModel
            {
                Id = t.Id,
                Nome = t.Nome.Valor,
                TamanhoChave = t.TamanhoChave.Valor,
                DataInicio = t.Periodo.Inicio,
                DataFim = t.Periodo.Fim,
                Estado = t.Estado.ToString()
            };
        }

        public async Task CriarAsync(TorneioViewModel model, CancellationToken ct = default)
        {
            var nome = Nome.Criar(model.Nome);
            var tamanho = TamanhoChave.Criar(model.TamanhoChave);
            var periodo = PeriodoDatas.Criar(model.DataInicio, model.DataFim);

            var torneio = new Torneio(nome, tamanho, periodo);

            await _torneios.AdicionarAsync(torneio, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task AtualizarAsync(TorneioViewModel model, CancellationToken ct = default)
        {
            var torneio = await _torneios.ObterPorIdAsync(model.Id, ct)
                ?? throw new Exception("Torneio não encontrado.");

            torneio.Renomear(Nome.Criar(model.Nome));
            torneio.AtualizarPeriodo(PeriodoDatas.Criar(model.DataInicio, model.DataFim));

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task RemoverAsync(Guid id, CancellationToken ct = default)
        {
            var t = await _torneios.ObterPorIdAsync(id, ct)
                ?? throw new Exception("Torneio não encontrado.");

            await _torneios.RemoverAsync(t, ct);
            await _ctx.SaveChangesAsync(ct);
        }
        public async Task InscreverParticipanteAsync(Guid torneioId, Guid participanteId, CancellationToken ct = default)
        {
            var t = await _torneios.ObterPorIdAsync(torneioId, ct) ?? throw new Exception("Torneio não encontrado.");
            var p = await _participantes.ObterPorIdAsync(participanteId, ct) ?? throw new Exception("Participante não encontrado.");

            t.Inscrever(p);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task PublicarAsync(Guid torneioId, CancellationToken ct = default)
        {
            var t = await _torneios.ObterPorIdAsync(torneioId, ct) ?? throw new Exception("Torneio não encontrado.");
            var participantesIds = t.Inscricoes
                .Select(i => i.ParticipanteId)
                .ToList();

            var pares = _chaveador.Gerar(participantesIds);
            t.PublicarComPares(pares);

            await _ctx.SaveChangesAsync(ct);
        }
    }
}
