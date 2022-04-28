using System.ComponentModel.DataAnnotations;

namespace GestaoOficinas.Api.Models
{
    public class OficinaServico
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long OficinaId { get; set; }
        [Required]
        public long ServicoId { get; set; }
        [Required]
        public DateTime DataServico { get; set; }

    }
}
