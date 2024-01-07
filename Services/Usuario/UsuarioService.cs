using AutoMapper;
using LojaLivros.Data;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Relatorios;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LojaLivros.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly DataDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;

        public UsuarioService(DataDbContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper) 
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }


        public async Task<UsuarioRegisterDto> Cadastrar(UsuarioRegisterDto usuarioRegisterDto)
        {

            try
            {

                 _autenticacaoInterface.CriarPasswordHash(usuarioRegisterDto.Senha, out byte[] passwordHash, out byte[] passwordSalt);

                var Usuario = new UsuarioModel
                {
                    NomeCompleto = usuarioRegisterDto.NomeCompleto,
                    Usuario = usuarioRegisterDto.Usuario,
                    Email = usuarioRegisterDto.Email,
                    Cargo = usuarioRegisterDto.Cargo,
                    Turno = usuarioRegisterDto.Turno,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                //Criar Cadastro banco
                _context.Add(Usuario);
                await _context.SaveChangesAsync();

                var usuarioCriado = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == usuarioRegisterDto.Email);

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioRegisterDto.Logradouro,
                    Bairro = usuarioRegisterDto.Bairro,
                    CEP = usuarioRegisterDto.CEP,
                    Complemento = usuarioRegisterDto.Complemento,
                    Numero = usuarioRegisterDto.Numero,
                    Estado = usuarioRegisterDto.Estado,
                    UsuarioId = usuarioCriado.Id
                };

                _context.Add(endereco);
                await _context.SaveChangesAsync();

                return usuarioRegisterDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }

        public async Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioRegisterDto usuarioRegisterDto)
        {
            try
            {   

                var mesmoUsuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Email == usuarioRegisterDto.Email || usuarioBanco.Usuario == usuarioRegisterDto.Usuario);

                if (mesmoUsuario == null)
                {
                    return true;
                }

                return false;


            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
       

        public async Task<UsuarioEditarDto> Editar(UsuarioEditarDto usuarioEditado)
        {

            try
            {

                var usuarioEditar = await _context.Usuarios.Include(endereco => endereco.Endereco).FirstOrDefaultAsync(usuario => usuario.Id == usuarioEditado.Id);

                if (usuarioEditar != null)
                {
                    usuarioEditar.Turno = usuarioEditado.Turno;
                    usuarioEditar.Cargo = usuarioEditado.Cargo;
                    usuarioEditar.NomeCompleto = usuarioEditado.NomeCompleto;
                    usuarioEditar.Usuario = usuarioEditado.Usuario;
                    usuarioEditar.Email = usuarioEditado.Email;
                    usuarioEditar.DataAlteracao = DateTime.Now;
                    usuarioEditar.Endereco = _mapper.Map<EnderecoModel>(usuarioEditado.Endereco);


                    _context.Update(usuarioEditar);
                    await _context.SaveChangesAsync();

                    return usuarioEditado;
                }

                return usuarioEditado;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> MudarSituacaoUsuario(int idUsuario)
        {
            try
            {
                var usuarioMudarSituacao = await _context.Usuarios.FindAsync(idUsuario);

                if(usuarioMudarSituacao != null)
                {
                    if (usuarioMudarSituacao.Situação == true)
                    {
                        usuarioMudarSituacao.Situação = false;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;

                    }else
                    {
                        usuarioMudarSituacao.Situação = true;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;
                    }
                    

                    _context.Update(usuarioMudarSituacao);
                    await _context.SaveChangesAsync();

                    return usuarioMudarSituacao;
                }


                return usuarioMudarSituacao;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int? idUsuario)
        {
            try
            {

                var usuario = await _context.Usuarios.Include(endereco => endereco.Endereco).FirstOrDefaultAsync(usuario => usuario.Id == idUsuario);
                return usuario;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? Id)
        {
           
            try
            {
                var registros = new List<UsuarioModel>();
                if (Id != null)
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Cargo == 0).Include(endereco => endereco.Endereco). ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Cargo != 0).Include(endereco => endereco.Endereco).ToListAsync();
                }

                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     



    }
}
