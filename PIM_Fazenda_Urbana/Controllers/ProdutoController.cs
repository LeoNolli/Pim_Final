using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: Lista todos os produtos
        [HttpGet]
        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Index()
        {
            try
            {
                var produtos = _produtoService.GetAll();
                return View(produtos);
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProductView(int id)
        {
            try
            {
                return View(_produtoService.GetById(id));
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Exibe o formulário de criação de produto
        [HttpGet]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Create()
        {
            try
            {
                return View();
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

        // POST: Cria um novo produto
        [HttpPost]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Create(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoService.Add(produto);
                    return RedirectToAction("Index");
                }

                return View(produto);
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

        // GET: Exibe o formulário de edição de um produto existente
        [HttpGet]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Edit(int id)
        {
            try
            {
                var produto = _produtoService.GetById(id);
                if (produto == null)
                {
                    return NotFound();
                }

                return View(produto);
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

        // POST: Atualiza um produto existente
        [HttpPost]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Edit(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoService.Update(produto);
                    return RedirectToAction("Index");
                }

                return View(produto);
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

        // POST: Deleta um produto
        [HttpPost]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Delete(int id)
        {
            try
            {
                _produtoService.Delete(id);
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
