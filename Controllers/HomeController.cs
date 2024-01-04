using LojaLivros.Dtos.Login;
using LojaLivros.Models;
using LojaLivros.Services.Home;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Sessao;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaLivros.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeInterface _homeInterface;
        private readonly ISessao _sessao;
        private readonly ILivroInterface _livroInterface;
        public HomeController(IHomeInterface homeInterface, ISessao sessao, ILivroInterface livroInterface)
        {
            _homeInterface = homeInterface;
            _sessao = sessao;
            _livroInterface = livroInterface;
        }

        public async Task<IActionResult> Index(string Pesquisar = null)
        {

            // Se usuário logado redireciona para Home
            var usuarioSessao = _sessao.BuscarSessao();
            if (usuarioSessao != null)
            {
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }
            if (Pesquisar == null)
            {
                


                IEnumerable<LivroModel> livrosdb = await _livroInterface.BuscarLivros();
                return View(livrosdb);
            }
            else
            {
                IEnumerable<LivroModel> livros = await _livroInterface.BuscarLivrosFiltro(Pesquisar);
                return View(livros);
            }
            
          
        }

        public async Task <IActionResult> Detalhes(int? id)
        {
            var usuarioSessao = _sessao.BuscarSessao();
            if (usuarioSessao != null)
            {
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }

            var livro = await _livroInterface.BuscarLivroPorId(id);
           return View(livro);
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