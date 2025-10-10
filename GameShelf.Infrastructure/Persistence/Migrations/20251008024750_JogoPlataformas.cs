using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JogoPlataformas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogoPlataforma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlataformaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoPlataforma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JogoPlataforma_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoPlataforma_Plataforma_PlataformaId",
                        column: x => x.PlataformaId,
                        principalTable: "Plataforma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogoPlataforma_JogoId",
                table: "JogoPlataforma",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_JogoPlataforma_PlataformaId",
                table: "JogoPlataforma",
                column: "PlataformaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogoPlataforma");

            migrationBuilder.DropTable(
                name: "Plataforma");
        }
    }
}
