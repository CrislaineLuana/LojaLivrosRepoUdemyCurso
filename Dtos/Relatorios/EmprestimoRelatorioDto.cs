using LojaLivros.Models;

namespace LojaLivros.Dtos.Relatorios
{
    public class EmprestimoRelatorioDto
    {

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }

        public string Usuario { get; set; }

        public int LivroId { get; set; }

        public string ISBN { get; set; }

        public string Titulo { get; set; }

        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime DataDevolucao { get; set; }
    }
}
