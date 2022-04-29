using System.ComponentModel.DataAnnotations;

namespace GestaoOficinas.Api.Models
{
    public class Oficina
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }


        [Required]
        [MaxLength(18, ErrorMessage ="CNPJ excede o tamanho máximo de caracteres")]
        [MinLength(14, ErrorMessage ="CNPJ menor que o tamanho minimo de caracteres")]
        public string CNPJ { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="Informe uma senha que contenha pelo menos 6 caracteres.")]
        public string? Password { get; set; }

        [Required]
        public int  UnidadeTempoDiaria { get; set; }


    }
}
