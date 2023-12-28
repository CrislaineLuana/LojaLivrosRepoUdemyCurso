using AutoMapper;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Cliente;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteInterface _clienteInterface;
        private readonly IMapper _mapper;

        public ClienteController(IClienteInterface clienteInterface, IMapper mapper)
        {
            _clienteInterface = clienteInterface;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteInterface.buscarClientes();
            return View(clientes);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var cliente = await _clienteInterface.BuscarClientePorId(id);



                return View(cliente);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var cliente = await _clienteInterface.BuscarClientePorId(id);

                var clienteEditado = new ClienteEditarDto
                {
                    NomeCompleto = cliente.NomeCompleto,
                    Email = cliente.Email,
                    Endereco =  _mapper.Map<EnderecoEditarDto>(cliente.Endereco) ,// cliente.Endereco,
                    Id = cliente.Id,
                    Usuario = cliente.Usuario
                };
                return View(clienteEditado);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> MudarSituacaoCliente(ClienteModel clienteModificar)
        {
            if (clienteModificar.Id != 0 && clienteModificar.Id != null)
            {
                var cliente = await _clienteInterface.MudarSituacaoCliente(clienteModificar.Id);

                if (cliente.Situação == true)
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
        public async Task<IActionResult> Cadastrar(ClienteRegisterDto clienteRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _clienteInterface.Cadastrar(clienteRegisterDto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clienteRegisterDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ClienteEditarDto clienteEditarDto)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _clienteInterface.Editar(clienteEditarDto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clienteEditarDto);
            }
        }

    }
}
