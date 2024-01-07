using LojaLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Relatorios
{
    public class LivroRelatorioDto
    {

        public int Id { get; set; }

        public string Capa { get; set; }

        public string ISBN { get; set; }

        public string Titulo { get; set; }

        public string Genero { get; set; }

        public string Autor { get; set; }

        public int AnoPublicacao { get; set; }

        public int QuantidadeEstoque { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
