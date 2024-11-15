using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.ViewModels;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IVendaService
    {
        IEnumerable<Venda> GetVendasByPeriodo(DateTime? dataInicio, DateTime? dataFim);
        IEnumerable<Venda> GetAll();
        Venda GetById(int vendaId);
        VendaViewModel GetVendaViewModelById(int vendaId);
        int Add(Carrinho carrinho, int funcionarioId);
        void Update(Venda venda);
        void Delete(int vendaId);
    }
}
