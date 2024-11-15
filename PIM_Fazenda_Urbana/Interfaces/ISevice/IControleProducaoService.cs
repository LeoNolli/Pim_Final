using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IControleProducaoService
    {
        IEnumerable<ControleProducao> GetAll();
        ControleProducao? GetById(int producaoId);
        int Add(ControleProducao producao);
        void Update(ControleProducao producao);
        void Delete(int producaoId);
        void CompleteProduction(ControleProducao producao);

    }
}
