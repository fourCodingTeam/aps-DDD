namespace BattleHub.Domain.Entities
{
    public class Inscricao
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid TorneioId { get; private set; }
        public Guid ParticipanteId { get; private set; }
        public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

        protected Inscricao() { }

        public Inscricao(Guid torneioId, Guid participanteId)
        {
            TorneioId = torneioId;
            ParticipanteId = participanteId;
        }
    }
}