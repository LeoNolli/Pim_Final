using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IUsuarioRepository
    {
        public Usuario? GetByEmail(string email);
        public int CreateUsuario(UsuarioRegistroDTO usuario);

    }
}
