using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IInsumoService
    {
        IEnumerable<Insumo> GetAll();
        Insumo? GetById(int insumoId);
        void Add(Insumo insumo);
        void Update(Insumo insumo);
        void UpdateQuantidade(int insumoId, int quantidadeUsada);
        void Delete(int insumoId);
    }
}
