using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Models
{
    public class Cliente : Pessoa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public Cliente()
        {

        }
        public Cliente(UsuarioRegistroDTO usuario)
        {
            Nome = usuario.Nome;
            CPF = usuario.CPF;
            RG = usuario.RG;
            Endereco = usuario.Endereco;
            Telefone = usuario.Telefone;
        }
    }
}
