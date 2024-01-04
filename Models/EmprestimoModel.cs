using System.Text.Json.Serialization;

namespace LojaLivros.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }
        public int LivroId { get; set; }
        [JsonIgnore]
        public LivroModel Livro { get; set; }
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime? DataDevolução { get; set; }

    }
}
