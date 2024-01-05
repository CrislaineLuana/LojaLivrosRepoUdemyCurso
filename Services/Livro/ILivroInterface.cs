using LojaLivros.Dtos.Livro;
using LojaLivros.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaLivros.Services.Livro
{
    public interface ILivroInterface
    {
        Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuario);
        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<List<LivroModel>> BuscarLivros();
        Task<LivroRegisterDto> Cadastrar(LivroRegisterDto livroDto, IFormFile foto);
        bool VerificaSeJaExisteCadastro(LivroRegisterDto livroDto);
        Task<LivroEditarDto> Editar(LivroEditarDto livroDto, IFormFile foto);
        Task<List<LivroModel>> BuscarLivrosFiltro(string filtro);
    }

}
