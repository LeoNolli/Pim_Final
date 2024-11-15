using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
	public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly IDbConnection _connection;

        public FuncionarioRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Funcionario> GetFuncionarios()
        {
            string query = "SELECT * FROM Funcionario f INNER JOIN Usuario u  ON f.UsuarioId = u.UsuarioID";
            _connection.Open();

            var funcionarios = _connection.Query<Funcionario, Usuario, Funcionario>(
                query,
                (funcionario, usuario) =>
                {
                    funcionario.Usuario = usuario; // Atribui o usuário ao funcionário
                    return funcionario;
                },
                splitOn: "UsuarioId" // Indica a coluna que diferencia os objetos
            );
            _connection.Close();

            return funcionarios;
        }

        public Funcionario GetFuncionarioById(int id)
        {
            string query = "SELECT * FROM Funcionario WHERE FuncionarioId = @id";

            _connection.Open();

            var funcionario = _connection.QueryFirst<Funcionario>(query, new
            {
                @id = id
            });

            _connection.Close();

            return funcionario;
        }

        public Funcionario GetFuncionarioByUsuarioId(int id)
        {
            string query = "SELECT * FROM Funcionario WHERE UsuarioId = @id";

            _connection.Open();

            var funcionario = _connection.QueryFirst<Funcionario>(query, new
            {
                @id = id
            });

            _connection.Close();

            return funcionario;
        }

        public void CreateFuncionario(UsuarioFuncionarioRegistroDTO novoFuncionario, int usuarioId)
        {
            string query = "INSERT INTO Funcionario (`Nome`, `CPF`, `RG`, `Endereco`, `Telefone`, `Cargo`, `UsuarioId`) " +
                "VALUES (@Nome, @CPF, @RG, @Endereco, @Telefone, @Cargo, @UsuarioId);";

            _connection.Open();
            _connection.Execute(query, new
            {
                @Nome = novoFuncionario.Nome,
                @CPF = novoFuncionario.CPF,
                @RG = novoFuncionario.RG,
                @Endereco = novoFuncionario.Endereco,
                @Telefone = novoFuncionario.Telefone,
                @Cargo = novoFuncionario.Cargo,
                @UsuarioId = usuarioId
            });
            _connection.Close();
        }

        public bool UpdateFuncionario(Funcionario funcionario)
        {
            string query = "UPDATE Funcionario " +
                           "SET `Nome` = @Nome," +
                           "`CPF` = @CPF," +
                           "`RG` = @RG," +
                           "`Endereco` = @Endereco," +
                           "`Telefone` = @Telefone," +
                           "`Cargo` = @Cargo " +
                           "WHERE `FuncionarioId` = @FuncionarioId;";
            _connection.Open();
            var linhasAfetadas = _connection.Execute(query, new
            {
                @Nome = funcionario.Nome,
                @CPF = funcionario.CPF,
                @RG = funcionario.RG,
                @Endereco = funcionario.Endereco,
                @Telefone = funcionario.Telefone,
                @Cargo = funcionario.Cargo,
                @FuncionarioId = funcionario.FuncionarioId
            });
            _connection.Close();

            if (linhasAfetadas > 0)
                return true;

            else
                return false;

        }

		public void Delete(int id)
		{
			string sql = "DELETE FROM Funcionario WHERE FuncionarioId = @id";
			_connection.Open();

			_connection.Execute(sql, new { id = id });
			_connection.Close();
		}
	}
}
