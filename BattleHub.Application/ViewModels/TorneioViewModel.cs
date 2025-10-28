using System.ComponentModel.DataAnnotations;

namespace BattleHub.Application.ViewModels
{
    public class TorneioViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do torneio é obrigatório.")]
        [StringLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tamanho da chave é obrigatório.")]
        [Range(4, 32, ErrorMessage = "O tamanho da chave deve ser 4, 8, 16 ou 32.")]
        public int TamanhoChave { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        public string Estado { get; set; } = "Rascunho";
    }
}
