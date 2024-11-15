using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;


        public UsuarioRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Usuario? GetByEmail(string email)
        {
            var query = "SELECT * FROM Usuario WHERE Email = @Email";
            _connection.Open();

            var usuario = _connection.QueryFirstOrDefault<Usuario>(query, new { @Email = email });
            _connection.Close();
            return usuario;
        }

        public int CreateUsuario(UsuarioRegistroDTO usuario)
        {
            var query = "INSERT INTO Usuario (`Email`, `Senha`, `DataCriacao`, `Funcao`) VALUES (@Email, @Senha, @Data, @Funcao);" +
                "SELECT LAST_INSERT_ID();";
            _connection.Open();

            int usuarioId = _connection.ExecuteScalar<int>(query, new
            {
                @Email = usuario.Email,
                @Senha = usuario.Senha,
                @Data = DateTime.Now,
                @Funcao = usuario.Funcao
            });
            _connection.Close();

            return usuarioId;
        }

    }
}

