using BattleHub.Domain.Enums;
using BattleHub.Domain.ObjetosDeValor;
using BattleHub.Domain.ValueObjects;

namespace BattleHub.Domain.Entities
{
    public class Torneio
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Nome Nome { get; private set; }
        public TamanhoChave TamanhoChave { get; private set; }
        public PeriodoDatas Periodo { get; private set; }
        public EstadoTorneio Estado { get; private set; } = EstadoTorneio.Rascunho;

        private readonly List<Inscricao> _inscricoes = new();
        public IReadOnlyCollection<Inscricao> Inscricoes => _inscricoes;

        private readonly List<Partida> _partidas = new();
        public IReadOnlyCollection<Partida> Partidas => _partidas;

        protected Torneio() { }

        public Torneio(Nome nome, TamanhoChave tamanhoChave, PeriodoDatas periodo)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            TamanhoChave = tamanhoChave ?? throw new ArgumentNullException(nameof(tamanhoChave));
            Periodo = periodo ?? throw new ArgumentNullException(nameof(periodo));
        }

        public bool PodeInscrever() =>
            (Estado is EstadoTorneio.Rascunho or EstadoTorneio.Publicado)
            && _inscricoes.Count < TamanhoChave.Valor;

        public void Inscrever(Participante participante)
        {
            if (!PodeInscrever())
                throw new InvalidOperationException("Inscrições fechadas.");

            if (_inscricoes.Any(i => i.ParticipanteId == participante.Id))
                throw new InvalidOperationException("Participante já inscrito.");

            _inscricoes.Add(new Inscricao(Id, participante.Id));
        }

        public void Publicar()
        {
            if (Estado != EstadoTorneio.Rascunho)
                throw new InvalidOperationException("Somente torneio em rascunho pode ser publicado.");

            if (_inscricoes.Count != TamanhoChave.Valor)
                throw new InvalidOperationException("Para publicar, o número de inscrições deve preencher a chave.");

            Estado = EstadoTorneio.Publicado;
            GerarChaveamento();
        }

        private void GerarChaveamento()
        {
            // MVP: parear inscrições na ordem de criação
            var ids = _inscricoes.Select(i => i.ParticipanteId).ToList();
            int rodada = 1;
            for (int i = 0; i < ids.Count; i += 2)
                _partidas.Add(Partida.Criar(Id, rodada, ids[i], ids[i + 1]));
        }

        public void Iniciar()
        {
            if (Estado != EstadoTorneio.Publicado)
                throw new InvalidOperationException("Torneio deve estar publicado para iniciar.");
            Estado = EstadoTorneio.EmAndamento;
        }

        public void FinalizarSeEncerrado()
        {
            if (_partidas.Count > 0 && _partidas.All(p => p.Estado == EstadoPartida.Concluida))
                Estado = EstadoTorneio.Finalizado;
        }

        public void AtualizarPeriodo(PeriodoDatas novo)
        {
            if (Estado != EstadoTorneio.Rascunho)
                throw new InvalidOperationException("Só é possível alterar período no rascunho.");
            Periodo = novo;
        }

        public void Renomear(Nome novoNome) => Nome = novoNome ?? throw new ArgumentNullException(nameof(novoNome));
    }
}
