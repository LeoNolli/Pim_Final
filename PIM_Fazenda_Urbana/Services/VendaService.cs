using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.ViewModels;

namespace PIM_Fazenda_Urbana.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IProdutoService _produtoService;

        public VendaService(IVendaRepository vendaRepository, IProdutoService produtoService)
        {
            _vendaRepository = vendaRepository;
            _produtoService = produtoService;
        }
        public IEnumerable<Venda> GetVendasByPeriodo(DateTime? dataInicio, DateTime? dataFim)
        {
            if (dataInicio == null && dataFim == null)
                throw new BadHttpRequestException("É necessário informar ao menos uma data para o relatório");

            if(dataFim == null)
                dataFim = DateTime.Now;

            if (dataInicio == null)
                dataInicio = DateTime.MinValue;

            if(dataFim < dataInicio)
                throw new BadHttpRequestException("Data Final não pode ser menor que Data Inicial");


            dataFim = dataFim.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            return _vendaRepository.GetVendasByPeriodo(dataInicio, dataFim);
        }

        public VendaViewModel GetVendaViewModelById(int vendaId)
        {
            return _vendaRepository.GetVendaViewModelById(vendaId);
        }
        public IEnumerable<Venda> GetAll()
        {
            return _vendaRepository.GetAll();
        }

        public Venda GetById(int vendaId)
        {
            return _vendaRepository.GetById(vendaId);
        }

        public int Add(Carrinho carrinho, int funcionarioId)
        {

            ChecarQuantidadeItens(carrinho.Itens);

            Venda venda = new Venda();
            venda.DataHora = DateTime.Now;
            venda.ClienteId = 1;
            venda.FuncionarioId = funcionarioId;
            venda.ValorTotal = carrinho.CalcularTotal();

            venda.VendaId = _vendaRepository.Add(venda);
            


            AddItemVenda(carrinho, venda.VendaId);


            return venda.VendaId;
        }

        public void Update(Venda venda)
        {
            _vendaRepository.Update(venda);
        }

        public void Delete(int vendaId)
        {
            DeleteItensByVenda(vendaId);
            _vendaRepository.Delete(vendaId);
        }
        private void AddItemVenda(Carrinho carrinho, int idVenda)
        {

            foreach (var item in carrinho.Itens)
            {
                var itemVenda = new ItemVenda
                {
                    ProdutoId = item.ProdutoId,
                    VendaId = idVenda,
                    Quantidade = item.Quantidade,
                    PrecoVenda = item.Preco
                };
                _vendaRepository.AddItemVenda(itemVenda);
                RemoverQuantidadeProdutos(itemVenda.ProdutoId, itemVenda.Quantidade);
            }
        }

        private void ChecarQuantidadeItens(IEnumerable<CarrinhoItem> itens)
        {
            foreach (var item in itens)
            {
                var produto = _produtoService.GetById(item.ProdutoId);

                if (produto.QuantidadeEstoque < item.Quantidade)
                    throw new BadHttpRequestException($"Quantidade do item {produto.Nome} não disponível");
            }
        }

        private void RemoverQuantidadeProdutos(int produtoId, int qtdRemover)
        {
            var produto = _produtoService.GetById(produtoId);

            produto.QuantidadeEstoque -= qtdRemover;

            _produtoService.Update(produto);
        } 
        private void AdicionarQuantidadeProdutos(int produtoId, int qtdAdd)
        {
            var produto = _produtoService.GetById(produtoId);

            produto.QuantidadeEstoque += qtdAdd;

            _produtoService.Update(produto);
        }

        private void DeleteItensByVenda(int vendaId)
        {
            var itensVenda = _vendaRepository.GetItemByVendaId(vendaId);

            foreach (var item in itensVenda)
            {
                AdicionarQuantidadeProdutos(item.ProdutoId, item.Quantidade);
            }

            _vendaRepository.DeleteItemVenda(vendaId);
        }
    }
}
