using LojaLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaLivros.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }


        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
    }
}

