using LojaLivros.Data;
using LojaLivros.Dtos.Login;
using LojaLivros.Models;
using LojaLivros.Services.Autenticacao;

namespace LojaLivros.Services.Home
{
    public class HomeService : IHomeInterface
    {
        private readonly DataDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        public HomeService(DataDbContext context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
                _autenticacaoInterface = autenticacaoInterface;
        }

        public async Task<ClienteModel> RealizarLogin(LoginDto loginDto)
        {
            try
            {
                // var cliente = _context.Clientes.

                //Verificar se o login confere

                //Gerar Token e colocar no localhost
                //Entrar usuário

                return new ClienteModel();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
