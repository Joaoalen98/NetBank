using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Mudanca_Entidade_Transacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContaEnviouAgencia",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContaEnviouNumero",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaEnviouAgencia",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "ContaEnviouNumero",
                table: "Transacoes");
        }
    }
}
