using LojaLivros.Data;
using LojaLivros.Dtos.Response;
using LojaLivros.Models;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Sessao;
using Newtonsoft.Json;

namespace LojaLivros.Services.Emprestimo
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly DataDbContext _context;
        private readonly ISessao _sessao;
        private readonly ILivroInterface _livroInterface;
        public EmprestimoService(DataDbContext context, ISessao sessao, ILivroInterface livroInterface)
        {
                _context = context;
                _sessao = sessao;
                _livroInterface = livroInterface;
        }

        public async Task<ServiceResponse<EmprestimoModel>> Emprestar(int livroId)
        {

            ServiceResponse<EmprestimoModel> serviceResponse = new ServiceResponse<EmprestimoModel>();

            try
            {
                var sessaoUsuario =  _sessao.BuscarSessao();
                if(sessaoUsuario == null) {

                    serviceResponse.Mensagem = "É necessário estar logado para emprestar um livro!";
                    serviceResponse.Status = false;
                    return serviceResponse;
                }

                var livro = await _livroInterface.BuscarLivroPorId(livroId);

               var emprestimo =  new EmprestimoModel
                {
                    UsuarioId = sessaoUsuario.Id,
                    LivroId = livroId,
                    Usuario = sessaoUsuario,
                    Livro = livro
                };

                _context.Add(emprestimo);
                _context.SaveChangesAsync();

                BaixarEstoque(livro);


            }
            catch (Exception ex) {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Status = false;
            }

            return serviceResponse;

        }


            public async void BaixarEstoque(LivroModel livro)
            {
                livro.QuantidadeEstoque--;
                _context.Update(livro);
                _context.SaveChangesAsync();
            }

            public async void RetornarEstoque(LivroModel livro)
            {
                livro.QuantidadeEstoque++;
                _context.Update(livro);
                _context.SaveChangesAsync();
        }
    }
}
