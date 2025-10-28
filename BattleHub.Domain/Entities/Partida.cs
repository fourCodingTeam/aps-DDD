using BattleHub.Domain.Enums;
using BattleHub.Domain.ValueObjects;

namespace BattleHub.Domain.Entities
{
    public class Partida
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid TorneioId { get; private set; }
        public int Rodada { get; private set; }
        public Guid? ParticipanteAId { get; private set; }
        public Guid? ParticipanteBId { get; private set; }
        public DateTime? AgendadaPara { get; private set; }
        public EstadoPartida Estado { get; private set; } = EstadoPartida.Pendente;
        public ResultadoPartida? Resultado { get; private set; }

        protected Partida() { }
        private Partida(Guid torneioId, int rodada, Guid? a, Guid? b)
        {
            TorneioId = torneioId;
            Rodada = rodada;
            ParticipanteAId = a;
            ParticipanteBId = b;
        }

        public static Partida Criar(Guid torneioId, int rodada, Guid a, Guid b)
            => new(torneioId, rodada, a, b);

        public void Agendar(DateTime quando)
        {
            if (Estado == EstadoPartida.Concluida)
                throw new InvalidOperationException("Partida já concluída.");
            AgendadaPara = quando;
            Estado = EstadoPartida.Agendada;
        }

        public void ReportarResultado(Guid vencedorId, string? placar)
        {
            if (ParticipanteAId is null || ParticipanteBId is null)
                throw new InvalidOperationException("Partida sem dois participantes.");

            if (vencedorId != ParticipanteAId && vencedorId != ParticipanteBId)
                throw new InvalidOperationException("Vencedor inválido para a partida.");

            Resultado = new ResultadoPartida(vencedorId, placar);
            Estado = EstadoPartida.Concluida;
        }
    }
}
