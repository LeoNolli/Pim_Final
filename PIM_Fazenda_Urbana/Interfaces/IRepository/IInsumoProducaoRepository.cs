using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IInsumoProducaoRepository
    {
        IEnumerable<InsumoProducao> GetByProducaoId(int producaoId);
        void Add(InsumoProducao insumoProducao);
        void DeleteByProducaoId(int producaoId);
    }
}
