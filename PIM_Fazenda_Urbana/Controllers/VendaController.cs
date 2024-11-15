using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PIM_Fazenda_Urbana.Extensions;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using System.Xml.Linq;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaService _vendaService;
        private const string CarrinhoSessionKey = "Carrinho";


        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Index()
        {
            try
            {
                var vendas = _vendaService.GetAll();
                return View(vendas);
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao carregar as vendas.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Details(int id)
        {
            try
            {
                var venda = _vendaService.GetVendaViewModelById(id);
                if (venda == null)
                {
                    TempData["Error"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao carregar os detalhes da venda.";
                return RedirectToAction("Index");
            }
        }


        [Authorize(Roles = "Admin, Gerente, Comum")]
        [HttpPost]
        public IActionResult Create()
        {
            try
            {
                var carrinho = ObterCarrinhoDaSessao();
                if (ModelState.IsValid)
                {
                    int funcionarioId = int.Parse(User.FindFirst("IdPessoal").Value);

                    int vendaId = _vendaService.Add(carrinho, funcionarioId);

                    LimparCarrinho();

                    TempData["Sucess"] = "Venda criada com sucesso!";

                    return RedirectToAction("Details", new { id = vendaId });
                }
                else
                    throw new Exception("Modelo não válido");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao criar a venda. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Edit(int id)
        {
            try
            {
                var venda = _vendaService.GetById(id);
                if (venda == null)
                {
                    TempData["Error"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao carregar a venda para edição.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        [HttpPost]
        public IActionResult Edit(Venda venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vendaService.Update(venda);
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao atualizar a venda. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        public IActionResult Delete(int id)
        {
            try
            {
                var venda = _vendaService.GetById(id);
                if (venda == null)
                {
                    TempData["Error"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao carregar a venda para exclusão.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, Gerente, Comum")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _vendaService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao excluir a venda. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ExportarPdf(DateTime? dataInicio, DateTime? dataFim)
        {
            try
            {
                var vendas = _vendaService.GetVendasByPeriodo(dataInicio, dataFim);
                var pdfBytes = GeneratePdf(vendas, dataInicio, dataFim);
                return File(pdfBytes, "application/pdf", "RelatorioVendas.pdf");
            }
            catch (BadHttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Houve um erro ao excluir a venda. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        private byte[] GeneratePdf(IEnumerable<Venda> vendas, DateTime? dataInicio, DateTime? dataFim)
        {
            using (var stream = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var graphics = XGraphics.FromPdfPage(page);

                var fontTitle = new XFont("Arial", 18, XFontStyleEx.Bold);
                var fontRegular = new XFont("Arial", 12, XFontStyleEx.Regular);

                // Título e intervalo de datas
                graphics.DrawString("Relatório de Vendas", fontTitle, XBrushes.Black, new XRect(0, 20, page.Width, 40), XStringFormats.TopCenter);
                graphics.DrawString($"Período: {(dataInicio.HasValue ? dataInicio.Value.ToString("dd/MM/yyyy") : "Início")} - {(dataFim.HasValue ? dataFim.Value.ToString("dd/MM/yyyy") : "Agora")}",
                    fontRegular, XBrushes.Black, new XRect(0, 40, page.Width, 40), XStringFormats.TopCenter);

                // Cabeçalho da tabela
                int yOffset = 80;
                graphics.DrawString("ID da Venda", fontRegular, XBrushes.DarkGreen, 40, yOffset);
                graphics.DrawString("Data e Hora", fontRegular, XBrushes.DarkGreen, 140, yOffset);
                graphics.DrawString("Valor Total", fontRegular, XBrushes.DarkGreen, 240, yOffset);
                graphics.DrawString("Funcionário ID", fontRegular, XBrushes.DarkGreen, 440, yOffset);

                // Dados da tabela
                yOffset += 20;

                foreach (var venda in vendas)
                {
                    graphics.DrawString(venda.VendaId.ToString(), fontRegular, XBrushes.Black, 40, yOffset);
                    graphics.DrawString(venda.DataHora.ToString("dd/MM/yyyy HH:mm"), fontRegular, XBrushes.Black, 140, yOffset);
                    graphics.DrawString($"R$ {venda.ValorTotal:F2}", fontRegular, XBrushes.Black, 240, yOffset);
                    graphics.DrawString(venda.FuncionarioId.ToString(), fontRegular, XBrushes.Black, 440, yOffset);
                    yOffset += 20;
                }

                document.Save(stream);
                return stream.ToArray();
            }
        }
        private Carrinho ObterCarrinhoDaSessao()
        {
            var carrinho = HttpContext.Session.GetObjectFromJson<Carrinho>(CarrinhoSessionKey);
            return carrinho ?? new Carrinho();
        }
        private void SalvarCarrinhoNaSessao(Carrinho carrinho)
        {
            HttpContext.Session.SetObjectAsJson(CarrinhoSessionKey, carrinho);
        }

        private void LimparCarrinho()
        {
            var carrinho = ObterCarrinhoDaSessao();
            carrinho.Limpar();
            SalvarCarrinhoNaSessao(carrinho);
        }
    }
}
