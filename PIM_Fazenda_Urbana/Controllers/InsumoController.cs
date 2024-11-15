using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class InsumoController : Controller
    {
        private readonly IInsumoService _insumoService;

        public InsumoController(IInsumoService insumoService)
        {
            _insumoService = insumoService;
        }

        [HttpGet]
		[Authorize(Roles = "Admin, Gerente, Comum")]
		public IActionResult Index()
        {
            try
            {
                var insumos = _insumoService.GetAll();
                return View(insumos);
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

        [HttpPost]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Create(Insumo insumo)
        {
            try
            {
                _insumoService.Add(insumo);
                return RedirectToAction("Index", "Insumo");
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
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Edit(int id)
        {
            try
            {
                var insumo = _insumoService.GetById(id);
                return View(insumo);
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

        [HttpPost]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Edit(Insumo insumo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _insumoService.Update(insumo);
                    return RedirectToAction("Index");
                }
                return View(insumo);
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

        [HttpPost]
		[Authorize(Roles = "Admin, Gerente")]
		public IActionResult Delete(int id)
        {
            try
            {
                _insumoService.Delete(id);
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
