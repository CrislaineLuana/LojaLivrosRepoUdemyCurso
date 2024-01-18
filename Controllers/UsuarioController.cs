using AutoMapper;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Enums;
using LojaLivros.Filters;
using LojaLivros.Models;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioInterface usuarioInterface, IMapper mapper)
        {
            _usuarioInterface = usuarioInterface;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? Id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(Id);
            return View(usuarios);
        }

        public IActionResult Cadastrar(int? id)
        {
            UsuarioRegisterDto usuarioRegisterDto = new UsuarioRegisterDto();

            ViewBag.Cargo = PerfilEnum.Operador;

            if (id != null)
            {
                //usuarioRegisterDto = new UsuarioRegisterDto();

                ViewBag.Cargo = PerfilEnum.Cliente;
            }

          

            return View(null);
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
                    Usuario = usuario.Usuario,
                    Endereco = _mapper.Map<EnderecoEditarDto>(usuario.Endereco)
                };
                return View(usuarioEditado);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> MudarSituacaoUsuario(UsuarioModel usuario)
        {
            if (usuario.Id != 0 && usuario.Id != null )
            {
                var usuarioBanco = await _usuarioInterface.MudarSituacaoUsuario(usuario.Id);
                
                if(usuarioBanco.Situação == true)
                {
                    TempData["MensagemSucesso"] = "Usuário ativo com sucesso!";
                }
                else
                {
                   TempData["MensagemSucesso"] = "Inativação realizada com sucesso!";
                }
                if(usuarioBanco.Cargo != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                return RedirectToAction("Index", "Cliente",new { Id = "0"});
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
                if (usuario.Cargo != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                return RedirectToAction("Index", "Cliente", new { Id = "0" });

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

                if (usuario.Cargo != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                return RedirectToAction("Index", "Cliente", new { Id = "0" });
            }
            else
            {
                return View(usuarioEditado);
            }

        }
    }
}
