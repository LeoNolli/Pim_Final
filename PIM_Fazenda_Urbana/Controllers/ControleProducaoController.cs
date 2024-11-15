using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models.ViewModels;
using PIM_Fazenda_Urbana.Models;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class ControleProducaoController : Controller
    {
        private readonly IControleProducaoService _controleProducaoService;
        private readonly IProdutoService _produtoService;
        private readonly IInsumoProducaoService _insumoProducaoService;
        private readonly IInsumoService _insumoService;

        public ControleProducaoController(
            IControleProducaoService controleProducaoService,
            IProdutoService produtoService,
            IInsumoProducaoService insumoProducaoService,
            IInsumoService insumoService)
        {
            _controleProducaoService = controleProducaoService;
            _produtoService = produtoService;
            _insumoProducaoService = insumoProducaoService;
            _insumoService = insumoService;
        }

        // GET: Exibe a lista de produções
        [HttpGet]
		[Authorize(Roles = "Admin, Gerente, Comum")]
		public IActionResult Index()
        {
            var producoes = _controleProducaoService.GetAll();
            return View(producoes);
        }

        // GET: Exibe o formulário para criação de uma nova produção
        [HttpGet]
		[Authorize(Roles = "Admin, Gerente, Comum")]
		public IActionResult Create(int produtoId)
        {
            try
            {
                // Inicializa o modelo de controle de produção
                var producao = new ControleProducao
                {
                    Insumos = _insumoService.GetAll().Select(i => new InsumoProducaoViewModel
                    {
                        InsumoId = i.InsumoId,
                        Nome = i.Nome,
                        QuantidadeUsada = 0 // Inicialmente a quantidade é zero
                    }).ToList()
                };
                producao.ProdutoId = produtoId;
                return View(producao);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

		[Authorize(Roles = "Admin, Gerente, Comum")]
		// POST: Recebe os dados da produção e insumos
		[HttpPost]
        public IActionResult Create(ControleProducao producao)
        {
            try
            {
                producao.FuncionarioId = int.Parse(User.FindFirst("IdPessoal").Value);

                int producaoId = _controleProducaoService.Add(producao);

                foreach (var insumo in producao.Insumos.Where(i => i.Selecionado))
                {
                    var insumoProducao = new InsumoProducao
                    {
                        ProducaoId = producaoId,
                        InsumoId = insumo.InsumoId,
                        Quantidade = insumo.QuantidadeUsada
                    };
                    _insumoProducaoService.Add(insumoProducao);

                    _insumoService.UpdateQuantidade(insumo.InsumoId, insumo.QuantidadeUsada);
                }
                return RedirectToAction("Index");

            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index");
            }
        }

        // GET: Exibe o formulário para edição de uma produção existente
        [HttpGet]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Edit(int id)
        {
            try
            {
                var producao = _controleProducaoService.GetById(id);
                if (producao == null)
                {
                    return NotFound();
                }

                // Preenche a lista de insumos disponíveis
                producao.Insumos = _insumoService.GetAll().Select(i => new InsumoProducaoViewModel
                {
                    InsumoId = i.InsumoId,
                    Nome = i.Nome,
                    QuantidadeUsada = _insumoProducaoService.GetByProducaoId(id)
                        .FirstOrDefault(ip => ip.InsumoId == i.InsumoId)?.Quantidade ?? 0
                }).ToList();

                return View(producao);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index");
            }
        }

        // POST: Atualiza os dados da produção e insumos
        [HttpPost]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Edit(ControleProducao producao)
        {
            // Se o modelo não for válido, retorna para a página de edição
            if (!ModelState.IsValid)
                return View(producao);

            try
            {

                // 1. Atualiza a produção
                _controleProducaoService.Update(producao);

                // 2. Atualizar os insumos usados na produção
                _insumoProducaoService.DeleteByProducaoId(producao.ProducaoId); // Deleta insumos antigos

                foreach (var insumo in producao.Insumos)
                {
                    var insumoProducao = new InsumoProducao
                    {
                        ProducaoId = producao.ProducaoId,
                        InsumoId = insumo.InsumoId,
                        Quantidade = insumo.QuantidadeUsada
                    };
                    _insumoProducaoService.Add(insumoProducao);

                    // Atualizar a quantidade do insumo no estoque (caso necessário)
                    _insumoService.UpdateQuantidade(insumo.InsumoId, insumo.QuantidadeUsada);
                }

                return RedirectToAction("Index");

            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index");
            }

        }

        // POST: Deleta uma produção
        [HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
        {
            try
            {
                // Deleta a produção e seus insumos
                _insumoProducaoService.DeleteByProducaoId(id); // Deleta os insumos associados
                _controleProducaoService.Delete(id); // Deleta a produção

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index");
            }
        }

		[HttpPost]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult CompleteProduction(int id)
        {
            try
            {
                var producao = _controleProducaoService.GetById(id);

                if (producao.Status == "Concluida")
                    throw new BadHttpRequestException("Ação não pode ser concluida, pois a produção ja foi concluida");

                var produto = _produtoService.GetById(producao.ProdutoId);

                produto.QuantidadeEstoque += producao.Quantidade;

                producao.Status = "Concluida";
                producao.DataConclusao = DateTime.Now;
                _produtoService.Update(produto);
                _controleProducaoService.CompleteProduction(producao);

                return RedirectToAction("Index");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index");
            }
        }
    }
}
