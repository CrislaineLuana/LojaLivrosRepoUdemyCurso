using LojaLivros.Dtos.Login;
using LojaLivros.Models;

namespace LojaLivros.Services.Home
{
    public interface IHomeInterface
    {
        Task<ClienteModel> RealizarLogin(LoginDto loginDto);
    }
}
