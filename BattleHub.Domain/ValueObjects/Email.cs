namespace BattleHub.Domain.ValueObjects;

public record Email
{
    public string Valor { get; }

    private Email(string valor) => Valor = valor;

    public static Email Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("E-mail não pode ser vazio.");

        var v = valor.Trim().ToLowerInvariant();
        if (!v.Contains('@') || v.StartsWith("@") || v.EndsWith("@"))
            throw new ArgumentException("Formato de e-mail inválido.");

        return new Email(v);
    }

    public override string ToString() => Valor;
}