using LojaLivros.Data;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LojaLivros.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly DataDbContext _dbContext;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public UsuarioService(DataDbContext context, IAutenticacaoInterface autenticacaoInterface) 
        {
            _dbContext = context;
            _autenticacaoInterface = autenticacaoInterface;
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
                _dbContext.Add(Usuario);
                await _dbContext.SaveChangesAsync();

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

                var mesmoUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Email == usuarioRegisterDto.Email || usuarioBanco.Usuario == usuarioRegisterDto.Usuario);

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

                var usuarioEditar = await _dbContext.Usuarios.FindAsync(usuarioEditado.Id);

                if (usuarioEditar != null)
                {
                    usuarioEditar.Turno = usuarioEditado.Turno;
                    usuarioEditar.Cargo = usuarioEditado.Cargo;
                    usuarioEditar.NomeCompleto = usuarioEditado.NomeCompleto;
                    usuarioEditar.Usuario = usuarioEditado.Usuario;
                    usuarioEditar.Email = usuarioEditado.Email;
                    usuarioEditar.DataAlteracao = DateTime.Now;


                    _dbContext.Update(usuarioEditar);
                    await _dbContext.SaveChangesAsync();

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
                var usuarioMudarSituacao = await _dbContext.Usuarios.FindAsync(idUsuario);

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
                    

                    _dbContext.Update(usuarioMudarSituacao);
                    await _dbContext.SaveChangesAsync();

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

                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == idUsuario);
                return usuario;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
           
            try
            {
                var usuarios = await _dbContext.Usuarios.ToListAsync();
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
