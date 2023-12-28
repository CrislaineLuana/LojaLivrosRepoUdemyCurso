using LojaLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Endereco
{
    public class EnderecoEditarDto
    {
        public int Id { get; set; }
   
        [Required(ErrorMessage = "Digite o logradouro!")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Digite o bairro!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o número!")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Digite o CEP!")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o estado!")]
        public string Estado { get; set; }
        public string? Complemento { get; set; }
        public int ClienteId { get; set; }
    }
}
