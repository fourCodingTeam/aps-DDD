using BattleHub.Domain.ValueObjects;

namespace BattleHub.Domain.Entities
{
    public class Participante
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }

        protected Participante() { }

        public Participante(Nome nome, Email email)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public void AtualizarNome(Nome novoNome) => Nome = novoNome ?? throw new ArgumentNullException(nameof(novoNome));
        public void AtualizarEmail(Email novoEmail) => Email = novoEmail ?? throw new ArgumentNullException(nameof(novoEmail));
    }
}