using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Livro
{
    public class LivroEditarDto
    {
        public int Id { get; set; }
     
        public string? Capa { get; set; }
        [Required(ErrorMessage = "Digite o título!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Digite o ISBN!")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Digite o gênero!")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "Digite o autor!")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Digite o ano de publicação!")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Digite a quantidade em estoque!")]
        public int QuantidadeEstoque { get; set; }
        [Required(ErrorMessage = "Digite a descrição!")]
        public string Descricao { get; set; }
    }
}
