using PIM_Fazenda_Urbana.Models.ViewModels;

namespace PIM_Fazenda_Urbana.Models
{
    public class ControleProducao
    {
        public int ProducaoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; }
        public int FuncionarioId { get; set; }
        public List<InsumoProducaoViewModel> Insumos { get; set; } // Lista de insumos usados na produção
        public int ProdutoId { get; set; }
    }
}
