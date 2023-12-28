using System.Text.Json.Serialization;

namespace LojaLivros.Models
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public ClienteModel Cliente { get; set; }
    }
}
