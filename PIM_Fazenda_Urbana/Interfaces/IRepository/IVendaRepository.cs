using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.ViewModels;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IVendaRepository
    {
        IEnumerable<Venda> GetVendasByPeriodo(DateTime? dataInicio, DateTime? dataFim);
        IEnumerable<Venda> GetAll();
        VendaViewModel GetVendaViewModelById(int vendaId);
        Venda GetById(int vendaId);
        int Add(Venda venda);
        void Update(Venda venda);
        void Delete(int vendaId);
        IEnumerable<ItemVenda> GetItemByVendaId(int vendaId);
        void AddItemVenda(ItemVenda itemVenda);
        void DeleteItemVenda(int vendaId);
    }
}
