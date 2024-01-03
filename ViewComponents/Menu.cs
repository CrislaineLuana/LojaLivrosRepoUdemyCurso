using LojaLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LojaLivros.ViewComponents
{
    public class Menu: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuario");
            if (string.IsNullOrEmpty(sessaoUsuario)) return View();

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            return View(usuario);
        }
    }
}
