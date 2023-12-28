using AutoMapper;
using LojaLivros.Data;
using LojaLivros.Dtos.Clientes;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;
using LojaLivros.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LojaLivros.Services.Cliente
{
    public class ClienteService: IClienteInterface
    {
        private readonly DataDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;
        public ClienteService(DataDbContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }

        public async Task<List<ClienteModel>> buscarClientes()
        {

            try
            {

                var clientes = await _context.Clientes.Include(c => c.Endereco).ToListAsync();
                return clientes; 
                

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteModel> MudarSituacaoCliente(int? idCliente)
        {
            try
            {
                var clienteMudarSituacao = await _context.Clientes.FindAsync(idCliente);

                if (clienteMudarSituacao != null)
                {
                    if (clienteMudarSituacao.Situação == true)
                    {
                        clienteMudarSituacao.Situação = false;
                        clienteMudarSituacao.DataAlteracao = DateTime.Now;

                    }
                    else
                    {
                        clienteMudarSituacao.Situação = true;
                        clienteMudarSituacao.DataAlteracao = DateTime.Now;
                    }


                    _context.Update(clienteMudarSituacao);
                    await _context.SaveChangesAsync();

                    return clienteMudarSituacao;
                }


                return clienteMudarSituacao;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
            public async Task<ClienteModel> BuscarClientePorId(int? idCliente)
        {
            try
            {

                var cliente = await _context.Clientes.Include(c => c.Endereco).FirstOrDefaultAsync(cliente => cliente.Id == idCliente);
                return cliente;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteEditarDto> Editar(ClienteEditarDto clienteEditarDto)
        {
            try
            {
                var clienteBanco = await _context.Clientes.Include(c => c.Endereco).FirstOrDefaultAsync(cliente => cliente.Id == clienteEditarDto.Id);

                if (clienteBanco != null)
                {
                    clienteBanco.NomeCompleto = clienteEditarDto.NomeCompleto;
                    clienteBanco.Usuario = clienteEditarDto.Usuario;
                    clienteBanco.Email = clienteEditarDto.Email;
                    //clienteBanco.Endereco.CEP = clienteEditarDto.Endereco.CEP;
                    //clienteBanco.Endereco.Logradouro = clienteEditarDto.Endereco.Logradouro;
                    //clienteBanco.Endereco.Numero = clienteEditarDto.Endereco.Numero;
                    //clienteBanco.Endereco.Complemento = clienteEditarDto.Endereco.Complemento;
                    //clienteBanco.Endereco.Bairro = clienteEditarDto.Endereco.Bairro;
                    //clienteBanco.Endereco.Id = clienteEditarDto.Endereco.Id;
                    //clienteBanco.Endereco.ClienteId = clienteEditarDto.Endereco.ClienteId;//clienteEditarDto.Endereco;
                    clienteBanco.Endereco = _mapper.Map<EnderecoModel>(clienteEditarDto.Endereco);


                    clienteBanco.DataAlteracao = DateTime.Now;


                    _context.Update(clienteBanco);
                    await _context.SaveChangesAsync();

                    return clienteEditarDto;
                }

                return clienteEditarDto;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


            public async Task<ClienteRegisterDto> Cadastrar(ClienteRegisterDto clienteRegisterDto)
        {

            try
            {

                _autenticacaoInterface.CriarPasswordHash(clienteRegisterDto.Senha, out byte[] passwordHash, out byte[] passwordSalt);

              

                var Cliente = new ClienteModel
                {
                    NomeCompleto = clienteRegisterDto.NomeCompleto,
                    Usuario = clienteRegisterDto.Usuario,
                    Email = clienteRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };

                //Criar Cadastro banco
                _context.Add(Cliente);
                await _context.SaveChangesAsync();

                var clienteCriado = await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Email == clienteRegisterDto.Email);

                var endereco = new EnderecoModel
                {
                    Logradouro = clienteRegisterDto.Logradouro,
                    Bairro = clienteRegisterDto.Bairro,
                    CEP = clienteRegisterDto.CEP,
                    Complemento = clienteRegisterDto.Complemento,
                    Numero = clienteRegisterDto.Numero,
                    Estado = clienteRegisterDto.Estado,
                    ClienteId = clienteCriado.Id
                };

                _context.Add(endereco);               
                await _context.SaveChangesAsync();


                return clienteRegisterDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
