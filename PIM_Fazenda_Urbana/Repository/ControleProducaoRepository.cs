using Dapper;
using MySql.Data.MySqlClient;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class ControleProducaoRepository : IControleProducaoRepository
    {
        private readonly IDbConnection _connection;

        public ControleProducaoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<ControleProducao> GetAll()
        {
            string sql = "SELECT * FROM ControleProducao";
            _connection.Open();

            var producoes = _connection.Query<ControleProducao>(sql);
            _connection.Close();

            return producoes;
        }

        public ControleProducao? GetById(int producaoId)
        {
            string sql = "SELECT * FROM ControleProducao WHERE ProducaoId = @ProducaoId";
            _connection.Open();

            var producao = _connection.QueryFirstOrDefault<ControleProducao>(sql, new { ProducaoId = producaoId });
            _connection.Close();

            return producao;
        }

        public int Add(ControleProducao producao)
        {
            string sql = @"
        INSERT INTO ControleProducao (Quantidade, DataInicio, DataConclusao, Status, FuncionarioId, ProdutoId)
        VALUES (@Quantidade, @DataInicio, @DataConclusao, @Status, @FuncionarioId, @ProdutoId);
        SELECT LAST_INSERT_ID();";
            _connection.Open();

            int id = _connection.ExecuteScalar<int>(sql, producao);

            _connection.Close();

            return id;
        }

        public void Update(ControleProducao producao)
        {
            string sql = @"
        UPDATE ControleProducao
        SET Quantidade = @Quantidade, DataInicio = @DataInicio, DataConclusao = @DataConclusao, Status = @Status
        WHERE ProducaoId = @ProducaoId";
            _connection.Open();

            _connection.Execute(sql, producao);
            _connection.Close();
        }

        public void Delete(int producaoId)
        {
            string sql = "DELETE FROM ControleProducao WHERE ProducaoId = @ProducaoId";
            _connection.Open();

            _connection.Execute(sql, new { ProducaoId = producaoId });
            _connection.Close();
        }
    }

}
