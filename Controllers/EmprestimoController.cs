using LojaLivros.Services.Emprestimo;
using LojaLivros.Services.Home;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Sessao;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ISessao _sessao;
        private readonly ILivroInterface _livroInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        public EmprestimoController( ISessao sessao, ILivroInterface livroInterface, IEmprestimoInterface emprestimoInterface)
        {
            _sessao = sessao;
            _livroInterface = livroInterface;
            _emprestimoInterface = emprestimoInterface;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Emprestar(int id)
        {

            var sessaoUsuario = _sessao.BuscarSessao();
            if(sessaoUsuario == null)
            {
                TempData["MensagemErro"] = "É necessário estar logado para emprestar livros!";
                return RedirectToAction("Login", "Home");
            }

            var emprestimo = await _emprestimoInterface.Emprestar(id);

            TempData["MensagemSucesso"] = "Emprestimo realizado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Devolver(int id)
        {

            var sessaoUsuario = _sessao.BuscarSessao();
            if (sessaoUsuario == null)
            {
                TempData["MensagemErro"] = "É necessário estar logado para devolver livros!";
                return RedirectToAction("Login", "Home");
            }

            var emprestimo = await _emprestimoInterface.Devolver(id);
            
            TempData["MensagemSucesso"] = "Devolução realizada com sucesso!";
            return RedirectToAction("Index", "Home");
        }


    }
}
