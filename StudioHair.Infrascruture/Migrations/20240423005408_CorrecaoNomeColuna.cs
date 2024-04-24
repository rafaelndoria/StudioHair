using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioHair.Infrascruture.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoNomeColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Número",
                table: "Pessoa",
                newName: "Numero");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 4, 22, 21, 54, 6, 293, DateTimeKind.Local).AddTicks(8219));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Pessoa",
                newName: "Número");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 4, 17, 22, 20, 42, 303, DateTimeKind.Local).AddTicks(6916));
        }
    }
}
