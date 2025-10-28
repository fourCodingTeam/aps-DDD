namespace BattleHub.Domain.ValueObjects;

public record PeriodoDatas
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }

    private PeriodoDatas(DateTime inicio, DateTime fim)
    {
        Inicio = inicio;
        Fim = fim;
    }

    public static PeriodoDatas Criar(DateTime inicio, DateTime fim)
    {
        if (fim <= inicio)
            throw new ArgumentException("Data final deve ser posterior à data inicial.");

        return new PeriodoDatas(inicio, fim);
    }

    public int DuracaoEmDias => (Fim - Inicio).Days;
}
