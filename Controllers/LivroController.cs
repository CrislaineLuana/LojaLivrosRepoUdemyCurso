using AutoMapper;
using LojaLivros.Data;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Livro;
using LojaLivros.Filters;
using LojaLivros.Models;
using LojaLivros.Services.Livro;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Controllers
{
     [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class LivroController : Controller
    {
       
        private readonly ILivroInterface _livroInterface;
        private readonly IMapper _mapper;
        public LivroController(ILivroInterface livroInterface, IMapper mapper)
        {
             _livroInterface = livroInterface;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var livro = await _livroInterface.BuscarLivroPorId(id);



                return View(livro);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var livro = await _livroInterface.BuscarLivroPorId(id);
                var livroEditadoDto = _mapper.Map<LivroEditarDto>(livro);

                return View(livroEditadoDto);
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Cadastrar(LivroRegisterDto livroRegisterDto, IFormFile foto)
        {
      
            if(foto != null)
            {
                if (ModelState.IsValid)
                {

                    if (!_livroInterface.VerificaSeJaExisteCadastro(livroRegisterDto))
                    {

                        TempData["MensagemErro"] = "Código ISBN já cadastrado!";
                        return View(livroRegisterDto);
                    };

                    var livro = await _livroInterface.Cadastrar(livroRegisterDto, foto);

                    TempData["MensagemSucesso"] = "Livro Cadastrado com sucesso!";



                    return RedirectToAction("Index");
                }

                else
                {
                    
                    return View(livroRegisterDto);
                }
            }else
            {
                TempData["MensagemErro"] = "Incluir uma imagem de capa!";
                return View(livroRegisterDto);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Editar(LivroEditarDto livroEditarDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _livroInterface.Editar(livroEditarDto, foto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(livroEditarDto);
            }
        }
    }
}
