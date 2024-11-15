using System.ComponentModel.DataAnnotations;

namespace PIM_Fazenda_Urbana.Models
{
    public class Funcionario : Pessoa
    {
        public int FuncionarioId { get; set; }

        [MaxLength(30, ErrorMessage = "Cargo maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"Cargo\" é obrigatório\"")]
        public string Cargo { get; set; }
    }
}
