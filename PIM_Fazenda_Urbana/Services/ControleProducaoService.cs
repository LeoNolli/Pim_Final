using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Services
{
    public class ControleProducaoService : IControleProducaoService
    {
        private readonly IControleProducaoRepository _controleProducaoRepository;

        public ControleProducaoService(IControleProducaoRepository controleProducaoRepository)
        {
            _controleProducaoRepository = controleProducaoRepository;
        }

        public IEnumerable<ControleProducao> GetAll()
        {
            return _controleProducaoRepository.GetAll();
        }

        public ControleProducao? GetById(int producaoId)
        {
            return _controleProducaoRepository.GetById(producaoId);
        }

        public int Add(ControleProducao producao)
        {
            if (producao.Quantidade <= 0)
                throw new BadHttpRequestException("Houve um erro ao inserir a produção. Quantidade inválida");
            if (producao.Insumos.Count() <= 0)
                throw new BadHttpRequestException("Houve um erro ao inserir a produção. É obrigatório que uma produção tenha ao menos 1 insumo");

            producao.Status = "Produzindo";
            return _controleProducaoRepository.Add(producao);
        }

        public void Update(ControleProducao producao)
        {
            if (producao.Quantidade <= 0)
                throw new BadHttpRequestException("Houve um erro ao editar a produção. Quantidade inválida");
            if (producao.Insumos.Count() <= 0)
                throw new BadHttpRequestException("Houve um erro ao editar a produção. É obrigatório que uma produção tenha ao menos 1 insumo");
            if(producao.DataConclusao.HasValue && producao.DataInicio > producao.DataConclusao)
                throw new BadHttpRequestException("Houve um erro ao editar a produção. Data inicial não pode ser menor que a data de conclusão");



            _controleProducaoRepository.Update(producao);
        }

        public void Delete(int producaoId)
        {
            _controleProducaoRepository.Delete(producaoId);
        }

        public void CompleteProduction(ControleProducao producao)
        {
            _controleProducaoRepository.Update(producao);
        }
    }
}
