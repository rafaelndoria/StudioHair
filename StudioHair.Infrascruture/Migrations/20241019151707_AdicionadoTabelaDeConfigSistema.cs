using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioHair.Infrascruture.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoTabelaDeConfigSistema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoSistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorPrimaria = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#ffc0cb"),
                    CorSecundaria = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#f8f9fa"),
                    CorFonte = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#212529"),
                    TamanhoFonte = table.Column<int>(type: "int", nullable: false, defaultValue: 16),
                    TemaDark = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoSistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoSistemas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 10, 19, 12, 17, 5, 276, DateTimeKind.Local).AddTicks(2868));

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoSistemas_UsuarioId",
                table: "ConfiguracaoSistemas",
                column: "UsuarioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracaoSistemas");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataDeCadastro",
                value: new DateTime(2024, 9, 13, 21, 17, 13, 731, DateTimeKind.Local).AddTicks(9919));
        }
    }
}
