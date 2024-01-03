using LojaLivros.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LojaLivros.Services.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoInterface
    {

        private readonly IConfiguration _configuration;
        public AutenticacaoService(IConfiguration configuration)
        {
             _configuration = configuration;
        }
        public void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificaLogin(string senha, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateRandomToken(UsuarioModel usuario)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuario.NomeCompleto));
            claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Cargo.ToString()));


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        
    }
}
