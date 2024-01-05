using AutoMapper;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Filters;
using LojaLivros.Models;
using LojaLivros.Services.Emprestimo;
using LojaLivros.Services.Sessao;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    [UsuarioLogado]
    public class ClienteController : Controller
    {
        
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessao _sessao;
        private readonly IEmprestimoInterface _emprestimoInterface;
        //private readonly IMapper _mapper;

        public ClienteController( IUsuarioInterface usuarioInterface, ISessao sessao, IEmprestimoInterface emprestimoInterface)
        {
       
            _usuarioInterface = usuarioInterface;
            _sessao = sessao;
            _emprestimoInterface = emprestimoInterface;
        }

        public async Task<IActionResult> Index(int? id)
        {

            if (_sessao.BuscarSessao() == null) return RedirectToAction("Login", "Home");

            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }


        public async Task<IActionResult> Perfil()
        {

            var sessaoUsuario = _sessao.BuscarSessao();

            if (sessaoUsuario == null) return RedirectToAction("Login", "Home");

            var emprestimosUsuario = await _emprestimoInterface.BuscarEmprestimos(sessaoUsuario);

            return View(emprestimosUsuario);
        }







    }
}
