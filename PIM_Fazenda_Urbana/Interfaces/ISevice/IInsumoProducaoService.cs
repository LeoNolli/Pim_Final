using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IInsumoProducaoService
    {
        IEnumerable<InsumoProducao> GetByProducaoId(int producaoId);
        void Add(InsumoProducao insumoProducao);
        void DeleteByProducaoId(int producaoId);
    }
}
