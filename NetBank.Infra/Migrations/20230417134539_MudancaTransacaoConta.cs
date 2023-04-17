using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MudancaTransacaoConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Contas_ContaEnviouId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Contas_ContaRecebeuId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_ContaRecebeuId",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "ContaEnviouId",
                table: "Transacoes",
                newName: "ContaId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_ContaEnviouId",
                table: "Transacoes",
                newName: "IX_Transacoes_ContaId");

            migrationBuilder.AlterColumn<string>(
                name: "ContaRecebeuId",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ContaRecebeuAgencia",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContaRecebeuNumero",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Contas_ContaId",
                table: "Transacoes",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Contas_ContaId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "ContaRecebeuAgencia",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "ContaRecebeuNumero",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Transacoes",
                newName: "ContaEnviouId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_ContaId",
                table: "Transacoes",
                newName: "IX_Transacoes_ContaEnviouId");

            migrationBuilder.AlterColumn<string>(
                name: "ContaRecebeuId",
                table: "Transacoes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ContaRecebeuId",
                table: "Transacoes",
                column: "ContaRecebeuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Contas_ContaEnviouId",
                table: "Transacoes",
                column: "ContaEnviouId",
                principalTable: "Contas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Contas_ContaRecebeuId",
                table: "Transacoes",
                column: "ContaRecebeuId",
                principalTable: "Contas",
                principalColumn: "Id");
        }
    }
}
