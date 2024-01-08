using AutoMapper;
using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using LojaLivros.Dtos.Relatorios;
using LojaLivros.Dtos.Response;
using LojaLivros.Enums;
using LojaLivros.Models;
using LojaLivros.Services.Livro;
using Newtonsoft.Json;
using System.Data;

namespace LojaLivros.Services.Relatorio
{
    public class RelatorioService : IRelatorioInterface
    {
        private readonly IMapper _mapper;

        public RelatorioService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DataTable ColetarDados<T>(List<T> dados, int idRelatorio)
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = Enum.GetName(typeof(RelatorioEnum), idRelatorio);

     
            
            var colunas = dados.First().GetType().GetProperties();

       


            foreach (var coluna in colunas)
            {
                
                    dataTable.Columns.Add(coluna.Name, coluna.PropertyType);

                
            }

            



            switch (idRelatorio)
            {
                case 1:
                    var dadosLivro = JsonConvert.SerializeObject(dados);
                    var DadosLivroModel = JsonConvert.DeserializeObject<List<LivroRelatorioDto>>(dadosLivro);
                    if(DadosLivroModel != null)
                    {
                        return ExportarLivro(dataTable, DadosLivroModel);
                    }
                   
                    break;
                case 2:

                    var dadosClientes = JsonConvert.SerializeObject(dados);
                    var dadosCLientesModel = JsonConvert.DeserializeObject<List<ClienteRelatorioDto>>(dadosClientes);
                    if (dadosCLientesModel != null)
                    {
                        return ExportarCliente(dataTable, dadosCLientesModel);
                    }

                    break;
                case 3:
                    var dadosFuncionario = JsonConvert.SerializeObject(dados);
                    var dadosFUncionarioModel = JsonConvert.DeserializeObject<List<FuncionarioRelatorioDto>>(dadosFuncionario);
                    if (dadosFUncionarioModel != null)
                    {
                        return ExportarFuncionario(dataTable, dadosFUncionarioModel);
                    }


                    break;
                case 4:

                    var dadosEmprestimo = JsonConvert.SerializeObject(dados);
                    var dadpsEmprestimoModel = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimo);
                    if (dadpsEmprestimoModel != null)
                    {
                        return ExportarEmprestimo(dataTable, dadpsEmprestimoModel);
                    }

                    break;
                case 5:

                    var dadosEmprestimoPendentes = JsonConvert.SerializeObject(dados);
                    var dadpsEmprestimoPendenteModel = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimoPendentes);
                    if (dadpsEmprestimoPendenteModel != null)
                    {
                        return ExportarEmprestimo(dataTable, dadpsEmprestimoPendenteModel);
                    }

                    break;

            }

            return new DataTable();


            
        }

        public DataTable ExportarLivro(DataTable data, List<LivroRelatorioDto> dados)
        {

            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.Capa, dado.ISBN, dado.Titulo, dado.Genero, dado.Autor, dado.AnoPublicacao, dado.QuantidadeEstoque, dado.Descricao, dado.DataCadastro, dado.DataAlteracao);
            }

            return data;

        }

        public DataTable ExportarCliente(DataTable data, List<ClienteRelatorioDto> dados)
        {

            
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.NomeCompleto, dado.Usuario, dado.Email, dado.Situação == "True" ? "Ativo" : "Inativo", dado.Cargo, dado.Turno, dado.Logradouro, dado.Bairro, dado.Numero, dado.CEP, dado.Estado, dado.Complemento, dado.DataCadastro, dado.DataAlteracao); ;
            }

            return data;

        }

        public DataTable ExportarFuncionario(DataTable data, List<FuncionarioRelatorioDto> dados)
        {


            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.NomeCompleto, dado.Usuario, dado.Email, dado.Situação == "True" ? "Ativo" : "Inativo", dado.Cargo,dado.Turno, dado.Logradouro, dado.Bairro, dado.Numero, dado.CEP, dado.Estado, dado.Complemento, dado.DataCadastro, dado.DataAlteracao);
            }

            return data;

        }

        public DataTable ExportarEmprestimo(DataTable data, List<EmprestimoRelatorioDto> dados)
        {

      

            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.UsuarioId,dado.NomeCompleto, dado.Usuario, dado.LivroId, dado.ISBN, dado.Titulo, dado.DataEmprestimo, dado.DataDevolucao);
            }

            return data;

        }


        public DataTable ExportarEmprestimoPendente(DataTable data, List<EmprestimoRelatorioDto> dados)
        {



            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.UsuarioId, dado.NomeCompleto, dado.Usuario, dado.LivroId, dado.ISBN, dado.Titulo, dado.DataEmprestimo);
            }

            return data;

        }




    }
}
