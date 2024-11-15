using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IFuncionarioService
    {
        public IEnumerable<Funcionario> GetFuncionarios();
        public Funcionario GetFuncionarioByrId(int id);
        public Funcionario GetFuncionarioByUsuarioId(int id);
        public void CreateFuncionario(UsuarioFuncionarioRegistroDTO funcionario, int usuarioId);
        public bool UpdateFuncionario(Funcionario funcionario);
        public void Delete(int id);
    }
}
