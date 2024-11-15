using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoService _produtoService;

        public HomeController(ILogger<HomeController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var produtos = _produtoService.GetAll();
            return View(produtos);
        }

        public IActionResult Search(string searchTerm)
        {
            var produtos = string.IsNullOrEmpty(searchTerm)
                ? _produtoService.GetAll()
                : _produtoService.GetAll()
                    .Where(p => p.Nome.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)); // Filtra produtos pelo nome

            ViewBag.SearchTerm = searchTerm; // Armazena o termo de pesquisa para exibir na View

            if (produtos.Count() < 1)
                TempData["Error"] = "Não conseguimos encontrar um produto com o termo especificado :(";

            return View("Index", produtos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
