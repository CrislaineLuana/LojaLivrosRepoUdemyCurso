using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLivros.Migrations
{
    /// <inheritdoc />
    public partial class situacaoclientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Situação",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situação",
                table: "Clientes");
        }
    }
}
