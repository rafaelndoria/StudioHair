using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioHair.Infrascruture.Migrations
{
    /// <inheritdoc />
    public partial class TrocandoTipoColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "ProdutoUnidades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 4, 27, 9, 42, 22, 765, DateTimeKind.Local).AddTicks(2205));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Quantidade",
                table: "ProdutoUnidades",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 4, 22, 21, 54, 6, 293, DateTimeKind.Local).AddTicks(8219));
        }
    }
}
