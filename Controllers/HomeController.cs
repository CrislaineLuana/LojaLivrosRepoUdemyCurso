using LojaLivros.Dtos.Login;
using LojaLivros.Models;
using LojaLivros.Services.Home;
using LojaLivros.Services.Sessao;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaLivros.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeInterface _homeInterface;
        private readonly ISessao _sessao;
        public HomeController(IHomeInterface homeInterface, ISessao sessao)
        {
            _homeInterface = homeInterface;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // Se usuário logado redireciona para Home
           

            return View();
        }


        public IActionResult Login()
        {
            if (_sessao.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessao();

            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var login = await _homeInterface.RealizarLogin(loginDto);
                if(login != null) { 
                if(login.Status == false)
                {
                    TempData["MensagemErro"] = "Credenciais inválidas!";
                    return View(login.Dados);
                }

                _sessao.CriarSessao(login.Dados);
                TempData["MensagemSucesso"] = "Usuário logado com sucesso!";
                return RedirectToAction("Index");
            }
            }
            return Ok();
        }
    }
}