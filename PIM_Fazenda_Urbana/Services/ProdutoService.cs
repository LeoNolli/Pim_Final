using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtoRepository.GetAll();
        }

        public Produto? GetById(int produtoId)
        {
            var produto = _produtoRepository.GetById(produtoId);

            if (produto == null)
                throw new BadHttpRequestException("Houve um erro ao concluir a ação. Produto não encontrado");

            return produto;

        }

        public void Add(Produto produto)
        {
            _produtoRepository.Add(produto);
        }

        public void Update(Produto produto)
        {
            _produtoRepository.Update(produto);
        }

        public void Delete(int produtoId)
        {
            _produtoRepository.Delete(produtoId);
        }
    }
}
