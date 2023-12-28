using AutoMapper;
using LojaLivros.Dtos.Endereco;
using LojaLivros.Dtos.Livro;
using LojaLivros.Dtos.Usuarios;
using LojaLivros.Models;

namespace LojaLivros.Profiles
{
    public class ProfileAutoMapper : Profile
    {

        public ProfileAutoMapper()
        {
            CreateMap<UsuarioModel, UsuarioRegisterDto>();
            CreateMap<UsuarioRegisterDto, UsuarioModel>();
            CreateMap<UsuarioModel, UsuarioEditarDto>();
            CreateMap<UsuarioEditarDto, UsuarioModel>();
            CreateMap<EnderecoModel, EnderecoEditarDto>();
            CreateMap<EnderecoEditarDto, EnderecoModel>();
            CreateMap<LivroRegisterDto, LivroModel>();
            CreateMap<LivroModel, LivroRegisterDto>();
            CreateMap<LivroEditarDto, LivroModel>();
            CreateMap<LivroModel, LivroEditarDto>();
        }
    }
}
