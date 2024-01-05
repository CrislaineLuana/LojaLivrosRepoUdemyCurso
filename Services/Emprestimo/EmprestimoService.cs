using LojaLivros.Data;
using LojaLivros.Dtos.Response;
using LojaLivros.Models;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Sessao;
using Microsoft.EntityFrameworkCore;
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
                    //Usuario = sessaoUsuario,
                    Livro = livro
                };

                _context.Add(emprestimo);
                await _context.SaveChangesAsync();

           
              

                var livroEstoque = await BaixarEstoque(livro);

                serviceResponse.Dados = emprestimo;
            }
            catch (Exception ex) {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Status = false;
            }

            return serviceResponse;

        }


        


        public async Task<List<LivroModel>> BuscarEmprestimos(UsuarioModel usuario)
        {

           
            try
            {
                //var usuarioEmprestimos3 = await _context.Emprestimos.Where(usuario => usuario.UsuarioId == usuario.Id).Include(livro => livro.Livro).Include(usuario => usuario.Usuario).ToListAsync();

                var usuarioEmprestimos2 = await _context.Livros.Include(emprestimos => emprestimos.Emprestimos).Where(usuario => usuario.Id == usuario.Id).ToListAsync();


                return usuarioEmprestimos2;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

         
        }

        public async Task<EmprestimoModel> Devolver(int idEmprestimo)
        {


            try
            {
                var emprestimo = await _context.Emprestimos.Include(livro => livro.Livro).FirstOrDefaultAsync(emprestimo => emprestimo.Id == idEmprestimo);

                if(emprestimo == null)
                {
                    throw new Exception("Empéstimo não localizado!");
                }

                emprestimo.DataDevolução = DateTime.Now;

                _context.Update(emprestimo);
                await _context.SaveChangesAsync();

                var livroEstoque = await RetornarEstoque(emprestimo.Livro);

                return emprestimo;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }



        public async Task<LivroModel> BaixarEstoque(LivroModel livro)
            {
                livro.QuantidadeEstoque--;
                _context.Update(livro);
                await _context.SaveChangesAsync();

                return livro;
            }

            public async Task<LivroModel> RetornarEstoque(LivroModel livro)
            {
                livro.QuantidadeEstoque++;
                _context.Update(livro);
                await _context.SaveChangesAsync();

            return livro;
        }
    }
}
