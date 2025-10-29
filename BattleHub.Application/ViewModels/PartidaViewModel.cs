namespace BattleHub.Application.ViewModels
{
    public class PartidaViewModel
    {
        public Guid Id { get; set; }
        public int Rodada { get; set; }
        public string ParticipanteA { get; set; } = string.Empty;
        public string ParticipanteB { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Vencedor { get; set; }
        public string? Placar { get; set; }
    }
}
