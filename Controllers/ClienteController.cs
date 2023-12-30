using AutoMapper;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
    public class ClienteController : Controller
    {
        
        private readonly IUsuarioInterface _usuarioInterface;
        //private readonly IMapper _mapper;

        public ClienteController( IUsuarioInterface usuarioInterface)
        {
       
            _usuarioInterface = usuarioInterface;
        
        }

        public async Task<IActionResult> Index(int? id)
        {
            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }

      

        

       

       

    }
}
