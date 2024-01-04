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

        public IActionResult Emprestar(int id)
        {
            var emprestimo = _emprestimoInterface.Emprestar(id);
            return View();
        }
    }
}
