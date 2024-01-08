using AutoMapper;
using ClosedXML.Excel;
using LojaLivros.Dtos.Relatorios;
using LojaLivros.Filters;
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
    [UsuarioLogado]
    public class RelatorioController : Controller
    {
        private readonly ISessao _sessao;
        private readonly ILivroInterface _livroInterface;
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        private readonly IRelatorioInterface _relatorioInterface;
        private readonly IMapper _mapper;

        public RelatorioController(ISessao sessao, ILivroInterface livroInterface, IUsuarioInterface usuarioInterface, IEmprestimoInterface emprestimoInterface, IRelatorioInterface relatorioInterface, IMapper mapper)
        {
            _sessao = sessao;
            _livroInterface = livroInterface;
            _usuarioInterface = usuarioInterface;
            _emprestimoInterface = emprestimoInterface;
            _relatorioInterface = relatorioInterface;
            _mapper = mapper;
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
                    List<LivroRelatorioDto> dadosLivros = _mapper.Map<List<LivroRelatorioDto>>(livros);
                    tabela = _relatorioInterface.ColetarDados(dadosLivros, id);
                    break;
                case 2:

                    List<UsuarioModel> clientes = await _usuarioInterface.BuscarUsuarios(0);
                    //List<ClienteRelatorioDto> dadosClientes = _mapper.Map<List<ClienteRelatorioDto>>(clientes);
                    List<ClienteRelatorioDto> dadosClientes = new List<ClienteRelatorioDto>();
                    foreach (var cliente in clientes)
                    {



                        dadosClientes.Add(
                            new ClienteRelatorioDto
                            {
                                Id = cliente.Id,
                                NomeCompleto = cliente.NomeCompleto,
                                Usuario = cliente.Usuario,
                                Email = cliente.Email,
                                Situação = cliente.Situação.ToString(),
                                Cargo = cliente.Cargo.ToString(),
                                Turno = cliente.Turno.ToString(),
                                Logradouro = cliente.Endereco.Logradouro,
                                Bairro = cliente.Endereco.Bairro,
                                Numero = cliente.Endereco.Numero,
                                CEP = cliente.Endereco.CEP,
                                Estado = cliente.Endereco.Estado,
                                Complemento = cliente.Endereco.Complemento,
                                DataCadastro = cliente.DataCadastro,
                                DataAlteracao = cliente.DataAlteracao
                                


                            }

                        );
                    }


                    tabela = _relatorioInterface.ColetarDados(dadosClientes, id);
                     
                    break;
                case 3:

                    List<UsuarioModel> funcionarios = await _usuarioInterface.BuscarUsuarios(null);
                    //List<FuncionarioRelatorioDto> dadosFuncionarios = _mapper.Map<List<FuncionarioRelatorioDto>>(funcionarios);
                    List<FuncionarioRelatorioDto> dadosFuncionarios = new List<FuncionarioRelatorioDto>();

                    foreach (var funcionario in funcionarios)
                    {



                        dadosFuncionarios.Add(
                            new FuncionarioRelatorioDto
                            {
                                Id = funcionario.Id,
                                NomeCompleto = funcionario.NomeCompleto,
                                Usuario = funcionario.Usuario,
                                Email = funcionario.Email,
                                Situação = funcionario.Situação.ToString(),
                                Cargo = funcionario.Cargo.ToString(),
                                Turno = funcionario.Turno.ToString(),
                                Logradouro = funcionario.Endereco.Logradouro,
                                Bairro = funcionario.Endereco.Bairro,
                                Numero = funcionario.Endereco.Numero,
                                CEP = funcionario.Endereco.CEP,
                                Estado = funcionario.Endereco.Estado,
                                Complemento = funcionario.Endereco.Complemento,
                                DataCadastro = funcionario.DataCadastro,
                                DataAlteracao = funcionario.DataAlteracao

                            }

                        );
                    }

                    tabela = _relatorioInterface.ColetarDados(dadosFuncionarios, id);

                    break;
                case 4:

                    List<EmprestimoModel> emprestimos = await _emprestimoInterface.BuscarEmprestimosGeral(null);
                    //List<FuncionarioRelatorioDto> dadosFuncionarios = _mapper.Map<List<FuncionarioRelatorioDto>>(funcionarios);
                    List<EmprestimoRelatorioDto> dadosEmprestimos = new List<EmprestimoRelatorioDto>();

                    foreach (var emprestimo in emprestimos)
                    {



                        dadosEmprestimos.Add(
                            new EmprestimoRelatorioDto
                            {
                                Id = emprestimo.Id,
                                UsuarioId = emprestimo.UsuarioId,
                                NomeCompleto = emprestimo.Usuario.NomeCompleto,
                                Usuario = emprestimo.Usuario.Usuario,
                                LivroId = emprestimo.LivroId,
                                ISBN = emprestimo.Livro.ISBN,
                                Titulo = emprestimo.Livro.Titulo,
                                DataEmprestimo = emprestimo.DataEmprestimo,
                                DataDevolucao = (DateTime)emprestimo.DataDevolução

                            }

                        );
                    }

                    tabela = _relatorioInterface.ColetarDados(dadosEmprestimos, id);


                    break;
                case 5:

                    List<EmprestimoModel> emprestimosPendentes = await _emprestimoInterface.BuscarEmprestimosGeral("pendente");
                    //List<FuncionarioRelatorioDto> dadosFuncionarios = _mapper.Map<List<FuncionarioRelatorioDto>>(funcionarios);
                    List<EmprestimoRelatorioDto> dadosEmprestimosPendentes = new List<EmprestimoRelatorioDto>();

                    foreach (var emprestimo in emprestimosPendentes)
                    {



                        dadosEmprestimosPendentes.Add(
                            new EmprestimoRelatorioDto
                            {
                                Id = emprestimo.Id,
                                UsuarioId = emprestimo.UsuarioId,
                                NomeCompleto = emprestimo.Usuario.NomeCompleto,
                                Usuario = emprestimo.Usuario.Usuario,
                                LivroId = emprestimo.LivroId,
                                ISBN = emprestimo.Livro.ISBN,
                                Titulo = emprestimo.Livro.Titulo,
                                DataEmprestimo = emprestimo.DataEmprestimo

                            }

                        );
                    }

                    tabela = _relatorioInterface.ColetarDados(dadosEmprestimosPendentes, id);


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
