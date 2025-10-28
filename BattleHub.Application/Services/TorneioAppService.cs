using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using BattleHub.Domain.Entities;
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

        public TorneioAppService(ITorneioRepository torneios, AppDbContext ctx)
        {
            _torneios = torneios;
            _ctx = ctx;
        }

        public async Task<IEnumerable<TorneioViewModel>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await _torneios.Query().AsNoTracking().ToListAsync(ct);
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
    }
}
