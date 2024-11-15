using Dapper;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;
using System.Data;

namespace PIM_Fazenda_Urbana.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _connection;

        public ProdutoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Produto> GetAll()
        {
            string sql = "SELECT * FROM Produto";
            _connection.Open();

            var produtos = _connection.Query<Produto>(sql);
            _connection.Close();

            return produtos;
        }

        public Produto? GetById(int produtoId)
        {
            string sql = "SELECT * FROM Produto WHERE ProdutoId = @ProdutoId";
            _connection.Open();

            var produto = _connection.QueryFirstOrDefault<Produto>(sql, new { ProdutoId = produtoId });
            _connection.Close();

            return produto;
        }

        public void Add(Produto produto)
        {
            string sql = @"
        INSERT INTO Produto (Nome, Descricao, QuantidadeEstoque, Preco)
        VALUES (@Nome, @Descricao, @QuantidadeEstoque, @Preco)";
            _connection.Open();

            _connection.Execute(sql, produto);
            _connection.Close();
        }

        public void Update(Produto produto)
        {
            string sql = @"
        UPDATE Produto
        SET Nome = @Nome, Descricao = @Descricao, QuantidadeEstoque = @QuantidadeEstoque, Preco = @Preco
        WHERE ProdutoId = @ProdutoId";
            _connection.Open();

            _connection.Execute(sql, produto);
            _connection.Close();
        }

        public void Delete(int produtoId)
        {
            string sql = "DELETE FROM Produto WHERE ProdutoId = @ProdutoId";
            _connection.Open();

            _connection.Execute(sql, new { ProdutoId = produtoId });
            _connection.Close();
        }
    }
}
