namespace LojaLivros.Services.Autenticacao
{
    public interface IAutenticacaoInterface
    {
        public void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
