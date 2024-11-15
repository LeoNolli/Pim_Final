using System.Reflection.Metadata.Ecma335;

namespace PIM_Fazenda_Urbana.Models.ViewModels
{
    public class VendaViewModel
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int ClienteId { get; set; }
        public Funcionario Funcionario { get; set; }
        public IEnumerable<ItemVendaViewModel> Itens { get; set; }
        public decimal Total { get; set; }
    }
}
