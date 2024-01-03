using LojaLivros.Models;

namespace LojaLivros.Services.Sessao
{
    public interface ISessao
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
    }
}
