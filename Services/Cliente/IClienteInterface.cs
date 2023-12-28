using LojaLivros.Dtos.Clientes;
using LojaLivros.Models;

namespace LojaLivros.Services.Cliente
{
    public interface IClienteInterface
    {
        Task<List<ClienteModel>> buscarClientes();
        Task<ClienteRegisterDto> Cadastrar(ClienteRegisterDto clienteRegisterDto);
        Task<ClienteModel> BuscarClientePorId(int? idCliente);
        Task<ClienteEditarDto> Editar(ClienteEditarDto clienteEditarDto);
      Task<ClienteModel> MudarSituacaoCliente(int? idCliente);
    }
}
