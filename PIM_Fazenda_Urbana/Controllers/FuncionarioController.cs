using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Index()
        {
            try
            {
                return View(_funcionarioService.GetFuncionarios());
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Funcionario");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index", "Funcionario");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                return RedirectToAction("Index", "Funcionario");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Edit([FromRoute] int id)
        {
            try
            {
                return View(_funcionarioService.GetFuncionarioByrId(id));
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Funcionario");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index", "Funcionario");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Edit(Funcionario funcionario)
        {
            try
            {
                _funcionarioService.UpdateFuncionario(funcionario);
                return RedirectToAction("Index", "Funcionario");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Funcionario");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao concluir a ação. Tente novamente por favor";
                return RedirectToAction("Index", "Funcionario");
            }
        }

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			try
			{
				_funcionarioService.Delete(id);
				return RedirectToAction("Index", "Funcionario");
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
