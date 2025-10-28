namespace BattleHub.Domain.ValueObjects;

public record Nome
{
    public string Valor { get; }

    private Nome(string valor) => Valor = valor;

    public static Nome Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("Nome não pode ser vazio.");

        var v = valor.Trim();
        if (v.Length < 2)
            throw new ArgumentException("Nome muito curto.");

        return new Nome(v);
    }

    public override string ToString() => Valor;
}
