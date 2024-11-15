using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Services
{
    public class InsumoService : IInsumoService
    {
        private readonly IInsumoRepository _insumoRepository;

        public InsumoService(IInsumoRepository insumoRepository)
        {
            _insumoRepository = insumoRepository;
        }

        public IEnumerable<Insumo> GetAll()
        {
            return _insumoRepository.GetAll();
        }

        public Insumo? GetById(int insumoId)
        {

            var insumo = _insumoRepository.GetById(insumoId);

            if (insumo == null)
                throw new BadHttpRequestException("Não foi possível localizar o insumo com esse Id");

            return insumo;
        }

        public void Add(Insumo insumo)
        {
            _insumoRepository.Add(insumo);
        }

        public void Update(Insumo insumo)
        {
            _insumoRepository.Update(insumo);
        }

        public void UpdateQuantidade(int insumoId, int quantidadeUsada)
        {
            var insumo = _insumoRepository.GetById(insumoId);
            if (insumo != null)
            {
                insumo.Quantidade -= quantidadeUsada; // Subtrai a quantidade usada do estoque
                _insumoRepository.Update(insumo);
            }
        }

        public void Delete(int insumoId)
        {
            _insumoRepository.Delete(insumoId);
        }


    }
}
