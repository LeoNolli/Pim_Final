using Dapper;
using MySql.Data.MySqlClient;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class InsumoRepository : IInsumoRepository
    {

        private readonly IDbConnection _connection;

        public InsumoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Método para obter todos os Insumo
        public IEnumerable<Insumo> GetAll()
        {
            _connection.Open();
            string sql = "SELECT * FROM Insumo";
            var insumos = _connection.Query<Insumo>(sql);

            _connection.Close();
            return insumos;
        }

        // Método para obter um insumo pelo ID
        public Insumo GetById(int id)
        {
            _connection.Open();

            string sql = "SELECT * FROM Insumo WHERE InsumoId = @Id";
            var insumo = _connection.QueryFirstOrDefault<Insumo>(sql, new { Id = id });

            _connection.Close();
            return insumo;

        }

        // Método para adicionar um novo insumo
        public void Add(Insumo insumo)
        {

            string sql = @"
                INSERT INTO Insumo (Nome, Descricao, Quantidade, Preco)
                VALUES (@Nome, @Descricao, @Quantidade, @Preco)";

            _connection.Open();
            _connection.Execute(sql, insumo);
            _connection.Close();
        }

        // Método para atualizar um insumo existente
        public void Update(Insumo insumo)
        {

            string sql = @"
                UPDATE Insumo
                SET Nome = @Nome,
                    Descricao = @Descricao,
                    Quantidade = @Quantidade,
                    Preco = @Preco
                WHERE InsumoId = @InsumoId";
            _connection.Open();
            _connection.Execute(sql, insumo);
            _connection.Close();

        }

        // Método para deletar um insumo pelo ID
        public void Delete(int id)
        {
            _connection.Open();
            string sql = "DELETE FROM Insumo WHERE InsumoId = @Id";
            _connection.Execute(sql, new { Id = id });
            _connection.Close();

        }
    }
}
