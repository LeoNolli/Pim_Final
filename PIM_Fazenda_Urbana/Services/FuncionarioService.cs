using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Services
{
	public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public void CreateFuncionario(UsuarioFuncionarioRegistroDTO funcionario, int usuarioId)
        {
            _funcionarioRepository.CreateFuncionario(funcionario, usuarioId);
        }

		public Funcionario GetFuncionarioByrId(int id)
        {
            return _funcionarioRepository.GetFuncionarioById(id);
        }

        public Funcionario GetFuncionarioByUsuarioId(int id)
        {
            return _funcionarioRepository.GetFuncionarioByUsuarioId(id);
        }

        public IEnumerable<Funcionario> GetFuncionarios()
        {
            return _funcionarioRepository.GetFuncionarios();
        }

        public bool UpdateFuncionario(Funcionario funcionario)
        {
            return _funcionarioRepository.UpdateFuncionario(funcionario);
        }
		public void Delete(int id)
		{
			_funcionarioRepository.Delete(id);
		}
	}
}
