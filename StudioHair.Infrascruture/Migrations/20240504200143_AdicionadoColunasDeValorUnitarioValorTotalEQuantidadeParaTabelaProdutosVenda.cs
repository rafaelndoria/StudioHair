using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioHair.Infrascruture.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoColunasDeValorUnitarioValorTotalEQuantidadeParaTabelaProdutosVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "ProdutosVendas",
                newName: "ValorUnitario");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ProdutosVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "ProdutosVendas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 5, 4, 17, 1, 41, 135, DateTimeKind.Local).AddTicks(8718));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ProdutosVendas");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "ProdutosVendas");

            migrationBuilder.RenameColumn(
                name: "ValorUnitario",
                table: "ProdutosVendas",
                newName: "Valor");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 4, 27, 9, 42, 22, 765, DateTimeKind.Local).AddTicks(2205));
        }
    }
}
