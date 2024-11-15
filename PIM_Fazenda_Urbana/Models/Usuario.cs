using Microsoft.CodeAnalysis.Emit;

namespace PIM_Fazenda_Urbana.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Funcao{ get; set; }
    }
}
