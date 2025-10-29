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

        public async Task<IEnumerable<ParticipanteViewModel>> ListarAsync()
        {
            var lista = await _participantes.Query().ToListAsync();
            return lista.Select(p => new ParticipanteViewModel
            {
                Id = p.Id,
                Nome = p.Nome.Valor,
                Email = p.Email.Valor
            });
        }

        public async Task<ParticipanteViewModel?> ObterPorIdAsync(Guid id)
        {
            var p = await _participantes.ObterPorIdAsync(id);
            if (p == null) return null;

            return new ParticipanteViewModel
            {
                Id = p.Id,
                Nome = p.Nome.Valor,
                Email = p.Email.Valor
            };
        }

        public async Task CriarAsync(ParticipanteViewModel model)
        {
            var nome = Nome.Criar(model.Nome);
            var email = Email.Criar(model.Email);

            var participante = new Participante(nome, email);

            await _participantes.AdicionarAsync(participante);
            await _ctx.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ParticipanteViewModel model)
        {
            var p = await _participantes.ObterPorIdAsync(model.Id)
                ?? throw new Exception("Participante não encontrado.");

            p.AtualizarNome(Nome.Criar(model.Nome));
            p.AtualizarEmail(Email.Criar(model.Email));

            await _ctx.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var p = await _participantes.ObterPorIdAsync(id)
                ?? throw new Exception("Participante não encontrado.");

            await _participantes.RemoverAsync(p);
            await _ctx.SaveChangesAsync();
        }
    }
}
