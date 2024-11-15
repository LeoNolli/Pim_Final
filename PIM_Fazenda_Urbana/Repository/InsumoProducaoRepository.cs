using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class InsumoProducaoRepository : IInsumoProducaoRepository
    {
        private readonly IDbConnection _connection;

        public InsumoProducaoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<InsumoProducao> GetByProducaoId(int producaoId)
        {
            string sql = "SELECT * FROM InsumoProducao WHERE ProducaoId = @ProducaoId";
            _connection.Open();

            var insumos = _connection.Query<InsumoProducao>(sql, new { ProducaoId = producaoId });
            _connection.Close();

            return insumos;
        }

        public void Add(InsumoProducao insumoProducao)
        {
            string sql = @"
        INSERT INTO InsumoProducao (Quantidade, ProducaoId, InsumoId)
        VALUES (@Quantidade, @ProducaoId, @InsumoId)";
            _connection.Open();

            _connection.Execute(sql, insumoProducao);
            _connection.Close();
        }

        public void DeleteByProducaoId(int producaoId)
        {
            string sql = "DELETE FROM InsumoProducao WHERE ProducaoId = @ProducaoId";
            _connection.Open();

            _connection.Execute(sql, new { ProducaoId = producaoId });
            _connection.Close();
        }
    }
}
