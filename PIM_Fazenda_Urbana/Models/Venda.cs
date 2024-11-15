namespace PIM_Fazenda_Urbana.Models
{
    public class Venda
    {
        public int VendaId { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }
    }
}
