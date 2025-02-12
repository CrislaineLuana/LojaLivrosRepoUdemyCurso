﻿using LojaLivros.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaLivros.Models
{
    public class ClienteModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
        [Required]
        
        public PerfilEnum Cargo { get; set; }
        [Required]
        public TurnoEnum Turno { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Situação { get; set; } = true;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        //[Required]
        //public EnderecoModel Endereco { get; set; }
 
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
