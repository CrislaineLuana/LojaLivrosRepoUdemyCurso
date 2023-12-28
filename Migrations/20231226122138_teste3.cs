using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLivros.Migrations
{
    /// <inheritdoc />
    public partial class teste3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuário",
                table: "Clientes",
                newName: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Clientes",
                newName: "Usuário");
        }
    }
}
