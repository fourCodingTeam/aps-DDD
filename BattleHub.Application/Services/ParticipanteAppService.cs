using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using BattleHub.Domain.Entities;
using BattleHub.Domain.Repositories;
using BattleHub.Domain.ValueObjects;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BattleHub.Application.Services
{
    public class ParticipanteAppService : IParticipanteAppService
    {
        private readonly IParticipanteRepository _participantes;
        private readonly AppDbContext _ctx;

        public ParticipanteAppService(IParticipanteRepository participantes, AppDbContext ctx)
        {
            _participantes = participantes;
            _ctx = ctx;
        }

        public async Task<IEnumerable<ParticipanteViewModel>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await _participantes.Query().ToListAsync(ct);
            return lista.Select(p => new ParticipanteViewModel
            {
                Id = p.Id,
                Nome = p.Nome.Valor,
                Email = p.Email.Valor
            });
        }

        public async Task<ParticipanteViewModel?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            var p = await _participantes.ObterPorIdAsync(id, ct);
            if (p == null) return null;

            return new ParticipanteViewModel
            {
                Id = p.Id,
                Nome = p.Nome.Valor,
                Email = p.Email.Valor
            };
        }

        public async Task CriarAsync(ParticipanteViewModel model, CancellationToken ct = default)
        {
            var nome = Nome.Criar(model.Nome);
            var email = Email.Criar(model.Email);

            var participante = new Participante(nome, email);

            await _participantes.AdicionarAsync(participante, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task AtualizarAsync(ParticipanteViewModel model, CancellationToken ct = default)
        {
            var p = await _participantes.ObterPorIdAsync(model.Id, ct)
                ?? throw new Exception("Participante não encontrado.");

            p.AtualizarNome(Nome.Criar(model.Nome));
            p.AtualizarEmail(Email.Criar(model.Email));

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task RemoverAsync(Guid id, CancellationToken ct = default)
        {
            var p = await _participantes.ObterPorIdAsync(id, ct)
                ?? throw new Exception("Participante não encontrado.");

            await _participantes.RemoverAsync(p, ct);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}
