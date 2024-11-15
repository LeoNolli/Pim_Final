using System.ComponentModel.DataAnnotations;

namespace PIM_Fazenda_Urbana.Models
{
    public class Insumo
    {
        public int InsumoId { get; set; }

        [MaxLength(50, ErrorMessage = "Nome maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório\"")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "Descricao maior do que o permitido")]
        [Required(ErrorMessage = "O campo \"Descricao\" é obrigatório\"")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo \"Quantidade\" é obrigatório\"")]
        [Range(1, int.MaxValue, ErrorMessage = "A Quantidade deve ser maior que 0")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo \"Preço\" é obrigatório")]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Preco { get; set; }
    }
}
