namespace BattleHub.Domain.Interfaces
{
    public interface IChaveamento
    {
        IReadOnlyList<(Guid A, Guid B)> Gerar(IReadOnlyList<Guid> participantes);
    }
}
