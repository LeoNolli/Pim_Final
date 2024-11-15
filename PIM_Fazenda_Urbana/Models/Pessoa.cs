using System.ComponentModel.DataAnnotations;

namespace PIM_Fazenda_Urbana.Models
{
    public abstract class Pessoa
    {
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório\"")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo \"CPF\" é obrigatório\"")]
        [MaxLength(11, ErrorMessage = "CPF maior do que o permitido")]
        public string CPF {get; set;}

        [MaxLength(20, ErrorMessage= "RG maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"RG\" é obrigatório\"")]
        public string RG{ get; set; }

        [MaxLength(100, ErrorMessage = "Endereço maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"Endereco\" é obrigatório\"")]
        public string Endereco { get; set; }

        [MaxLength(20, ErrorMessage = "Telefone maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório")]
        public string Telefone { get; set; }

        public Usuario Usuario { get; set; }
    }
}
