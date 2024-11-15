using System.ComponentModel.DataAnnotations;

namespace PIM_Fazenda_Urbana.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'Nome' não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'Descrição' é obrigatório.")]
        [StringLength(300, ErrorMessage = "A 'Descrição' não pode ter mais de 300 caracteres.")]
        public string Descricao { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "O 'Preço' deve ser um valor positivo.")]
        public decimal Preco { get; set; }

        public int QuantidadeEstoque { get; set; } = 0; 
    }
}
