using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetAll();
        Produto GetById(int produtoId);
        void Add(Produto produto);
        void Update(Produto produto);
        void Delete(int produtoId);
    }
}
