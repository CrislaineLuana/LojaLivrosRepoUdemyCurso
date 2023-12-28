using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();
            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public async Task<IActionResult> Detalhes(int? Id)
        {
            if(Id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(Id);



                return View(usuario);
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Editar(int? Id)
        {
            if (Id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(Id);

                var usuarioEditado = new UsuarioEditarDto
                {
                    NomeCompleto = usuario.NomeCompleto,
                    Email = usuario.Email,
                    Cargo = usuario.Cargo,
                    Turno = usuario.Turno,
                    Id = usuario.Id,
                    Usuario = usuario.Usuario
                };
                return View(usuarioEditado);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> MudarSituacaoUsuario(UsuarioModel usuarioInativar)
        {
            if (usuarioInativar.Id != 0 && usuarioInativar.Id != null )
            {
                var usuario = await _usuarioInterface.MudarSituacaoUsuario(usuarioInativar.Id);
                
                if(usuario.Situação == true)
                {
                    TempData["MensagemSucesso"] = "Usuário ativo com sucesso!";
                }
                else
                {
                   TempData["MensagemSucesso"] = "Inativação realizada com sucesso!";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioRegisterDto usuarioRegisterDto)
        {
            if (ModelState.IsValid)
            {
                if (!await _usuarioInterface.VerificaSeExisteUsuarioEEmail(usuarioRegisterDto))
                {
                    TempData["MensagemErro"] = "Já existe email/usuário cadastrado!";
                    return View(usuarioRegisterDto);
                }
                var usuario = await _usuarioInterface.Cadastrar(usuarioRegisterDto);
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuarioRegisterDto);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioEditarDto usuarioEditado)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Editar(usuarioEditado);
                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuarioEditado);
            }

        }
    }
}
