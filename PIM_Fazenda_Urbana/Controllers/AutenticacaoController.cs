using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;
using PIM_Fazenda_Urbana.Services;

namespace PIM_Fazenda_Urbana.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IClienteService _clienteService;
        public AutenticacaoController(IUsuarioService usuarioService, IFuncionarioService funcionarioService, IClienteService clienteService)
        {
            _usuarioService = usuarioService;
            _funcionarioService = funcionarioService;
            _clienteService = clienteService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Registrar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult RegistrarClienteAction(UsuarioRegistroDTO novoUsuario)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                string senha = novoUsuario.Senha;

                int usuarioId = _usuarioService.CreateUsuario(novoUsuario);

                Cliente novoCliente = new Cliente(novoUsuario);

                novoCliente.UsuarioId = usuarioId;

                _clienteService.CreateCliente(novoCliente);

                return LoginAction(novoUsuario.Email, senha).Result;
            }
            catch (Exception ex)
            {
                TempData["RegisterError"] = "Houve um erro ao concluir o registo. Tente novamente por favor";
                return RedirectToAction("Registrar");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegistrarFuncionarioAction(UsuarioFuncionarioRegistroDTO novoFuncionario)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                int usuarioID = _usuarioService.CreateUsuario(novoFuncionario);
                _funcionarioService.CreateFuncionario(novoFuncionario, usuarioID);

				return RedirectToAction("Index", "Funcionario");
			}
			catch (Exception ex)
            {
                TempData["RegisterError"] = "Houve um erro ao concluir o registo. Tente novamente por favor";
				return RedirectToAction("Index", "Funcionario");
			}
		}

        [AllowAnonymous]
        public async Task<IActionResult> LoginAction(string email, string senha)
        {
            var usuario = _usuarioService.GetByEmail(email);

            if (usuario == null || usuario.Senha != UsuarioService.HashPassword(senha))
            {
                TempData["LoginError"] = "email ou senha estão incorretos. Tente novamente!";
                return RedirectToAction("Login", "Autenticacao");
            }

            int pessoaId;

            if (usuario.Funcao == "Cliente")
            {
                var cliente = _clienteService.GetClienteByUsuarioId(usuario.UsuarioId);
                pessoaId = cliente.Id;
            }
            else
            {
                var funcionario = _funcionarioService.GetFuncionarioByUsuarioId(usuario.UsuarioId);
                pessoaId = funcionario.FuncionarioId;
            }

            await AutenticacaoService.Login(HttpContext, usuario, pessoaId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await AutenticacaoService.Logout(HttpContext);
            return Redirect("/Autenticacao/Index");
        }

    }
}
