using LojaLivros.Models;

namespace LojaLivros.Services.Autenticacao
{
    public interface IAutenticacaoInterface
    {
        public void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerificaLogin(string senha, byte[] passwordHash, byte[] passwordSalt);
        string CreateRandomToken(UsuarioModel usuario);
    }
}
