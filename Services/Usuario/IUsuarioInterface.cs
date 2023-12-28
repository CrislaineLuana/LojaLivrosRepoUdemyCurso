using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;

namespace LojaLivros.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<UsuarioRegisterDto> Cadastrar(UsuarioRegisterDto usuarioRegisterDto);
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BuscarUsuarioPorId(int? idUsuario);
        Task<UsuarioModel> MudarSituacaoUsuario(int idUsuario);
        Task<UsuarioEditarDto> Editar(UsuarioEditarDto usuarioEditado);
        Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioRegisterDto usuarioRegisterDto);

    }
}
