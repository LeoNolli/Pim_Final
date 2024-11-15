namespace PIM_Fazenda_Urbana.Models
{
    public class ItemVenda
    {
        public int ProdutoId { get; set; }
        public int VendaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }

        public decimal CalcularTotal()
        {
            return this.Quantidade * this.PrecoVenda;
        }
    }
}
