using System.ComponentModel.DataAnnotations;

namespace BattleHub.Application.ViewModels
{
    public class ParticipanteViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(180, ErrorMessage = "O e-mail deve ter no máximo 180 caracteres.")]
        public string Email { get; set; } = string.Empty;
    }
}
