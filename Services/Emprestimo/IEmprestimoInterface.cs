using LojaLivros.Dtos.Response;
using LojaLivros.Models;

namespace LojaLivros.Services.Emprestimo
{
    public interface IEmprestimoInterface
    {
        Task<ServiceResponse<EmprestimoModel>> Emprestar(int livroId);
        Task<List<LivroModel>> BuscarEmprestimos(UsuarioModel usuario);
        Task<EmprestimoModel> Devolver(int idEmprestimo);
    }
}
