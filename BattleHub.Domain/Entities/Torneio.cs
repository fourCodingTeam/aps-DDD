using BattleHub.Domain.Enums;
using BattleHub.Domain.Interfaces;
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

        public void Publicar(IChaveamento chaveamento)
        {
            if (Estado != EstadoTorneio.Rascunho)
                throw new InvalidOperationException("Somente torneio em rascunho pode ser publicado.");

            Estado = EstadoTorneio.Publicado;

            try
            {
                var pares = chaveamento.Gerar(_inscricoes.Select(i => i.ParticipanteId).ToList());
                int rodada = 1;

                foreach (var (a, b) in pares)
                    _partidas.Add(Partida.Criar(Id, rodada, a, b));
            } 
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Erro ao criar o torneio. Error: {ex}");
            }
        }

        public void PublicarComPares(IReadOnlyList<(Guid A, Guid B)> pares)
        {
            if (Estado != EstadoTorneio.Rascunho)
                throw new InvalidOperationException("Apenas torneios em rascunho podem ser publicados.");

            if (pares.Count != _inscricoes.Count / 2)
                throw new InvalidOperationException("Número inválido de pares para gerar partidas.");

            Estado = EstadoTorneio.Publicado;

            int rodada = 1;
            foreach (var (a, b) in pares)
            {
                var partida = Partida.Criar(Id, rodada, a, b);
                _partidas.Add(partida);
            }
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
