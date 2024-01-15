using LojaLivros.Filters;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class FuncionarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        //private readonly IMapper _mapper;

        public FuncionarioController(IUsuarioInterface usuarioInterface)
        {

            _usuarioInterface = usuarioInterface;

        }

        public async Task<IActionResult> Index()
        {
            var funcionarios = await _usuarioInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }

      

    }
}
