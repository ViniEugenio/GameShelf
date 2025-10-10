using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JogoGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genero",
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
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogoGenero",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAtivacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoGenero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JogoGenero_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoGenero_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogoGenero_GeneroId",
                table: "JogoGenero",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_JogoGenero_JogoId",
                table: "JogoGenero",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogoGenero");

            migrationBuilder.DropTable(
                name: "Genero");
        }
    }
}
