using AutoMapper;
using LojaLivros.Data;
using LojaLivros.Dtos.Livro;
using LojaLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaLivros.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly DataDbContext _context;
        private readonly IMapper _mapper;
        private string _caminhoServidor;
        public LivroService(DataDbContext context, IMapper mapper, IWebHostEnvironment sistema)
        {
            _context = context;
            _mapper = mapper;
            _caminhoServidor = sistema.WebRootPath;
        }

        public async Task<List<LivroModel>> BuscarLivros()
        {

            try
            {
                var livros = await _context.Livros.ToListAsync();
                return livros;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LivroModel>> BuscarLivrosFiltro(string filtro)
        {

            try
            {
                var livros = await _context.Livros.Where(livro => livro.Titulo.Contains(filtro) || livro.Autor.Contains(filtro)).ToListAsync();
                return livros;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuario)
        {

            // var livro = await _context.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
            if(usuario == null)
            {
                var emprestimoSemUsuario = await _context.Emprestimos.Include(livro => livro.Livro).FirstOrDefaultAsync(emprestimo => emprestimo.LivroId == id);

                if(emprestimoSemUsuario == null)
                {
                    var livro = await BuscarLivroPorId(id);
                    
                    var emprestimoBanco = new EmprestimoModel
                    {
                        Livro = livro,
                        Usuario = null
                    };
                    return emprestimoBanco;
                }

                return emprestimoSemUsuario;
            }


            var emprestimo = await _context.Emprestimos.Include(livro => livro.Livro).Include(usuario => usuario.Usuario).FirstOrDefaultAsync(emprestimo => emprestimo.LivroId == id && emprestimo.DataDevolução == null);

            if(emprestimo == null)
            {
                var livro = await BuscarLivroPorId(id);

                var emprestimoBanco = new EmprestimoModel
                {
                    Livro = livro,
                    Usuario = usuario
                };

                return emprestimoBanco;
            }


            return emprestimo;
        }




        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {

             var livro = await _context.Livros.Include(emprestimo => emprestimo.Emprestimos).FirstOrDefaultAsync(livro => livro.Id == id);
            

            return livro;
        }


        public async Task<LivroRegisterDto> Cadastrar(LivroRegisterDto livroDto, IFormFile foto)
        {
            try
            {
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + livroDto.ISBN + ".png";

                string caminhoParaSalvarImagens = _caminhoServidor + "\\imagem\\";

                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
                {
                    foto.CopyToAsync(stream).Wait();
                }

                var livro = _mapper.Map<LivroModel>(livroDto);
               livro.Capa = nomeCaminhoImagem;


                _context.Add(livro);
                await _context.SaveChangesAsync();

                return livroDto;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<LivroEditarDto> Editar(LivroEditarDto livroDto, IFormFile foto)
        {
            try
            {
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + livroDto.ISBN + ".png";

                string caminhoParaSalvarImagens = _caminhoServidor + "\\imagem\\";

                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
                {
                    foto.CopyToAsync(stream).Wait();
                }

                var livro = _mapper.Map<LivroModel>(livroDto);

                livro.Capa = nomeCaminhoImagem;
                livro.DataAlteracao = DateTime.Now;


                _context.Update(livro);
                await _context.SaveChangesAsync();

                return livroDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaSeJaExisteCadastro(LivroRegisterDto livroDto)
        {
            try
            {
                var livroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroDto.ISBN);
                if (livroBanco != null)
                {
                    return false;
                }
              

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
               
            }
           

        }
    }
}
