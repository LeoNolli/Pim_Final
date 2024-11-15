using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IFuncionarioRepository
    {
        public IEnumerable<Funcionario> GetFuncionarios();
        public Funcionario GetFuncionarioById(int id);
        public Funcionario GetFuncionarioByUsuarioId(int id);
        public void CreateFuncionario(UsuarioFuncionarioRegistroDTO novoFuncionario, int userId);
        public bool UpdateFuncionario(Funcionario funcionario);
		public void Delete(int id);

	}
}
