using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Models
{
    public class LivroModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Capa { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Genero  { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public int AnoPublicacao { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        [Required]
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
   
    }
}
