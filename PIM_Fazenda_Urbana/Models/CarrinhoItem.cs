namespace PIM_Fazenda_Urbana.Models
{
    public class CarrinhoItem
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public decimal? TotalItem
        {
            get
            {
                return this.Quantidade * this.Preco;
            }
        }
    }
}
