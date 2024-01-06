using ClosedXML.Excel;
using LojaLivros.Models;
using LojaLivros.Services.Emprestimo;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Relatorio;
using LojaLivros.Services.Sessao;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace LojaLivros.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly ISessao _sessao;
        private readonly ILivroInterface _livroInterface;
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        private readonly IRelatorioInterface _relatorioInterface;

        public RelatorioController(ISessao sessao, ILivroInterface livroInterface, IUsuarioInterface usuarioInterface, IEmprestimoInterface emprestimoInterface, IRelatorioInterface relatorioInterface)
        {
            _sessao = sessao;
            _livroInterface = livroInterface;
            _usuarioInterface = usuarioInterface;
            _emprestimoInterface = emprestimoInterface;
            _relatorioInterface = relatorioInterface;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Gerar(int id)
        {

            var tabela = new DataTable();

            switch (id)
            {
                case 1:
                    List<LivroModel> livros = await _livroInterface.BuscarLivros();
                     tabela = _relatorioInterface.ColetarDados(livros, id);
                    break;
                case 2:

                    //List<UsuarioModel> usuario = await _usuarioInterface.BuscarUsuarios(0);
                    //tabela = _relatorioInterface.ColetarDados(usuario, id);

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(tabela, "Dados");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Dados.xls");

                }
            }

         
        }

    }
}
