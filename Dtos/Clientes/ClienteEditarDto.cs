using LojaLivros.Dtos.Endereco;
using LojaLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Clientes
{
    public class ClienteEditarDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o usuário!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        [Required]
        public EnderecoEditarDto Endereco { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
