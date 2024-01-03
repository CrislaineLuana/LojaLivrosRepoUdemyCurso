using LojaLivros.Dtos.Login;
using LojaLivros.Dtos.Response;
using LojaLivros.Models;

namespace LojaLivros.Services.Home
{
    public interface IHomeInterface
    {
        Task<ServiceResponse<UsuarioModel>> RealizarLogin(LoginDto loginDto);
    }
}
