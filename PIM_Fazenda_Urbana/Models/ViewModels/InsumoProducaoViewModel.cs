namespace PIM_Fazenda_Urbana.Models.ViewModels
{
    public class InsumoProducaoViewModel
    {
        public int InsumoId { get; set; }
        public string Nome { get; set; } // Nome do insumo
        public bool Selecionado { get; set; } // Indica se o insumo foi selecionado
        public int QuantidadeUsada { get; set; } // Quantidade utilizada
    }
}
