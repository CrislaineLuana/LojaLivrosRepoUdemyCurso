using LojaLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaLivros.Filters
{
    public class UsuarioLogado : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuario");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {
                        "controller", "Home"
                    },
                    {
                        "Action", "Login"
                    }
                });
            }else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {
                        "controller", "Home"
                    },
                    {
                        "Action", "Login"
                    }
                });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
