using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.ViewModels;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly IDbConnection _connection;

        public VendaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Venda> GetVendasByPeriodo(DateTime? dataInicio, DateTime? dataFim)
        {
            string sql = "SELECT * FROM Venda WHERE (@dataInicio IS NULL OR DataHora >= @dataInicio) AND (@dataFim IS NULL OR DataHora <= @dataFim)";
            return _connection.Query<Venda>(sql, new { dataInicio, dataFim });
        }

        public VendaViewModel GetVendaViewModelById(int vendaId)
        {
            string vendaSql = @"
                SELECT * FROM Venda
                WHERE VendaId = @VendaId";

            string itensSql = @"
                SELECT * FROM ItemVenda
                WHERE VendaId = @VendaId";

            string funcionarioSql = @"
                SELECT f.* FROM Funcionario f
                JOIN Venda v ON v.FuncionarioId = f.FuncionarioId
                WHERE v.VendaId = @VendaId";

            _connection.Open();

            // Obter a venda
            var venda = _connection.QuerySingleOrDefault<Venda>(vendaSql, new { VendaId = vendaId });

            if (venda == null)
            {
                _connection.Close();
                return null;
            }

            // Obter os itens da venda
            var itens = _connection.Query<ItemVendaViewModel>(itensSql, new { VendaId = vendaId });

            foreach(var item in itens)
            {
                item.Nome = _connection.QueryFirstOrDefault<string>("SELECT Nome FROM Produto WHERE ProdutoId = @ItemId", new { ItemId = item.ProdutoId });
            }


            // Obter o funcionário responsável
            var funcionario = _connection.QuerySingleOrDefault<Funcionario>(funcionarioSql, new { VendaId = vendaId });

            _connection.Close();

            // Construir o ViewModel
            var vendaViewModel = new VendaViewModel
            {
                Id = venda.VendaId,
                DataHora = venda.DataHora,
                ClienteId = venda.ClienteId,
                Funcionario = funcionario,
                Itens = itens,
                Total = venda.ValorTotal
            };

            return vendaViewModel;
        }


        public IEnumerable<Venda> GetAll()
        {
            string sql = "SELECT * FROM Venda";
            _connection.Open();
            var vendas = _connection.Query<Venda>(sql);
            _connection.Close();
            return vendas;
        }

        public Venda GetById(int vendaId)
        {
            string sql = "SELECT * FROM Venda WHERE VendaId = @VendaId";
            _connection.Open();
            var venda = _connection.QueryFirstOrDefault<Venda>(sql, new { VendaId = vendaId });
            _connection.Close();
            return venda;
        }

        public int Add(Venda venda)
        {
            string sql = @"
            INSERT INTO Venda (DataHora, ValorTotal, ClienteId, FuncionarioId)
            VALUES (@DataHora, @ValorTotal, @ClienteId, @FuncionarioId); 
            SELECT LAST_INSERT_ID();";
            _connection.Open();
            int vendaId = _connection.ExecuteScalar<int>(sql, venda);
            _connection.Close();
            return vendaId;
        }

        public void Update(Venda venda)
        {
            string sql = @"
            UPDATE Venda
            SET DataHora = @DataHora, ValorTotal = @ValorTotal, ClienteId = @ClienteId, FuncionarioId = @FuncionarioId
            WHERE VendaId = @VendaId";
            _connection.Open();
            _connection.Execute(sql, venda);
            _connection.Close();
        }

        public void Delete(int vendaId)
        {
            string sql = "DELETE FROM Venda WHERE VendaId = @VendaId";
            _connection.Open();
            _connection.Execute(sql, new { VendaId = vendaId });
            _connection.Close();
        }

        // Implementação do método AddItemVenda
        public void AddItemVenda(ItemVenda itemVenda)
        {
            string sql = @"
            INSERT INTO ItemVenda (ProdutoId, VendaId, Quantidade, PrecoVenda)
            VALUES (@ProdutoId, @VendaId, @Quantidade, @PrecoVenda)";
            _connection.Open();
            _connection.Execute(sql, itemVenda);
            _connection.Close();
        }

        public IEnumerable<ItemVenda> GetItemByVendaId(int vendaId)
        {
            string sql = "SELECT * FROM ItemVenda WHERE VendaId = @VendaId";
            _connection.Open();
            var itensVenda = _connection.Query<ItemVenda>(sql, new { VendaId = vendaId });
            _connection.Close();

            return itensVenda;
        }

        // Implementação do método DeleteItemVenda
        public void DeleteItemVenda(int vendaId)
        {
            string sql = "DELETE FROM ItemVenda WHERE VendaId = @VendaId";
            _connection.Open();
            _connection.Execute(sql, new { VendaId = vendaId });
            _connection.Close();
        }
    }
}
