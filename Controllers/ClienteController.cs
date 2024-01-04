using AutoMapper;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Filters;
using LojaLivros.Models;
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
        //private readonly IMapper _mapper;

        public ClienteController( IUsuarioInterface usuarioInterface, ISessao sessao)
        {
       
            _usuarioInterface = usuarioInterface;
            _sessao = sessao;
        }

        public async Task<IActionResult> Index(int? id)
        {

            if (_sessao.BuscarSessao() == null) return RedirectToAction("Login", "Home");

            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }

      

        

       

       

    }
}
