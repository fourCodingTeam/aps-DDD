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

        public async Task<IEnumerable<TorneioViewModel>> ListarAsync()
        {
            var lista = await _torneios.Query().ToListAsync();
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

        public async Task<TorneioViewModel?> ObterPorIdAsync(Guid id)
        {
            var t = await _torneios.ObterPorIdAsync(id);
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

        public async Task CriarAsync(TorneioViewModel model)
        {
            var nome = Nome.Criar(model.Nome);
            var tamanho = TamanhoChave.Criar(model.TamanhoChave);
            var periodo = PeriodoDatas.Criar(model.DataInicio, model.DataFim);

            var torneio = new Torneio(nome, tamanho, periodo);

            await _torneios.AdicionarAsync(torneio);
            await _ctx.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TorneioViewModel model)
        {
            var torneio = await _torneios.ObterPorIdAsync(model.Id)
                ?? throw new Exception("Torneio não encontrado.");

            torneio.Renomear(Nome.Criar(model.Nome));
            torneio.AtualizarPeriodo(PeriodoDatas.Criar(model.DataInicio, model.DataFim));

            await _ctx.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var t = await _torneios.ObterPorIdAsync(id)
                ?? throw new Exception("Torneio não encontrado.");

            await _torneios.RemoverAsync(t);
            await _ctx.SaveChangesAsync();
        }
        public async Task InscreverParticipanteAsync(Guid torneioId, Guid participanteId)
        {
            var t = await _torneios.ObterPorIdAsync(torneioId) ?? throw new Exception("Torneio não encontrado.");
            var p = await _participantes.ObterPorIdAsync(participanteId) ?? throw new Exception("Participante não encontrado.");

            _ctx.Inscricoes.Add(new Inscricao(t.Id, p.Id));
            await _ctx.SaveChangesAsync();
        }

        public async Task PublicarAsync(Guid torneioId)
        {
            var t = await _ctx.Torneios.FirstOrDefaultAsync(x => x.Id == torneioId);

            if (t is null) throw new Exception("Torneio não encontrado.");

            var participantesIds = t.Inscricoes
                .Select(i => i.ParticipanteId)
                .ToList();

            var pares = _chaveador.Gerar(participantesIds);

            t.PublicarComPares(pares);

            await _ctx.SaveChangesAsync();
        }
    }
}
