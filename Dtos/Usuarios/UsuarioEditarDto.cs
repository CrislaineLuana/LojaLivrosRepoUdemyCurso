using LojaLivros.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Usuarios
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o usuário! ")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione o cargo!")]

        public PerfilEnum Cargo { get; set; }
        [Required(ErrorMessage = "Selecione o turno!")]
        public TurnoEnum Turno { get; set; }


    }
}
