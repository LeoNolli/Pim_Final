using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _connection;

        public ClienteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Cria um novo cliente
        public void CreateCliente(Cliente cliente)
        {
            const string sql = @"
            INSERT INTO Cliente (Nome, CPF, RG, Endereco, Telefone, UsuarioId)
            VALUES (@Nome, @CPF, @RG, @Endereco, @Telefone, @UsuarioId)";

            try
            {
                _connection.Open();
                _connection.Execute(sql, new
                {
                    cliente.Nome,
                    cliente.CPF,
                    cliente.RG,
                    cliente.Endereco,
                    cliente.Telefone,
                    cliente.UsuarioId
                });
            }
            finally
            {
                _connection.Close();
            }
        }

        // Obtém um cliente pelo ID
        public Cliente GetCliente(int id)
        {
            const string sql = "SELECT * FROM Cliente WHERE ClienteId = @Id";

            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Cliente>(sql, new { Id = id });
            }
            finally
            {
                _connection.Close();
            }
        }

        // Obtém um cliente pelo ID do usuário
        public Cliente GetClienteByUsuarioId(int usuarioId)
        {
            const string sql = "SELECT * FROM Cliente WHERE UsuarioId = @UsuarioId";

            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Cliente>(sql, new { UsuarioId = usuarioId });
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
