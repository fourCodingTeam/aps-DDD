using BattleHub.Domain.Interfaces;

namespace BattleHub.Application.Services
{
    public class ChaveamentoAleatorio : IChaveamento
    {
        public IReadOnlyList<(Guid A, Guid B)> Gerar(IReadOnlyList<Guid> participantes)
        {
            var rnd = new Random();
            var lista = participantes.OrderBy(_ => rnd.Next()).ToList();
            var pares = new List<(Guid, Guid)>();
            for (int i = 0; i < lista.Count; i += 2)
                pares.Add((lista[i], lista[i + 1]));
            return pares;
        }
    }
}
