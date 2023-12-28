using LojaLivros.Dtos.Login;
using LojaLivros.Models;
using LojaLivros.Services.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaLivros.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeInterface _homeInterface;
        public HomeController(IHomeInterface homeInterface)
        {
            _homeInterface = homeInterface;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDto loginDto)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var login = _homeInterface.RealizarLogin(loginDto);
        //    }
        //}
    }
}