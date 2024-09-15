using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioHair.Infrascruture.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoVinculoEntreUsuarioEPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 9, 13, 21, 17, 13, 731, DateTimeKind.Local).AddTicks(9919));

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_UsuarioId",
                table: "Pessoa",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Usuarios_UsuarioId",
                table: "Pessoa",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Usuarios_UsuarioId",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_UsuarioId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pessoa");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 5, 4, 17, 1, 41, 135, DateTimeKind.Local).AddTicks(8718));
        }
    }
}
