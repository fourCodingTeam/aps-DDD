namespace BattleHub.Domain.ValueObjects;

public record TamanhoChave
{
    public int Valor { get; }

    private TamanhoChave(int valor) => Valor = valor;

    public static readonly int[] Permitidos = new[] { 4, 8, 16, 32 };

    public static TamanhoChave Criar(int valor)
    {
        if (!Permitidos.Contains(valor))
            throw new ArgumentException("Tamanho inválido para chaveamento (use 4, 8, 16 ou 32).");

        return new TamanhoChave(valor);
    }

    public override string ToString() => Valor.ToString();
}
