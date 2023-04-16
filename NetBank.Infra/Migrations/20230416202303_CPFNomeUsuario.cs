using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CPFNomeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Usuarios",
                newName: "CPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Usuarios",
                newName: "Cpf");
        }
    }
}
