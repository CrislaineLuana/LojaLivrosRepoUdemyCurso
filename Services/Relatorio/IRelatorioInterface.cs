using System.Data;

namespace LojaLivros.Services.Relatorio
{
    public interface IRelatorioInterface
    {
        //   void ColetarDados<T>(List<T> dados, int idRelatorio);
        DataTable ColetarDados<T>(List<T> dados, int idRelatorio);
    }
}
