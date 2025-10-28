using BattleHub.Domain.Entities;
using BattleHub.Domain.ValueObjects;

namespace BattleHub.Infraestrutura.Factory;

public static class TorneioFactory
{
    public static Torneio CriarRascunho(string nome, int tamanhoChave, DateTime inicio, DateTime fim)
    {
        var n = Nome.Criar(nome);
        var t = TamanhoChave.Criar(tamanhoChave);
        var p = PeriodoDatas.Criar(inicio, fim);
        return new Torneio(n, t, p);
    }
}
