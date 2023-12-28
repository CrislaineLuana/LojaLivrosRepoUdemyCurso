using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLivros.Migrations
{
    /// <inheritdoc />
    public partial class Atualizandotabelausuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "Usuarios");
        }
    }
}
