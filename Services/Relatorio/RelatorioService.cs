using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
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

        public RelatorioService()
        {
            
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
                    var d = JsonConvert.SerializeObject(dados);
                    var DadosLivroModel = JsonConvert.DeserializeObject<List<LivroModel>>(d);
                    if(DadosLivroModel != null)
                    {
                        return ExportarLivro(dataTable, DadosLivroModel);
                    }
                   
                    break;
                //case 2:

    

                //    break;
                    //case 3:
                    //    break;
                    //case 4:
                    //    break;
                    //case 5:
                    //    break;
            }

            return new DataTable();


            
        }

        public DataTable ExportarLivro(DataTable data, List<LivroModel> dados)
        {

            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.Capa, dado.ISBN, dado.Titulo, dado.Genero, dado.Autor, dado.AnoPublicacao, dado.QuantidadeEstoque,dado.Emprestimos, dado.Descricao, dado.DataCadastro, dado.DataAlteracao);
            }

            return data;

        }

  







    }
}
