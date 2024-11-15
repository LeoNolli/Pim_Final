using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Services
{
    public class InsumoProducaoService : IInsumoProducaoService
    {
        private readonly IInsumoProducaoRepository _insumoProducaoRepository;

        public InsumoProducaoService(IInsumoProducaoRepository insumoProducaoRepository)
        {
            _insumoProducaoRepository = insumoProducaoRepository;
        }

        public IEnumerable<InsumoProducao> GetByProducaoId(int producaoId)
        {
            return _insumoProducaoRepository.GetByProducaoId(producaoId);
        }

        public void Add(InsumoProducao insumoProducao)
        {
            _insumoProducaoRepository.Add(insumoProducao);
        }

        public void DeleteByProducaoId(int producaoId)
        {
            _insumoProducaoRepository.DeleteByProducaoId(producaoId);
        }
    }
}
