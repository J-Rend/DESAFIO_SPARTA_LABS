using System.ComponentModel.DataAnnotations;

namespace GestaoOficinas.Api.Models
{
    public class Servico
    {

        [Key]
        public long  Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Informe uma senha que contenha menos que 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Informe uma senha que contenha pelo menos 3 caracteres.")]
        public string?  Name { get; set; }

        [Required]
        public int  UnidadesTrabalhoRequerida { get; set; }

        [Required(ErrorMessage ="Informe uma oficina.")]
        public long Id_oficina { get; set; }


    }
}
