namespace LojaLivros.Dtos.Relatorios
{
    public class UsuarioRelatorioDto
    {
        public int Id { get; set; }

        public string NomeCompleto { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Situação { get; set; }

        public string Cargo { get; set; }
        public string Turno { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
