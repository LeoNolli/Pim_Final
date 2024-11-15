namespace PIM_Fazenda_Urbana.Models
{
    public class Carrinho
    {
        private List<CarrinhoItem> itens = new List<CarrinhoItem>();

        public IEnumerable<CarrinhoItem> Itens => itens;

        public void AdicionarItem(CarrinhoItem item)
        {
            var existente = itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);

            if (existente == null)
            {
                itens.Add(item);
            }
            else
            {
                existente.Quantidade += item.Quantidade;
            }
        }

        public void RemoverItem(int produtoId)
        {
            itens.RemoveAll(i => i.ProdutoId == produtoId);
        }

        public decimal CalcularTotal()
        {
            return itens.Sum(i => i.Preco * i.Quantidade);
        }

        public void Limpar()
        {
            itens.Clear();
        }
    }
}
