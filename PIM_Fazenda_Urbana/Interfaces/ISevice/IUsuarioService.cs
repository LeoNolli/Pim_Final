using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IUsuarioService
    {
        public Usuario? GetByEmail(string email);

        public int CreateUsuario(UsuarioRegistroDTO usuario);

    }
}
