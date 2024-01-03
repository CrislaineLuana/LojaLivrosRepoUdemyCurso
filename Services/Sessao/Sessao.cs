using LojaLivros.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LojaLivros.Services.Sessao
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
                _contextAccessor = httpContextAccessor;
        }
        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("SessaoUsuario");
            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                return null; 
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioJson = JsonConvert.SerializeObject(usuario);
            _contextAccessor.HttpContext.Session.SetString("SessaoUsuario", usuarioJson);
        }

        public void RemoverSessao()
        {
            _contextAccessor.HttpContext.Session.Remove("SessaoUsuario");
        }
    }
}
