using LojaLivros.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Dtos.Usuarios
{
    public class UsuarioRegisterDto
    {
        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; } = String.Empty;
        [Required(ErrorMessage = "Digite o usuário!")]
        public string Usuario { get; set; } = String.Empty;
        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Selecione o cargo!")]
        public PerfilEnum Cargo { get; set; }
        [Required(ErrorMessage = "Selecione o turno!")]
        public TurnoEnum Turno { get; set; }
        [Required(ErrorMessage = "Digite uma senha!")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres!")]
        public string Senha { get; set; } = String.Empty;
        [Required(ErrorMessage = "Digite a confirmação da senha!")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string ConfirmarSenha { get; set; } = String.Empty;

    }
}
