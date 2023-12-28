using System.Security.Cryptography;

namespace LojaLivros.Services.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoInterface
    {


        public void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        //public bool VerificaLogin(string email, string password)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {

        //    }
        //}

    }
}
