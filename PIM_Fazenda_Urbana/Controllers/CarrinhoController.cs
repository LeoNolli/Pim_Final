using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Extensions;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string CarrinhoSessionKey = "Carrinho";
        private readonly IProdutoService _produtoService;

        public CarrinhoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // Exibe o conteúdo do carrinho
        public IActionResult Index()
        {
            var carrinho = ObterCarrinhoDaSessao();
            return View(carrinho);
        }

        // Adiciona um produto ao carrinho
        [Authorize(Roles = "Admin, Gerente, Comum")]
        [HttpPost]
        public IActionResult AdicionarAoCarrinho(int produtoId, string nome, decimal preco, int quantidade)
        {
            try
            {
                var carrinho = ObterCarrinhoDaSessao();

                var estoque = _produtoService.GetById(produtoId).QuantidadeEstoque;

                if (quantidade > estoque)
                    throw new BadHttpRequestException("Quantidade não dispobível no estoque");

                if (quantidade < 1)
                    quantidade = 1;

                var item = new CarrinhoItem
                {
                    ProdutoId = produtoId,
                    Nome = nome,
                    Preco = preco,
                    Quantidade = quantidade
                };

                carrinho.AdicionarItem(item);
                SalvarCarrinhoNaSessao(carrinho);

                return RedirectToAction("Index");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ProductView", "Produto", new {id = produtoId});
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("ProductView", "Produto", new { id = produtoId });
            }
        }

        // Remove um item do carrinho
        [Authorize(Roles = "Admin, Gerente, Comum")]
        [HttpPost]
        public IActionResult RemoverDoCarrinho(int produtoId)
        {
            var carrinho = ObterCarrinhoDaSessao();
            carrinho.RemoverItem(produtoId);
            SalvarCarrinhoNaSessao(carrinho);

            return RedirectToAction("Index");
        }

        // Limpa o carrinho
        [HttpPost]
        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult LimparCarrinho()
        {
            var carrinho = ObterCarrinhoDaSessao();
            carrinho.Limpar();
            SalvarCarrinhoNaSessao(carrinho);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult ConfirmarCarrinho()
        {
            var carrinho = ObterCarrinhoDaSessao();
            return RedirectToAction("Create", "Venda", carrinho);
        }

        // Métodos auxiliares para gerenciar o carrinho na sessão
        private Carrinho ObterCarrinhoDaSessao()
        {
            var carrinho = HttpContext.Session.GetObjectFromJson<Carrinho>(CarrinhoSessionKey);
            return carrinho ?? new Carrinho();
        }

        private void SalvarCarrinhoNaSessao(Carrinho carrinho)
        {
            HttpContext.Session.SetObjectAsJson(CarrinhoSessionKey, carrinho);
        }

    }
}
